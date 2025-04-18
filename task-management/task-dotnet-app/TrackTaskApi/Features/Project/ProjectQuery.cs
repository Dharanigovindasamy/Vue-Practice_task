using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackTaskApi.Data;
using TrackTaskApi.Features.Project.Query;
using TrackTaskEntity.Configuration;
using TrackTaskEntity.Model;
using GraphQL;


namespace TrackTaskApi.Features.Project
{
    [Route("api/project")]
    [ApiController]
    public class ProjectQuery : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly TaskDbContext _context;

        public ProjectQuery(IMediator mediator, TaskDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetProjectById([FromRoute] GetProjectsById.getProjectsByIdRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects([FromQuery] GetProjects.getProjectsRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
