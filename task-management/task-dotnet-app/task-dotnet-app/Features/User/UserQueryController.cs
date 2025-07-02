using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using task_dotnet_app.Data;
using task_dotnet_app.Data.Model;
using System.Linq;

namespace task_dotnet_app.Features.User
{
    [ApiController]
    [Route("api/user")]
    public class UserQueryController : ControllerBase
    {
        private readonly TaskDbContext _context;
        private readonly IMemoryCache _cache;

        public UserQueryController(TaskDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpPost("bulk-insert-users")]
        public async Task<IActionResult> BulkInsertUsers()
        {
            var stopwatch = Stopwatch.StartNew();
            var users = new List<Users>();
            for (int i = 0; i < 1000; i++)
            {
                users.Add(new Users
                {
                    userName = $"User_{i}",
                    email = $"user_{i}@example.com",
                    ProjectId = 1
                });
            }
            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();
            stopwatch.Stop();
            return Ok(new { Message = "Inserted 1000 users", ElapsedMilliseconds = stopwatch.ElapsedMilliseconds });
        }

        // [HttpGet("getAllUsersWithTiming")]
        // public IActionResult GetAllUsersWithTiming()
        // {
        //     var cacheKey = "GetAllUsers";
        //     long dbFetchTime = 0;
        //     long cacheFetchTime = 0;
        //     List<Users> result;
        //     var stopwatch = new Stopwatch();

        //     // Try to get from cache
        //     stopwatch.Start();
        //     bool fromCache = _cache.TryGetValue(cacheKey, out result);
        //     stopwatch.Stop();
        //     if (fromCache)
        //     {
        //         cacheFetchTime = stopwatch.ElapsedMilliseconds;
        //         return Ok(new { Source = "Cache", FetchTimeMs = cacheFetchTime, ResultCount = result.Count, Users = result });
        //     }

        //     // If not in cache, fetch from DB
        //     stopwatch.Restart();
        //     result = _context.Users.Where(u => u.ProjectId == null).ToList();
        //     stopwatch.Stop();
        //     dbFetchTime = stopwatch.ElapsedMilliseconds;

        //     // Store in cache
        //     var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));
        //     _cache.Set(cacheKey, result, cacheEntryOptions);

        //     // Fetch from cache again to measure cache fetch time
        //     stopwatch.Restart();
        //     _cache.TryGetValue(cacheKey, out var cacheResult);
        //     stopwatch.Stop();
        //     cacheFetchTime = stopwatch.ElapsedMilliseconds;

        //     return Ok(new {
        //         Source = "Database",
        //         DbFetchTimeMs = dbFetchTime,
        //         CacheFetchTimeMs = cacheFetchTime,
        //         ResultCount = result.Count,
        //         TimeDifferenceMs = dbFetchTime - cacheFetchTime,
        //         Users = result
        //     });
        // }

        [HttpGet("getAllUsersWithTiming")]
        public IActionResult GetAllUsersWithTiming()
        {
            var cacheKey = "GetAllUsers";
            long dbFetchTime = 0;
            long cacheFetchTime = 0;
            List<Users> result;
            var stopwatch = new Stopwatch();

            // Try to get from cache
            stopwatch.Start();
            bool fromCache = _cache.TryGetValue(cacheKey, out result);
            stopwatch.Stop();
            if (fromCache)
            {
                cacheFetchTime = stopwatch.ElapsedMilliseconds;
                return Ok(new { Source = "Cache", FetchTimeMs = cacheFetchTime, ResultCount = result, Users = result });
            }

            // If not in cache, fetch from DB
            stopwatch.Restart();
            result = _context.Users.ToList().Where(u => u.ProjectId == null).ToList();
            stopwatch.Stop();
            dbFetchTime = stopwatch.ElapsedMilliseconds;

            // Store in cache
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));
            _cache.Set(cacheKey, result, cacheEntryOptions);

            // Fetch from cache again to measure cache fetch time
            stopwatch.Restart();
            _cache.TryGetValue(cacheKey, out var cacheResult);
            stopwatch.Stop();
            cacheFetchTime = stopwatch.ElapsedMilliseconds;

            return Ok(new {
                Source = "Database",
                DbFetchTimeMs = dbFetchTime,
                CacheFetchTimeMs = cacheFetchTime,
                ResultCount = result,
                TimeDifferenceMs = dbFetchTime - cacheFetchTime,
                Users = result
            });
        }

        [HttpGet("getAllUsersWithTimingText")]
        public IActionResult GetAllUsersWithTimingText()
        {
            var cacheKey = "GetAllUsers";
            long dbFetchTime = 0;
            long cacheFetchTime = 0;
            List<Users> result;
            var stopwatch = new Stopwatch();
            var output = new System.Text.StringBuilder();

            // Try to get from cache
            stopwatch.Start();
            bool fromCache = _cache.TryGetValue(cacheKey, out result);
            stopwatch.Stop();
            if (fromCache)
            {
                cacheFetchTime = stopwatch.ElapsedMilliseconds;
                output.AppendLine($"Fetched from: Cache");
                output.AppendLine($"Time taken: {cacheFetchTime} ms");
                output.AppendLine($"User count: {result.Count}");
                foreach (var user in result)
                {
                    output.AppendLine($"ID: {user.userId}, Name: {user.userName}, Email: {user.email}, ProjectId: {(user.ProjectId.HasValue ? user.ProjectId.ToString() : "null")}");
                }
                return Content(output.ToString());
            }

            // If not in cache, fetch from DB (including users with ProjectId == null)
            stopwatch.Restart();
            result = _context.Users.ToList(); // This fetches ALL users, including those with ProjectId == null
            stopwatch.Stop();
            dbFetchTime = stopwatch.ElapsedMilliseconds;

            // Store in cache
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));
            _cache.Set(cacheKey, result, cacheEntryOptions);

            output.AppendLine($"Fetched from: Database");
            output.AppendLine($"Time taken: {dbFetchTime} ms");
            output.AppendLine($"User count: {result.Count}");
            foreach (var user in result)
            {
                output.AppendLine($"ID: {user.userId}, Name: {user.userName}, Email: {user.email}, ProjectId: {(user.ProjectId.HasValue ? user.ProjectId.ToString() : "null")}");
            }
            return Content(output.ToString());
        }
    }
} 