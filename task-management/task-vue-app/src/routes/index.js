import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../components/Dashboard/HomePage.vue';
import TaskComponent from '../components/Dashboard/TaskComponent.vue';
import OrganiseComponent from '../components/Organiser/OrganiseComponent.vue';
import TaskCreationComponent from '../components/Dashboard/TaskCreationComponent.vue';

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      component: HomePage,
    },
    {
      path: '/Home',
      name: 'Home',
      component: TaskComponent,
      
    },
    {
      path: "/AddTask",
      name: "AddTask",
      component: TaskCreationComponent,
    },
    {
      path: '/Organise',
      name: 'Organise',
      component: OrganiseComponent,
    },
  ],
});

export default router;