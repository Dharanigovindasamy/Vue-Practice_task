import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../components/Dashboard/HomePage.vue';
import TaskComponent from '../components/Dashboard/TaskComponent.vue';
import OrganiseComponent from '../components/Organiser/OrganiseComponent.vue';

const router = createRouter({
  history: createWebHistory(), 
  routes: [
    {
     path: '/',
     component:HomePage, 
    },
    {
      path: '/Home',
      name: 'Home',
      component: TaskComponent,
    },
    {
      path: '/Organise',
      name: 'Organise',
      component: OrganiseComponent,
    },
  ],
});

export default router;