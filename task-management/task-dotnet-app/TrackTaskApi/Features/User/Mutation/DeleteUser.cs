using MediatR;
using TrackTaskEntity.Model;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using GraphQL;

namespace TrackTaskApi.Features.User.Mutations
{
    public class DeleteUser
    {
        public class DeleteUserRequest : IRequest<bool>
        {
            public int UserId { get; set; }
        }

        public class Handler : IRequestHandler<DeleteUserRequest, bool>
        {
            private readonly TaskDbContext _context;

            public Handler(TaskDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.UserId);
                if (user == null) return false;

                _context.Users.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
    }
}
