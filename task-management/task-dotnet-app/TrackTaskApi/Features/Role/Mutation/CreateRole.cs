using TrackTaskEntity.Model;
using MediatR;
using TrackTaskApi.Features;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using TrackTaskEntity.Model;
using GraphQL;


namespace TrackTaskApi.Features.Role.Mutation
{
    public class CreateRole
    {
        public class createRoleRequest : IRequest<Roles>
        {
            public string Name { get; set; }
            public int? ProjectId { get; set; }
            public int userId { get; set; }
            public int taskId { get; set; }

        }

        public class createRoleHandler : IRequestHandler<createRoleRequest, Roles>
        {
            private readonly TaskDbContext _context;

            public createRoleHandler(TaskDbContext context)
            {
                _context = context;
            }

            public async Task<Roles> Handle(createRoleRequest request, CancellationToken cancellationToken)
            {
                var role = new Roles
                {
                    RoleName = request.Name
                };
                   
                _context.Roles.Add(role);
                await _context.SaveChangesAsync(cancellationToken);

                return role;
            }
        }
    }
}
