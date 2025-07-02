using MediatR;
using Microsoft.AspNetCore.Mvc;
using task_dotnet_app.Data;
using task_dotnet_app.Data.Model;
using task_dotnet_app.Features.User.Mutations;

namespace task_dotnet_app.Features.User
{
    [Route("api/user")]
    [ApiController]
    public class UserMutation(IMediator mediator, TaskDbContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser(CreateUser.CreateUserRequest request)
        {
            var result = await mediator.Send(request);
            if (result == null)
            {
                return NotFound("User not created");
            }
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> UpdateUser(UpdateUser request)
        {
            var result = await mediator.Send(request);
            if (result == null)
            {
                return NotFound("User not updated");
            }
            return Ok((Users)result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var request = new DeleteUser.DeleteUserRequest { UserId = id };
            var result = await mediator.Send(request);
            if (!result)
            {
                return NotFound("User not found");
            }
            return Ok(result);
        }

    //     [HttpPost("bulk-insert-users")]
    //     public async Task<IActionResult> BulkInsertUsers()
    //     {
    //         var stopwatch = System.Diagnostics.Stopwatch.StartNew();
    //         var users = new List<Users>();
    //         for (int i = 0; i < 1000; i++)
    //         {
    //             users.Add(new Users
    //             {
    //                 userName = $"User_{i}",
    //                 email = $"user_{i}@example.com"
    //             });
    //         }
    //         context.Users.AddRange(users);
    //         await context.SaveChangesAsync();
    //         stopwatch.Stop();
    //         return Ok(new { Message = "Inserted 1000 users", ElapsedMilliseconds = stopwatch.ElapsedMilliseconds });
    //     }
    }
}
