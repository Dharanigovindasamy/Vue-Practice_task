<template>
  <div class="inner-form">
    <h2>User Actions</h2>
    <div class="nav-links">
      <router-link to="/users/userTable">TableForm</router-link>
      <router-link to="/users/searchUser">SearchPage</router-link>
    </div>
    <router-view />
  </div>
</template>

<style scoped>
.inner-form{
    /* text-align: center; */
    display: center;
}
.nav-links {
  display: flex;
  /* text-align: center; */
  gap: 20px;
  margin-bottom: 20px;
}

.nav-links a {
  text-decoration: none;
  font-weight: bold;
text-align: center;
  color: blue;
}

.nav-links a:hover {
  text-decoration: underline;
}
</style>
