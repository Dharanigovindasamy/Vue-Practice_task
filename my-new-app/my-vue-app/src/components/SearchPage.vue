<template>
  <div>
    <h2>Search Users</h2>
    <input
      v-model="searchQuery"
      placeholder="Search by name or username"
      @input="handleSearch"/>

    <ul>
      <li
        v-for="user in filteredUsers"
        :key="user.id"
        :class="{ selected: selectedUserIds.includes(user.id) }"
        @click="selectUser(user)"
      >
        <span v-html="highlightMatch(user.name)"></span> -
        <span v-html="highlightMatch(user.username)"></span>
      </li>
    </ul>

<div class="user-details">
  <h3>Selected Users</h3>
  <div class="user-cards">
    <div v-show="tableCreated" v-for="user in choosenUsers" :key="user.id" class="user-card">
      <h4>Name: {{ user.name }}</h4>
      <p>Email: {{ user.email }}</p>
      <p>City: {{ user.address.city }}</p>
      <p>Username:{{ user.username }}</p>
    

    <div class="post-users">
      <h3>User Posts</h3>
      <ul>
        <li v-for="post in postUsers" :key="post.id">
          <strong>{{ post.title }}</strong>
          <p>{{ post.body }}</p>
        </li>
      </ul>
    </div>
    </div>
  </div>
</div>

    
</div>

</template>

<script setup>
import { ref, onMounted, watch } from "vue";
import { useUserStore } from "../store/userStore";
import { usePostStore } from "../store/postStore";

const searchQuery = ref("");
const filteredUsers = ref([]);
const selectedUserIds = ref([]);
const choosenUsers = ref([]);
const postUsers = ref([]);
const userStore = useUserStore();
const postStore = usePostStore();
let detailsLoaded = ref(false);
let tableCreated = ref(false);

onMounted(async () => {
  await userStore.loadUsers();
  console.log("Mounted called");
  console.log(userStore.users, "Loaded Users");
});

const handleSearch = () => {
  if (searchQuery.value.length >= 2) {
    filteredUsers.value = userStore.users.filter(
      (user) =>
        user.name.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        user.username.toLowerCase().includes(searchQuery.value.toLowerCase())
    );

    console.log("Filtered Users:", filteredUsers.value);
  } else {
    filteredUsers.value = [];
  }
};

const selectUser = async (user) => {
  tableCreated.value = false;
  if (!selectedUserIds.value.includes(user.id)) {
    choosenUsers.value = [];
    selectedUserIds.value.push(user.id);
    choosenUsers.value.push(user);
  }

  await postStore.loadPosts(user.id).then(() => {
    postUsers.value = Array.isArray(postStore.posts)
      ? postStore.posts.filter((post) => post.id === user.id)
      : [];

    console.log("Selected Users:", choosenUsers.value);
    console.log("Post Users:", postUsers.value);
    detailsLoaded.value = true;
  });
};
watch(detailsLoaded, (newVal) => {
  if (detailsLoaded.value) {
    console.log("Details Loaded:", postStore.posts, newVal);
    consolePosts(postStore.posts);
    tableCreated.value = true;
  }
});

// Watch for changes in postStore.posts and update postUsers reactively
watch(
  () => postStore.posts,
  (newPosts) => {
    postUsers.value = newPosts && [...newPosts];
  },
  { deep: true }
);
const consolePosts = (post) => {
  console.log(post, "console.post");
  detailsLoaded.value = false;
};
const highlightMatch = (text) => {
  if (searchQuery.value.length < 2) return text;
  const regex = new RegExp(`(${searchQuery.value})`, "gi");
  return text.replace(regex, `<mark>$1</mark>`);
};
</script>

<style scoped>
/* .user-details {
  text-align: center;
} */

.user-cards {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  /* justify-content: center; */
}

.user-card {
  background: white;
  padding: 15px;
  border-radius: 10px;
  box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
  text-align: center;
}

input {
  width: 300px;
  padding: 8px;
  margin-bottom: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

ul {
  list-style: none;
  padding: 0;
}

li {
  padding: 5px 0;
  cursor: pointer;
}

li:hover {
  background-color: #f0f0f0;
}

mark {
  background-color: yellow;
}

.selected {
  font-weight: bold;
  color: blue;
}

.post-users {
  margin-top: 20px;
  
}

.post-users li {
  border-bottom: 1px solid #ddd;
  padding: 10px 0;
}
</style>
