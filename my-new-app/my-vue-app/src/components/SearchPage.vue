<template>
  <div>
    <h2>Search Users</h2>
    <input v-model="searchQuery" placeholder="Search by name or username" @input="handleSearch" />

    <ul>
      <li 
        v-for="user in filteredUsers" 
        :key="user.id" 
        :class="{ selected: selectedUser?.id === user.id }"
        @click="selectUser(user)"
      >
        <span v-html="highlightMatch(user.name)"></span> - 
        <span v-html="highlightMatch(user.username)"></span>
      </li>
    </ul>

    <div v-if="selectedUser" class="user-details">
      <h3>User Details</h3>
      <p><strong>Name:</strong> {{ selectedUser.name }}</p>
      <p><strong>Username:</strong> {{ selectedUser.username }}</p>
      <p><strong>Email:</strong> {{ selectedUser.email }}</p>
      <p><strong>Phone:</strong> {{ selectedUser.phone }}</p>
      <p><strong>Company:</strong> {{ selectedUser.company.name }}</p>

      <h3>Posts by {{ selectedUser.name }}</h3>
      <ul>
        <li v-for="post in postStore.posts" :key="post.id">
          <strong>{{ post.title }}</strong>
          <p>{{ post.body }}</p>
        </li>
      </ul>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from "vue";
import { useUserStore } from "../store/userStore";
import { usePostStore } from "../store/postStore";

const searchQuery = ref("");
const selectedUser = ref(null);  // Store the selected user details

const userStore = useUserStore();
const postStore = usePostStore();

// Computed property to filter users based on search input
const filteredUsers = computed(() =>
  searchQuery.value.length >= 2
    ? userStore.users.filter(
        (user) =>
          user.name.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
          user.username.toLowerCase().includes(searchQuery.value.toLowerCase())
      )
    : []
);

// Fetch user details and their associated posts
const selectUser = async (user) => {
  selectedUser.value = user;  // Store selected user details
  await postStore.loadPosts(user.id);  // Fetch posts using `posts/${id}`
  console.log(postStore.posts, "Posts loaded");
};

// Highlight search matches in the user list
const highlightMatch = (text) => {
  if (searchQuery.value.length < 2) return text;
  const regex = new RegExp(`(${searchQuery.value})`, "gi");
  return text.replace(regex, `<mark>$1</mark>`);
};
</script>

<style scoped>
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

.user-details {
  margin-top: 20px;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 4px;
  background-color: #fafafa;
}
</style>
