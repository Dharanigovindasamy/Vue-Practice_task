import { fetchPosts } from "@/services/mockAPI";
import { defineStore } from "pinia";

export const usePostStore = defineStore("post", {
  state: () => ({
    posts: [],
  }),

  actions: {
    async loadPosts(Ids) {
      this.posts = await fetchPosts(Ids);
      console.log("Posts loaded for user:", this.posts);
    },
  },
});
