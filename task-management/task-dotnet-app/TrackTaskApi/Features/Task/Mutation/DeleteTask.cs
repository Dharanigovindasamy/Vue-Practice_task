using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TrackTaskEntity.Model;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using GraphQL;

namespace TrackTaskApi.Features.Task.Mutation
{
    public class DeleteTask
    {
        public class DeleteTaskRequest : IRequest<bool>
        {
            public int Id { get; set; }
        }

        public class deleteTaskHandler : IRequestHandler<DeleteTaskRequest, bool>
        {
            private readonly TaskDbContext _context;

            public deleteTaskHandler(TaskDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
            {
                var task = await _context.TaskItems.FindAsync(request.Id);
                if (task == null)
                {
                    return false;
                }

                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
    }
}
