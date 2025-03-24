<script> 
import NavBarComponent from './components/Common/NavBarComponent.vue';
export default {
  components: {
    NavBarComponent,
  },
};
</script>

<template>
  <div>
    <nav-bar-component />
    <router-view /> 
  </div>
</template>

<style>
</style>
