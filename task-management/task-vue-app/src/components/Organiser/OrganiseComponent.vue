<template>
  <div class="task-board">
    <h4 class="board-title">Task Organise</h4>
    <div class="board-columns">
      <div class="board-column">
        <h4 class="column-title pending">Pending</h4>
        <draggable
          class="organised-task-list"
          v-model="pendingTasks"
          group="tasks"
          item-key="taskId"
        >
          <template #item="{ element }">
            <div class="task-card pending-task">
              <h5>{{ element.taskName }}</h5>
              <p>{{ element.category }}</p>
            </div>
          </template>
        </draggable>
      </div>

      <div class="board-column">
        <h4 class="column-title in-progress">In Progress</h4>
        <draggable
          class="organised-task-list"
          v-model="inProgressTasks"
          group="tasks"
          item-key="taskId"
        >
          <template #item="{ element }">
            <div class="task-card in-progress-task">
              <h5>{{ element.taskName }}</h5>
              <p>{{ element.category }}</p>
            </div>
          </template>
        </draggable>
      </div>

      <div class="board-column">
        <h4 class="column-title completed">Completed</h4>
        <draggable
          class="organised-task-list"
          v-model="completedTasks"
          group="tasks"
          item-key="taskId"
        >
          <template #item="{ element }">
            <div class="task-card completed-task">
              <h5>{{ element.taskName }}</h5>
              <p>{{ element.category }}</p>
            </div>
          </template>
        </draggable>
      </div>
    </div>
  </div>
</template>

<script>
import draggable from "vuedraggable";
import { onMounted, ref } from "vue";
import { useTaskStore } from "../../store/taskStore";

export default {
  components: {
    draggable,
  },
  setup() {
    const taskStore = useTaskStore();
    const pendingTasks = ref([]);
    const inProgressTasks = ref([]);
    const completedTasks = ref([]);

    const fetchAndCategorizeTasks = async () => {
      console.log("Fetching tasks...");
      await taskStore.fetchTask();
      console.log("Fetched tasks:", taskStore.tasks);

      taskStore.tasks.forEach((task) =>
        console.log("Task ID:", task.taskId, "Status:", task.status)
      );

      pendingTasks.value = taskStore.tasks.filter(
        (task) => task.status === "Pending"
      );
      inProgressTasks.value = taskStore.tasks.filter(
        (task) => task.status === "In Progress"
      );
      completedTasks.value = taskStore.tasks.filter(
        (task) => task.status === "Completed"
      );

      console.log("Pending Tasks:", pendingTasks.value);
      console.log("In Progress Tasks:", inProgressTasks.value);
      console.log("Completed Tasks:", completedTasks.value);
    };

    onMounted(fetchAndCategorizeTasks);

    return { pendingTasks, inProgressTasks, completedTasks };
  },
};
</script>

<style>
.task-board {
  width: 100%;
  padding: 20px;
  background: #f4f5f7;
}

.board-title {
  text-align: center;
  font-size: 24px;
  font-weight: bold;
  margin-bottom: 20px;
}

.board-columns {
  display: flex;
  justify-content: space-around;
  gap: 20px;
}

.board-column {
  background: white;
  padding: 15px;
  border-radius: 8px;
  width: 30%;
  min-height: 400px;
  box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
}

.column-title {
  text-align: center;
  padding: 10px;
  font-size: 18px;
  font-weight: bold;
  margin-bottom: 10px;
}

.pending {
  color: #d32f2f;
}

.in-progress {
  color: #fbc02d;
}

.completed {
  color: #388e3c;
}

.organised-task-list {
  min-height: 350px;
  background: #e1e4e8;
  padding: 10px;
  border-radius: 5px;
}

.task-card {
  background: white;
  padding: 12px;
  margin-bottom: 10px;
  border-radius: 5px;
  box-shadow: 0px 2px 4px rgba(0, 0, 0, 0.2);
}

.task-card h5 {
  margin: 0;
  font-size: 16px;
}

.task-card p {
  font-size: 14px;
  color: #666;
  margin: 5px 0 0 0;
}

.pending-task {
  border-left: 4px solid #d32f2f;
}

.in-progress-task {
  border-left: 4px solid #fbc02d;
}

.completed-task {
  border-left: 4px solid #388e3c;
}
</style>
