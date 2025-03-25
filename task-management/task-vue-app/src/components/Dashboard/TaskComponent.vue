<template>
  <div>
    <h3 class="task-list">Task List</h3>

    <div class="task-progress">
      <b-button variant="primary" class="addTask" @click="handleAddTask"> Add Task </b-button>
      <b-button variant="primary" class="deleteTask"> Delete Task </b-button>
    </div>

    <div class="container mt-4">
      <div class="ag-theme-alpine custom-ag-grid">

        <AgGridVue
          :rowData="rowData" 
          :columnDefs="columnDefs"
          :defaultColDef="defaultColDef"
          animateRows="true"
          rowSelection="multiple"
          :modules="modules"
        />
        <!-- <a href="#" @click.prevent="selectTask(task)">{{ rowData.value.taskName}}</a> -->

      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { AgGridVue } from "ag-grid-vue3";
import { ClientSideRowModelModule } from "ag-grid-community";
import { useTaskStore } from "@/store/taskStore";
import { useRouter } from "vue-router";

const modules = ref([ClientSideRowModelModule]);
const taskStore = useTaskStore();
const router = useRouter();
const rowData = ref([]);

const columnDefs = ref([
  { field: "taskId", headerName: "Task Id", sortable: true, filter: true },
  { field: "taskName", headerName: "Task Name", sortable: true, filter: true },
  { field: "category", headerName: "Category", sortable: true, filter: true },
  { field: "status", headerName: "Status", sortable: true, filter: true }
]);

const defaultColDef = ref({
  flex: 1,
  sortable: true,
  filter: true,
});

onMounted(async () => {
  await taskStore.fetchTask();
  rowData.value = taskStore.tasks; 
  rowData.value = rowData.value.map((task) => {
    return {
      taskId: task.taskId,
      taskName: task.taskName,
      category: task.category,
      status: task.status,
    };
  });
});

const handleAddTask = () => {
  router.push({ name: "AddTask" }); 
  taskStore.addTask();
  console.log("Task Added in store");
};

// const selectTask = (task) => {
//   console.log("Task Selected", task);
//   //router.push({ name: "TaskDetails", params: { taskId: task.taskId } });
// };
</script>

<style>
.task-list {
  text-align: center;
  margin-top: 20px;
  padding: 15px;
  font-size: 2rem;
}
.task-progress {
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
  margin-top: 20px;
  padding: 15px;
  font-size: 1rem;
}
.deleteTask,
.addTask {
  margin-left: 10px;
}
.ag-layout-normal {
  height: auto !important;
}
.ag-theme-alpine .ag-header {
  background-color: #007bff !important;
  color: white !important;
  font-weight: bold;
  font-size: 16px;
}

.ag-theme-alpine .ag-cell {
  border-right: 1px solid #ddd !important;
  padding: 10px;
  font-size: 14px;
}

.ag-theme-alpine .ag-row-hover {
  background-color: #f1f1f1 !important;
}

.ag-theme-alpine .ag-row-selected {
  background-color: #d4e8ff !important; 
}
</style>
