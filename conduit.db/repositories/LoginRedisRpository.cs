using conduit.domain.repositories;
using StackExchange.Redis;

namespace conduit.infrastructure.repositories;

public class LoginRedisRpository : ILoginRepository
{
    //inside the redis db we save: userId: loggedInTokenId
    private readonly IDatabase redisDb;

    public LoginRedisRpository(IConnectionMultiplexer multiplexer)
    {
        redisDb = multiplexer.GetDatabase();
    }

    public bool IsLoggedIn(long userId, long tokenId)
    {
        return GetInMemoryTokenId(userId) == tokenId;
    }

    public long Login(long userId)
    {
        return GetInMemoryTokenId(userId);
    }

    private long GetInMemoryTokenId(long userId)
    {
        if (!redisDb.StringGet(userId.ToString()).TryParse(out long tokenId))
        {
            tokenId = 0;
        }
        return tokenId;
    }

    public void Logout(long userId)
    {
        redisDb.StringIncrement(userId.ToString());
    }
}
