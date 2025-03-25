import { defineStore } from "pinia";
import { getTask } from "@/service/getTask";
import { addTask } from "@/service/addTask";

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

    async addTask(task) {
      try {
        const response = await addTask(task);
        console.log("Task added ", response);
        this.tasks.push(task);
        console.log("Task added successfully", this.tasks);
      } catch (error) {
        console.error("Error adding task:", error);
      }
    },

    async deleteTask(taskId) {
      try {
        this.tasks = this.tasks.filter((task) => task.id !== taskId);
      } catch (error) {
        console.error("Error deleting task:", error);
      }
    },
  },
});
