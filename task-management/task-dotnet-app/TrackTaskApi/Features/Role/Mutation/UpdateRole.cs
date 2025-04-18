using MediatR;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using TrackTaskEntity.Model;
using GraphQL;
using GraphQL;

namespace TrackTaskApi.Features.Role.Mutation
{
    public class UpdateRole : IRequest<Roles>
    {
        public class UpdateRoleRequest : IRequest<Roles>
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class UpdateRoleHandler : IRequestHandler<UpdateRoleRequest, Roles>
        {
            private readonly TaskDbContext _context;

            public UpdateRoleHandler(TaskDbContext context)
            {
                _context = context;
            }

            public async Task<Roles> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
            {
                var role = await _context.Roles.FindAsync(request.Id);
                if (role == null)
                {
                    throw new ExecutionError("Role not found");
                }

                role.RoleName = request.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return role;
            }
        }
    }
   
}
