using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using task_dotnet_app.Data;
using task_dotnet_app.Data.Model;

namespace task_dotnet_app.Features.User
{
    [ApiController]
    [Route("api/user/sp")]
    public class UserStoredProcedureController : ControllerBase
    {
        private readonly TaskDbContext _context;
        public UserStoredProcedureController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpPost("insert")] // POST api/user/sp/insert
        public IActionResult InsertUserViaSP([FromBody] Users user)
        {
            using (var connection = new NpgsqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("CALL insert_user(@userName, @email, @projectId)", connection))
                {
                    command.Parameters.AddWithValue("userName", user.userName);
                    command.Parameters.AddWithValue("email", user.email);
                    command.Parameters.AddWithValue("projectId", user.ProjectId ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
            return Ok("User inserted via stored procedure.");
        }

        [HttpGet("by-email")] // GET api/user/sp/by-email?email=...
        public ActionResult<List<Users>> GetUsersByEmail([FromQuery] string email)
        {
            var users = _context.Users
                .FromSqlRaw("SELECT * FROM get_users_by_email({0})", email)
                .ToList();
            return Ok(users);
        }
    }
} 