using StackExchange.Redis;
public static class RedisScripts
{
    public static string RemoveIfEqualScript { get; } = @"
        local key = KEYS[1]
        local value = ARGV[1]
        
        if redis.call('get', key) == value then
            return redis.call('del', key)
        else
            return 0
        end";

    public static RedisResult RemoveIfEqual(IDatabase redis, RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        var result = (long)redis.ScriptEvaluate(RemoveIfEqualScript, new[] { key }, new[] { value }, flags);
        return result == 1 ? RedisResult.Create(1, ResultType.Integer) : RedisResult.Create(0, ResultType.Integer);
    }

    // ReplaceIfEqual script code goes here.
}