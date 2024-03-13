using EA.Core.Caching.Redis;
using EA.Core.Logging.Serilog;
using EA.Core.MessageBroker.RabbitMQ;
using EA.Infrastructure.Services.Caching.Redis;
using EA.Infrastructure.Services.Logging.Serilog;
using EA.Infrastructure.Services.MessageBroker.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;


namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Redis Options
        // services.AddOptions<RedisClientOptions>()
        // .Configure<IConfiguration>((options, configuration) =>
        // {
        //     configuration.GetSection("RedisClientOptions").Bind(options);
        // });

        // services.AddScoped(sp => sp.GetRequiredService<IOptions<RedisClientOptions>>().Value);

        services.AddDistributedMemoryCache(options => {
            configuration.GetSection("RedisClientOptions").Bind(options);
        });
        services.AddScoped<IRedisClient, RedisClient>();

        // Add RabbitMQ Options
        services.AddOptions<RabbitMQOptions>()
        .Configure<IConfiguration>((options, configuration) =>
        {
            configuration.GetSection("RabbitMQOptions").Bind(options);
        });
        services.AddScoped<IRabbitMQClient, RabbitMQClient>();

        // Add Serilog logging
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(dispose: true);
        });
        services.AddScoped<ISerilogClient, SerilogClient>();


        return services;
    }
}
