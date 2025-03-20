import { fetchUsers } from "@/services/localService";
import { defineStore } from "pinia";

export const useUserStore = defineStore("user", {
  state: () => ({
    users: [],
  }),
  
  actions: {
    async loadUsers() {
      this.users = await fetchUsers();
      console.log("Users loaded:22", this.users);
    },
  },
});
