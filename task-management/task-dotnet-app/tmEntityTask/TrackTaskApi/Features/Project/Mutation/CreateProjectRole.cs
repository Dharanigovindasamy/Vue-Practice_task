﻿using MediatR;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using tmEntityTask.TrackTaskEntity.Model;

namespace TrackTaskApi.Features.Mutation
{
    public class CreateProjectRole
    {
        public class CreateProjectRoleRequest : IRequest<ProjectRole>
        {
            public int ProjectId { get; set; }
            public int RoleId { get; set; }
        }

        public class CreateProjectRoleHandler : IRequestHandler<CreateProjectRoleRequest, ProjectRole>
        {
            private readonly TaskDbContext _context;

            public CreateProjectRoleHandler(TaskDbContext context)
            {
                _context = context;
            }

            public async Task<ProjectRole> Handle(CreateProjectRoleRequest request, CancellationToken cancellationToken)
            {
                var projectRole = new ProjectRole
                {
                    ProjectId = request.ProjectId,
                    RoleId = request.RoleId
                };

                _context.ProjectRoles.Add(projectRole);
                await _context.SaveChangesAsync(cancellationToken);

                return projectRole; 
            }
        }
    }
}
