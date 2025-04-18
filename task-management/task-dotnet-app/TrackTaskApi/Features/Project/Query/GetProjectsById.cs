using MediatR;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using TrackTaskEntity.Model;
using GraphQL;

namespace TrackTaskApi.Features.Project.Query
{
    public class GetProjectsById
    {
        public class getProjectsByIdRequest : IRequest<Projects>
        {
            public int Id { get; set; }
        }

        public class getProjectsByIdHandler : IRequestHandler<getProjectsByIdRequest, Projects>
        {
            private readonly TaskDbContext _context;

            public getProjectsByIdHandler(TaskDbContext context)
            {
                _context = context;
            }

            public async Task<Projects> Handle(getProjectsByIdRequest request, CancellationToken cancellationToken)
            {
                var project = await _context.Projects.FindAsync(request.Id);
                if (project == null)
                {
                    throw new ExecutionError("Project not found");
                }

                return project;
            }
        }
    }
}
