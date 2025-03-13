<template>
  <div id="app">
    <h1>User List</h1>

    <input type="text" v-model="searchQuery" placeholder="Search users..." />

    <ul>
      <li v-for="user in filteredUsers" :key="user.id">
        <span @click="selectUser(user)">{{ user.name }}</span>
      </li>
    </ul>

    <div v-if="selectedUser">
      <h2>User Details</h2>
      <p>Name: {{ selectedUser.name }}</p>
      <p>Email: {{ selectedUser.email }}</p>
      <button @click="clearSelection">Close</button>
    </div>

  </div>
</template>

<script>
import { ref, computed, watch, onMounted } from "vue";
import axios from "axios";

export default {
  setup() {
    const users = ref([]); 
    const searchQuery = ref("");
    const selectedUser = ref(null); 

    const fetchUsers = async () => {
      try {
        const response = await axios.get("http://localhost:5000/api/users");
        console.log("response data", response);
        console.log("response details", response.data);
        users.value = response.data;
      } catch (error) {
        console.error("Error fetching users:", error);
      } 
    };

    const filteredUsers = computed(() =>
      users.value.filter((user) =>
        user.name.toLowerCase().includes(searchQuery.value.toLowerCase())
      )
    );

    watch(searchQuery, (newQuery) => {
      console.log("Search changed:", newQuery);
    });

    const selectUser = (user) => {
      selectedUser.value = user;
    };

    const clearSelection = () => {
      selectedUser.value = null;
    };

    onMounted(fetchUsers);

    return {
      users,
      searchQuery,
      filteredUsers,
      selectedUser,
      selectUser,
      clearSelection,
    };
  },
};
</script>

<style>
#app {
  text-align: center;
  margin-top: 20px;
}
input {
  margin-bottom: 10px;
  padding: 5px;
}
ul {
  list-style: none;
  padding: 0;
}
li {
  cursor: pointer;
  margin: 5px 0;
}
</style>
