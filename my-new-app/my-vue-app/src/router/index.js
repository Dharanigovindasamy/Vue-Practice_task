import { createRouter, createWebHistory } from "vue-router";
import HomePage from "../components/HomePage.vue";
import AboutPage from "../components/AboutPage.vue";
import ContactPage from "../components/ContactPage.vue";
import TableForm from "../components/TableForm.vue";
import InnerForm from "../components/InnerForm.vue";
import SearchPage from "../components/SearchPage.vue";

const routes = [
  { path: "/", component: HomePage },
  { path: "/about", component: AboutPage },
  { path: "/contact", component: ContactPage },
  {
    path: "/users",
    component: InnerForm,
    children: [
      { path: "userTable", component: TableForm },
      { path: "searchUser", component: SearchPage },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;

// let results = [3,5,9]
// let postsResult = Promise.all(() => {
//     results.map((item) => {
//         return axios.typicodeApi.get(`/posts/${item}`)
//     })
// })
// postsResult // [{id:3,message:"post 3 messgae"}, {id:5,message:"post 5 messgae"}, {id:9,message:"post 9 messgae"}]

