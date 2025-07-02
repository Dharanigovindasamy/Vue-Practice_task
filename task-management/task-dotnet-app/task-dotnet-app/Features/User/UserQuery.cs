// // File intentionally left empty. UserQueryController logic is now only in UserQueryController.cs.

// using MediatR;
// using Microsoft.AspNetCore.Mvc;
// using task_dotnet_app.Features.User.Queries;

// namespace task_dotnet_app.Features.User
// {
//     [Route("api/user")]
//     [ApiController]
//     public class UserQuery: ControllerBase
//     {
//         private readonly IMediator _mediator;

//         public UserQuery(IMediator mediator)
//         {
//             _mediator = mediator;
//         }

//         [HttpGet("get/{id}")]
//         public async Task<IActionResult> GetUserById(int id)
//         {
//             var request = new GetUserById.GetUserByIdRequest { UserId = id };
//             var result = await _mediator.Send(request);
//             return Ok(result);
//         }

//         // [HttpGet("getAllUsers")]
//         // public async Task<IActionResult> GetAllUsers()
//         // {
//         //     var request = new GetUsers.GetUsersRequest();
//         //     var result = await _mediator.Send(request);
//         //     return Ok(result);
//         // }

//          [HttpGet("getAllUsers")]
//         public async Task<IActionResult> GetAllUsers()
//         {
//             var cacheKey = "GetAllUsers";
//             if (!_cache.TryGetValue(cacheKey, out object result))
//             {
//                 var request = new GetUsers.GetUsersRequest();
//                 result = await _mediator.Send(request);
//                 var cacheEntryOptions = new MemoryCacheEntryOptions()
//                     .SetSlidingExpiration(TimeSpan.FromMinutes(1));
//                 _cache.Set(cacheKey, result, cacheEntryOptions);
//             }
//             return Ok(result);
//         }

//         [HttpGet("getAllUsersByRole/{roleId}")]
//         public async Task<IActionResult> GetAllUsersByRole(int roleId)
//         {
//             var request = new GetUsersByRole.GetUsersByRoleRequest { RoleId = roleId };
//             var result = await _mediator.Send(request);
//             return Ok(result);
//         }

        
//     }
// }
