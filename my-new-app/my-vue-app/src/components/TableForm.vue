<!-- TableForm.vue -->
<template>
  <div>
    <section v-if="showTable">
      <h2>User Table</h2>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Email</th>
            <th>Username</th>
            <th>Address</th>
            <th>Phone</th>
            <th>Company</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="user in userStore.users" :key="user.id">
            <td>{{ user.id }}</td>
            <td>
              <a href="#" @click.prevent="selectUser(user)">{{ user.name }}</a>
            </td>
            <td>{{ user.email }}</td>
            <td>{{ user.username }}</td>
            <td>{{ user.address.street }}, {{ user.address.city }}</td>
            <td>{{ user.phone }}</td>
            <td>{{ user.company.name }}</td>
          </tr>
        </tbody>
      </table>
    </section>

    <section v-else>
      <UserDetails
        v-if="selectedUser"
        :selectedUser="selectedUser"
        :postUsers="postUsers"
        @backToTable="showTable = true"
      />
    </section>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useUserStore } from "../store/userStore";
import { usePostStore } from "../store/postStore";
import UserDetails from "../components/UserDetails.vue";

const userStore = useUserStore();
const postStore = usePostStore();
const selectedUser = ref(null);
const postUsers = ref([]);
const showTable = ref(true);

onMounted(async () => {
  await userStore.loadUsers();
  console.log("Users Loaded:", userStore.users);
});

const selectUser = async (user) => {
  showTable.value = false; 
  selectedUser.value = user; 
  console.log("Selected User in table page:", user, user.id);

  try {
    const posts = await postStore.loadPosts(user.id); 
    console.log("Fetched Posts:", posts);
  } catch (error) {
    console.error("Error fetching user posts:", error);
  }
};
</script>

<style scoped>
table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 10px;
}

th, td {
  border: 1px solid #ddd;
  padding: 8px;
  text-align: left;
}

th {
  background-color: #6b24a6;
  color: white;
}

a {
  color: #6b24a6;
  text-decoration: none;
  font-weight: bold;
}

a:hover {
  text-decoration: underline;
}
</style>
