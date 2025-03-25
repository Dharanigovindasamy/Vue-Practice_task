//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using task_dotnet_app.Data.Model;

//namespace TaskApi.Controllers
//{
//    [ApiController]
//    [Route("api/tasks")]
//    public class TaskController : ControllerBase
//    {
//        [HttpGet]
//        public ActionResult<List<TaskItem>> GetTasks()
//        {
//            var tasks = new List<TaskItem>
//            {
//                new TaskItem { taskId = 1, taskName = "Design UI", category = "Development", status = "Pending" },
//                new TaskItem { taskId = 2, taskName = "Write API", category = "Backend", status = "In Progress" },
//                new TaskItem { taskId = 3, taskName = "Testing", category = "QA", status = "Completed" }
//            };

//            return Ok(tasks);
//        }

//        [HttpPost]
//        [Route("/AddTask")]
//        public ActionResult<TaskItem> CreateTask([FromBody] TaskItem task)
//        {
//            if (task == null)
//            {
//                return BadRequest("Invalid task data");
//            }

//            var taskData = new TaskItem
//            {
//                taskId = task.taskId,
//                taskName = task.taskName,
//                category = task.category,
//                status = task.status
//            };
//            task.add(taskData);
//            Console.WriteLine(taskData.taskName);
//            return Created("task created", taskData);
//        }



//        //[HttpPut("{id}")]
//        //public ActionResult<TaskItem> UpdateTask(int id, TaskItem task)
//        //{
//        //    return Ok(task);
//        //}

//        //[HttpDelete("{id}")]
//        //public ActionResult DeleteTask(int id)
//        //{
//        //    return NoContent();
//        //}
//    }


//}

using Microsoft.AspNetCore.Mvc;
using task_dotnet_app.Data;
using task_dotnet_app.Data.Model;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TaskController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<TaskItem>> GetTasks()
        {
            return Ok(_context.TaskItems.ToList()); 
        }

        [HttpPost("addTask")]
        public ActionResult<TaskItem> CreateTask([FromBody] TaskItem task)
        {
            if (task == null)
            {
                return BadRequest("Invalid task data");
            }

            _context.TaskItems.Add(task);
            _context.SaveChanges(); 

            return CreatedAtAction(nameof(CreateTask), new { id = task.taskId }, task);
        }
    }
}

