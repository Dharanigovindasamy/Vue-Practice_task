using MediatR;
using Microsoft.EntityFrameworkCore;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using TrackTaskEntity.Model;
using GraphQL;


namespace TrackTaskApi.Features.Role.Query
{
    public class GetRoles
    {
        public class GetRolesRequest : IRequest<List<Roles>>
        {
        }

        public class GetRolesHandler : IRequestHandler<GetRolesRequest, List<Roles>>
        {
            private readonly TaskDbContext _context;

            public GetRolesHandler(TaskDbContext context)
            {
                _context = context;
            }

            public async Task<List<Roles>> Handle(GetRolesRequest request, CancellationToken cancellationToken)
            {
                var roles = await _context.Roles.ToListAsync(cancellationToken);
                return roles;
            }
        }
    }
}
