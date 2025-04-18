using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackTaskApi.Features.User.Queries;
using TrackTaskEntity.Model;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using GraphQL;

namespace TrackTaskApi.Features.User
{
    [Route("api/user")]
    [ApiController]
    public class UserQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var request = new GetUserById.GetUserByIdRequest { UserId = id };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var request = new GetUsers.GetUsersRequest();
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("getAllUsersByRole/{roleId}")]
        public async Task<IActionResult> GetAllUsersByRole(int roleId)
        {
            var request = new GetUsersByRole.GetUsersByRoleRequest { RoleId = roleId };
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
