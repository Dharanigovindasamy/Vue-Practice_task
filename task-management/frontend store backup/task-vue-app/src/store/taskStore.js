// // import { defineStore } from "pinia";
// // import { getTask } from "@/service/getTask";
// // //import { addTask } from "@/service/addTask";

// // export const useTaskStore = defineStore("task", {
// //   state: () => ({
// //     tasks: [], 
// //   }),
// //   actions: {
// //     async fetchTask() {
// //       try {
// //         const response = await getTask(); 
// //         if (response) {
// //           this.tasks = [...response]; 
          
// //           console.log("Tasks fetched successfully", this.tasks);
// //         } else {
// //           console.error("No data received from API");
// //         }
// //       } catch (error) {
// //         console.error("Error fetching tasks:", error);
// //       }
// //     },
// //     async createTask(task) {
// //       try {
// //         task.taskId = this.tasks.length > 0 ? this.tasks[this.tasks.length - 1].taskId + 1 : 1;
// //         //const response = await addTask(task);
// //         //console.log("Task added ", response);
// //         this.tasks.push(task);
// //         console.log("Task added successfully", this.tasks);
// //       } catch (error) {
// //         console.error("Error adding task:", error);
// //       }
// //     },
// //     async deleteTask(taskId) {
// //       try {
// //         this.tasks = this.tasks.filter((task) => task.id !== taskId);
// //       } catch (error) {
// //         console.error("Error deleting task:", error);
// //       }
// //     },
// //   },
// // });



// import { defineStore } from "pinia";
// import { ref } from "vue";
// import { getTask } from "@/service/getTask";
// //import axios from "axios";

// export const useTaskStore = defineStore("taskStore", () => {
//   const tasks = ref([]);

//   const fetchTask = async () => {
//     // try {
//     //   const response = await axios.get("http://localhost:5000/api/tasks"); 
//     //   tasks.value = response.data;
//     //   console.log("fetch tasks", tasks.value);
//     // } catch (error) {
//     //   console.error("Error fetching tasks:", error);
//     // }
//     try {
//                const response = await getTask(); 
//                console.log(response, "get task response");
//                if (response) {
//                 tasks.value = [...response]; 
                
//                  console.log("Tasks fetched successfully", tasks.value);
//                } else {
//                  console.error("No data received from API");
//                }
//             } catch (error) {
//              console.error("Error fetching tasks:", error);
//              }
//   };


//   const createTask = (newTask) => {
//     tasks.value.push(newTask);
//     console.log(tasks.value,'instore');
//   };

//   const deleteTask = (taskId) => {
//     console.log("taskId after delete", taskId);
//     tasks.value = tasks.value.filter((task) => task.id !== taskId);
//     console.log(tasks.value,'delete in store');
//   };
 

//   return { tasks, fetchTask, createTask, deleteTask };
// });



import { defineStore } from "pinia";
import { ref } from "vue";
import { getTask } from "@/service/getTask";

export const useTaskStore = defineStore("taskStore", () => {
  const tasks = ref([]);

  // const fetchTask = async () => {
  //   try {
  //     const response = await getTask();
  //     console.log(response, "get task response");
  //     if (response) {
  //       // Merge fetched tasks with existing tasks, ensuring no duplicates
  //       const existingTaskIds = new Set(tasks.value.map((task) => task.taskId));

  //       const newTasks = response.filter((task) => !existingTaskIds.has(task.taskId));

  //       tasks.value = [...tasks.value, ...newTasks]; // Keep existing and add new
  //       console.log("Tasks fetched successfully", tasks.value);
  //     } else {
  //       console.error("No data received from getTask");
  //     }
   
  //   } catch (error) {
  //     console.error("Error fetching tasks:", error);
  //   }
  // };
  const fetchTask = async () => {
    try {
      const response = await getTask();
      console.log(response, "get task response");
  
      if (response && Array.isArray(response)) {
        tasks.value = response;
       // tasks.value = [...response];
        console.log("Tasks fetched successfully", tasks.value);
      } else {
        console.error("No data received or data is not an array");
      }
    } catch (error) {
      console.error("Error fetching tasks:", error);
    }
  };
  

  const createTask = (newTask) => {
    if (!newTask.taskId) {
      newTask.taskId =
        tasks.value.length > 0 ? tasks.value[tasks.value.length - 1].taskId + 1 : 1;
    }
    newTask.status = "Pending";
    if (!tasks.value.some((task) => task.taskId === newTask.taskId)) {
      tasks.value.push(newTask);
      console.log("Task added successfully", tasks.value);
    }
    // tasks.value.push(newTask);
    // console.log("Task added successfully", tasks.value, "in store");
  };

  const deleteTask = (taskId) => {
    console.log("Deleting task with taskId:", taskId);
    tasks.value = tasks.value.filter((task) => task.taskId !== taskId);
    console.log("Tasks after deletion:", tasks.value, "in store");
  };

  const updateTask = (updatedTask) => {
    const index = tasks.value.findIndex((t) => t.taskId === updatedTask.taskId);
    if (index !== -1) {
      tasks.value[index] = { ...updatedTask }; 
    }
    console.log("updated task in store ", updateTask);
  };

  return { tasks, fetchTask, createTask, deleteTask, updateTask };
});