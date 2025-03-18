<script setup>
import { ref } from "vue";
import { RouterView, RouterLink} from "vue-router";

const isMenuOpen = ref(false);
</script>

<template>
  <div class="app">
    <nav class="navbar">
      <div class="menu-icon" @click="isMenuOpen = !isMenuOpen">
        ☰
      </div>
      <h2 class="title">User Management</h2>
    </nav>

    <div class="sidebar" :class="{ open: isMenuOpen }">
      <RouterLink to="/" @click="isMenuOpen = false">Home</RouterLink>
      <RouterLink to="/about" @click="isMenuOpen = false">About</RouterLink>
      <RouterLink to="/contact" @click="isMenuOpen = false">Contact</RouterLink>
      <RouterLink to="/users" @click="isMenuOpen = false">Users</RouterLink>
    </div>

    <div class="content">
      <RouterView />
    </div>
  </div>
</template>

<style scoped>
.app {
  font-family: Arial, sans-serif;
  display: flex;
  flex-direction: column;
  height: 100vh;
}

.navbar {
  background-color: #333;
  color: white;
  display: flex;
  align-items: center;
  padding: 10px 20px;
}

.menu-icon {
  font-size: 24px;
  cursor: pointer;
  margin-right: 10px;
}

.sidebar {
  position: fixed;
  top: 0;
  left: -250px;
  width: 250px;
  height: 100%;
  background: #222;
  display: flex;
  flex-direction: column;
  padding-top: 50px;
  transition: 0.3s;
}

.sidebar.open {
  left: 0;
}

.sidebar a {
  color: white;
  text-decoration: none;
  padding: 15px;
  display: block;
}

.sidebar a:hover {
  background: #444;
}

.content {
  padding: 20px;
  flex-grow: 1;
}
</style>
