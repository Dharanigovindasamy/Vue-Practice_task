using MediatR;
using Microsoft.EntityFrameworkCore;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using TrackTaskEntity.Model;
using GraphQL;

namespace TrackTaskApi.Features.Project.Query
{
    public class GetProjects
    {
        public class getProjectsRequest : IRequest<List<Projects>>
        {

        }
        public class getProjectsHandler : IRequestHandler<getProjectsRequest, List<Projects>>
        {
            private readonly TaskDbContext _context;

            public getProjectsHandler(TaskDbContext context)
            {
                _context = context;
            }

            public async Task<List<Projects>> Handle(getProjectsRequest request, CancellationToken cancellationToken)
            {
                var projects = await _context.Projects.ToListAsync(cancellationToken);
                return projects;
            }
        }
    }
}
