// // import { fetchPosts } from "@/services/mockAPI";
// // import { defineStore } from "pinia";

// // export const usePostStore = defineStore("post", {
// //   state: () => ({
// //     posts: [],
// //   }),

// //   actions: {
// //     async loadPosts(Ids) {
// //       if (!Ids || Ids.length === 0) {
// //         this.posts = []; 
// //         return [];
// //       }

// //       console.log("Loading posts for users:", Ids);
// //       try {
// //         this.posts = await fetchPosts(Ids);
// //         console.log("Posts loaded:", this.posts);
// //         return this.posts; 
// //       } catch (error) {
// //         console.error("Error loading posts:", error);
// //         return []; 
// //       }
// //     },

// //     clearPosts() {
// //       this.posts = []; 
// //     },
// //   },
// // });
// import { fetchPosts } from "@/services/mockAPI";
// import { defineStore } from "pinia";

// export const usePostStore = defineStore("post", {
//   state: () => ({
//     posts: [],
//   }),

//   actions: {
//     async loadPosts(userId) {  // ✅ Ensure this is a single userId
//       if (!userId) {
//         this.posts = []; 
//         return [];
//       }

//       console.log("Loading posts for user:", userId);
//       try {
//         this.posts = await fetchPosts(userId); // ✅ Ensure correct userId is passed
//         console.log("Posts loaded:", this.posts);
//         return this.posts; 
//       } catch (error) {
//         console.error("Error loading posts:", error);
//         return []; 
//       }
//     },

//     clearPosts() {
//       this.posts = []; 
//     },
//   },
// });


import { fetchPosts } from "@/services/mockAPI";
import { defineStore } from "pinia";

export const usePostStore = defineStore("post", {
  state: () => ({
    posts: [], 
  }),

  actions: {
    async loadPosts(Ids) {
      if (!Ids || Ids.length === 0) {
        this.posts = []; 
        return [];
      }

      console.log("Loading posts for users:", Ids);
      try {
        this.posts = await fetchPosts(Ids);
        console.log("Posts loaded:", this.posts);
        return this.posts; 
      } catch (error) {
        console.error("Error loading posts:", error);
        this.posts = []; 
        return [];
      }
    },

    clearPosts() {
      this.posts = []; 
    },
  },
});

