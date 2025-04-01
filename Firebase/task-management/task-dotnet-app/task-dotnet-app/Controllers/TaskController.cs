﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using task_dotnet_app.Data.Model;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem { taskId = 54321, taskName = "Design UI", category = "Development", status = "Pending" },
            new TaskItem { taskId = 54322, taskName = "Write API", category = "Backend", status = "In Progress" },
            new TaskItem { taskId = 54323, taskName = "Testing", category = "QA", status = "Completed" }
        };

        [HttpGet]
        public ActionResult<List<TaskItem>> GetTasks()
        {
            return Ok(tasks);
        }

        [HttpPost("AddTask")]
        public ActionResult<TaskItem> CreateTask([FromBody] TaskItem task)
        {
            if (task == null)
            {
                return BadRequest("Invalid task data");
            }

            // Add the task to the in-memory list
            tasks.Add(task);

            // Log the task name
            Console.WriteLine(task.taskName);

            // Return the created response with the task data
            return CreatedAtAction(nameof(CreateTask), new { id = task.taskId }, task);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.taskId == id);
            if (task == null)
            {
                return NotFound("Task not found");
            }

            tasks.Remove(task);
            return NoContent();
        }
    }


    //[HttpPut("{id}")]
    //public ActionResult<TaskItem> UpdateTask(int id, TaskItem task)
    //{
    //    return Ok(task);
    //}

    //[HttpDelete("{id}")]
    //public ActionResult DeleteTask(int id)
    //{
    //    return NoContent();
    //}
}



//using Microsoft.AspNetCore.Mvc;
//using task_dotnet_app.Data;
//using task_dotnet_app.Data.Model;

//namespace TaskApi.Controllers
//{
//    [ApiController]
//    [Route("api/tasks")]
//    public class TaskController : ControllerBase
//    {
//        private readonly TaskDbContext _context;

//        public TaskController(TaskDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public ActionResult<List<TaskItem>> GetTasks()
//        {
//            Console.WriteLine("GetTasks");
//            return Ok(_context.TaskItems.ToList()); 
//        }

//        [HttpPost("addTask")]
//        public ActionResult<TaskItem> CreateTask([FromBody] TaskItem task)
//        {
//            if (task == null)
//            {
//                return BadRequest("Invalid task data");
//            }

//            _context.TaskItems.Add(task);
//            _context.SaveChanges(); 

//            return CreatedAtAction(nameof(CreateTask), new { id = task.taskId }, task);
//        }
//    }
//}

