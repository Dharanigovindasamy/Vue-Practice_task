using MediatR;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using TrackTaskEntity.Model;
using GraphQL;

namespace TrackTaskApi.Features.Role.Query
{
    public class GetRoleById
    {
        public class GetRoleByIdRequest : IRequest<Roles>
        {
            public int Id { get; set; }
        }

        public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdRequest, Roles>
        {
            private readonly TaskDbContext _context;

            public GetRoleByIdHandler(TaskDbContext context)
            {
                _context = context;
            }

            public async Task<Roles> Handle(GetRoleByIdRequest request, CancellationToken cancellationToken)
            {
                var role = await _context.Roles.FindAsync(request.Id);
                if (role == null)
                {
                    throw new ExecutionError("Role not found");
                }

                return role;
            }
        }
    }
}
