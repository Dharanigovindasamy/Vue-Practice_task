using MediatR;
using Microsoft.EntityFrameworkCore;
using TrackTaskEntity.Model;
using TrackTaskApi.Data;
using TrackTaskEntity.Configuration;
using GraphQL;

namespace TrackTaskApi.Features.Task.Query
{
    public class GetTasks
    {

        public class getTasksRequest : IRequest<List<TaskItems>>
        {
            public int? ProjectId { get; set; }
            public int? UserId { get; set; }
        }

        public class getTasksHandler : IRequestHandler<getTasksRequest, List<TaskItems>>
        {
            private readonly Data.TaskDbContext _context;

            public getTasksHandler(Data.TaskDbContext context)
            {
                _context = context;
            }

            public async Task<List<TaskItems>> Handle(getTasksRequest request, CancellationToken cancellationToken)
            {
                var query = _context.TaskItems.AsQueryable();

                //if (request.ProjectId.HasValue)
                //{
                //    query = query.Where(t => t.ProjectId == request.ProjectId.Value);
                //}

                if (request.UserId.HasValue)
                {
                    query = query.Where(t => t.UserId == request.UserId.Value);
                }

                return await query.ToListAsync(cancellationToken);
            }
        }
    }
}
