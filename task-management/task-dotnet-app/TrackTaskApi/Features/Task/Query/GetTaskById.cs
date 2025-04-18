using MediatR;
using TrackTaskEntity.Model;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using GraphQL;

namespace TrackTaskApi.Features.Task.Query
{
    public class GetTaskById
    {
        public class getTaskByIdRequest : IRequest<TaskItems>
        {
            public int Id { get; set; }
        }

        public class getTaskByIdHandler : IRequestHandler<getTaskByIdRequest, TaskItems>
        {
            private readonly Data.TaskDbContext _context;

            public getTaskByIdHandler(Data.TaskDbContext context)
            {
                _context = context;
            }

            public async Task<TaskItems> Handle(getTaskByIdRequest request, CancellationToken cancellationToken)
            {
                var task = await _context.TaskItems.FindAsync(request.Id);
                if (task == null)
                {
                    throw new ExecutionError("Task not found");
                }

                return task;
            }
        }

    }
}
