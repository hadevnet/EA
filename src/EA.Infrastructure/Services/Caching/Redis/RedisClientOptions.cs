using StackExchange.Redis;

namespace EA.Infrastructure.Services.Caching.Redis;

public class RedisClientOptions
{
    public RedisClientOptions(string connectionString)
    {
        ConnectionString = connectionString;
        _connectionMultiplexerLazy = new Lazy<IConnectionMultiplexer>(() => StackExchange.Redis.ConnectionMultiplexer.Connect($"{connectionString},allowAdmin=true"));
    }
    public bool Enable { get; set; }
    public TimeSpan? DefaultCacheTimeout { get; set; }
    public string? ConnectionString { get; set; }
    private Lazy<IConnectionMultiplexer> _connectionMultiplexerLazy;
    public IConnectionMultiplexer ConnectionMultiplexer => _connectionMultiplexerLazy.Value;
}