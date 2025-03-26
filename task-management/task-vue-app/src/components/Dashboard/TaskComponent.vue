<script setup>
import { onMounted, ref, watch } from "vue";
import { AgGridVue } from "ag-grid-vue3";
import { ClientSideRowModelModule } from "ag-grid-community";
import { useTaskStore } from "@/store/taskStore";
import { useRouter } from "vue-router";
import DeleteButtonRenderer from "@/components/Dashboard/DeleteButtonRenderer.vue";
console.log("D", DeleteButtonRenderer);


const modules = ref([ClientSideRowModelModule]);
const taskStore = useTaskStore();
const router = useRouter();
const rowData = ref(taskStore.tasks.length > 0 ? [...taskStore.tasks] : []);
const gridApi = ref(null);
//const showDeleteModal = ref(false);
//const taskToDelete = ref(null);

// const handleDelete = (params) => {
//   alert(`Clicked on ${params.data.name}`);
// };

// const getRowById = (rowId) => {
//   if (gridApi.value) {
//     const rowNode = gridApi.value.getRowNode(rowId);
//     if (rowNode) {
//       alert(`Row Data: ${JSON.stringify(rowNode.data, null, 2)}`);
//     } else {
//       alert("Row not found!");
//     }
//   }
// };
const columnDefs = ref([
  { field: "taskId", headerName: "Task Id", sortable: true, filter: true },
  { field: "taskName", headerName: "Task Name", sortable: true, filter: true },
  { field: "category", headerName: "Category", sortable: true, filter: true },
  { field: "status", headerName: "Status", sortable: true, filter: true },
  {
    field: "delete",
    headerName: "Delete",
    cellRenderer: DeleteButtonRenderer,
    //  cellRendererParams: { context: { deleteTask } },
  //  cellRenderer: () => '<img src=".png" alt="Delete" style="width: 20px; cursor: pointer;" />',
  },
]);
console.log("Task component");
const defaultColDef = ref({
  flex: 1,
  sortable: true,
  filter: true,
});


const onGridReady = (params) => {
  gridApi.value = params.api;
  console.log("Grid is ready:", gridApi.value);
  //  const selectedRowData = gridApi.value!.getSelectedRows();
  //     gridApi.value!.applyTransaction({ remove: selectedRowData });
};

const deletedTask = (taskId) => {
  taskStore.deleteTask(taskId);
  console.log("Deleting task with ID:", taskId);
  if (taskId && gridApi.value) {
    const rowNode = gridApi.value.getRowNode(taskId);
    if (rowNode) {
      gridApi.value.applyTransaction({ remove: [rowNode.data] }); // Remove row from grid
      taskStore.deleteTask(taskId); // Update store
    }
  }
};

onMounted(async () => {
  console.log("mounted", rowData.value);
  if (rowData.value.length === 0) {
    await taskStore.fetchTask();
    rowData.value = [...taskStore.tasks];
  }
});

watch(
  () => taskStore.tasks,
  (newTasks) => {
    rowData.value = [...newTasks];
    console.log("refreshh", newTasks);
  },
  { deep: true }
);
console.log(rowData, "uiuiuiuiuiumain");

const handleAddTask = () => {
  router.push({ name: "AddTask" });
};

console.log("Row Data:", rowData.value);
console.log("Column Defs:", columnDefs.value);
</script>

<template>
  <div>
    <h3 class="task-list">Task List</h3>

    <div class="task-progress">
      <b-button variant="primary" class="addTask" @click="handleAddTask">
        Add Task
      </b-button>
      <!-- <b-button variant="primary" class="deleteTask"> Delete Task </b-button> -->
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
          :context="{ deletedTask}"
          @grid-ready="onGridReady"
          :components="{ DeleteButtonRenderer }"
        />
        <!-- <a href="#" @click.prevent="selectTask(task)">{{ rowData.value.taskName}}</a> -->
      </div>
    </div>

    <b-modal
      v-model="showDeleteModal"
      title="Confirm Deletion"
      ok-title="Yes"
      cancel-title="No"
      @ok="confirmDelete"
      @cancel="cancelDelete"
    >
      <p>Are you sure you want to delete this task?</p>
    </b-modal>
  </div>
</template>

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

