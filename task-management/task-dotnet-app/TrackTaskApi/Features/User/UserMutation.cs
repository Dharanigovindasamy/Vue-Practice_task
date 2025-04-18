using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using TrackTaskEntity.Model;
using TrackTaskApi.Features.User.Mutations;
using GraphQL;

namespace TrackTaskApi.Features.User
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
    }
}
