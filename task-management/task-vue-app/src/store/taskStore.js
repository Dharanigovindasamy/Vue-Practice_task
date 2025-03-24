import { defineStore } from "pinia";
import { getTask } from "@/service/getTask";

export const useTaskStore = defineStore("task", {
  state: () => ({
    tasks: [], 
  }),
  actions: {
    async fetchTask() {
      try {
        const response = await getTask(); 
        if (response) {
          this.tasks = [...response]; 
        } else {
          console.error("No data received from API");
        }
      } catch (error) {
        console.error("Error fetching tasks:", error);
      }
    },
  },
});
