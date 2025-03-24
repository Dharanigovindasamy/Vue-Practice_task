using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using task_dotnet_app.Data.Model;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<TaskItem>> GetTasks()
        {
            var tasks = new List<TaskItem>
            {
                new TaskItem { taskId = 1, taskName = "Design UI", category = "Development", status = "Pending" },
                new TaskItem { taskId = 2, taskName = "Write API", category = "Backend", status = "In Progress" },
                new TaskItem { taskId = 3, taskName = "Testing", category = "QA", status = "Completed" }
            };

            return Ok(tasks);
        }

        [HttpPost]
        public ActionResult<TaskItem> CreateTask(TaskItem task)
        {
            return Created("task created", task);
        }

        [HttpPut("{id}")]
        public ActionResult<TaskItem> UpdateTask(int id, TaskItem task)
        {
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            return NoContent();
        }
    }

  
}
