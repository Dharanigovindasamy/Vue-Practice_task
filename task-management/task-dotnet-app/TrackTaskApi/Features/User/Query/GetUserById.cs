using MediatR;
using Microsoft.EntityFrameworkCore;
using TrackTaskEntity.Model;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using GraphQL;

namespace TrackTaskApi.Features.User.Queries
{
    public class GetUserById
    {
        public class GetUserByIdRequest : IRequest<Users>
        {
            public int UserId { get; set; }
        }

        public class Handler : IRequestHandler<GetUserByIdRequest, Users>
        {
            private readonly TaskDbContext _context;

            public Handler(TaskDbContext context)
            {
                _context = context;
            }

            public async Task<Users> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .Include(u => u.Project)
                    .FirstOrDefaultAsync(u => u.userId == request.UserId, cancellationToken);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                return user;
            }
        }
    }
}
