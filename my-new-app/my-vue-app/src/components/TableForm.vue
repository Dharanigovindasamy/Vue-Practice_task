<template>
  <div>
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
          <td>{{ user.name }}</td>
          <td>{{ user.email }}</td>
          <td>{{ user.username }}</td>
          <td>{{ user.address.street }}, {{ user.address.city }}</td>
          <td>{{ user.phone }}</td>
          <td>{{ user.company.name }}</td>
        </tr>
      </tbody>
    </table>
    <button @click="userFetch">Click</button>
  </div>
</template>

<script setup>
import { onMounted } from "vue";
import { useUserStore } from "../store/userStore";

const userStore = useUserStore();

onMounted(async () => {
  console.log("Mounted called");
  await userStore.loadUsers(); // ✅ Ensure data is loaded before usage
});

function userFetch() {
  console.log(userStore.users, "Loaded Users");
}
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
</style>
