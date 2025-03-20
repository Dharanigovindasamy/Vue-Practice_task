<template>
  <div v-if="selectedUser" class="user-details">
    <h3>User Details</h3>
    <div class="user-card">
      <h4>Name: {{ selectedUser.name }}</h4>
      <p>Email: {{ selectedUser.email }}</p>
      <p>City: {{ selectedUser.address.city }}</p>
      <p>Username: {{ selectedUser.username }}</p>
    </div>

    <div v-if="postUsers.length" class="post-users">
      <h3>User Posts</h3>
      <ul>
        <li v-for="post in postUsers" :key="post.id">
          <strong>{{ post.title }}</strong>
          <p>{{ post.body }}</p>
        </li>
      </ul>
    </div>
    <button @click="goBack">Back to Table</button>
  </div>
</template>

<script setup>
import {defineProps, defineEmits } from "vue";


defineProps({
  selectedUser: Object,
  postUsers: Array,
});

const emit = defineEmits(["backToTable"]);

const goBack = () => {
  emit("backToTable");

};
</script>

<style scoped>
.user-details {
  margin-top: 20px;
}

.user-card {
  background: white;
  padding: 15px;
  border-radius: 10px;
  box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
  text-align: center;
  margin-bottom: 20px;
}

.post-users ul {
  list-style: none;
  padding: 0;
}

.post-users li {
  border-bottom: 1px solid #ddd;
  padding: 10px 0;
}

button {
  margin-top: 20px;
  padding: 10px 15px;
  background-color: #6b24a6;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

button:hover {
  background-color: #5a1d8b;
}
</style>
