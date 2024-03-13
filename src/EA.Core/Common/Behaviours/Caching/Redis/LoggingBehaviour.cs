using System.Diagnostics;
using EA.Core.Caching.Redis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class PerformanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceMiddleware> _logger;

    public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task Invoke(HttpContext context, IRedisClient redisCache)
    {
        var sw = Stopwatch.StartNew();

        // execute the request pipeline
        await _next(context);

        sw.Stop();
        var elapsedTime = sw.Elapsed;

        // log the elapsed time for each Redis cache operation
        _logger.LogInformation($"elapsed time: {elapsedTime}");
    }
}