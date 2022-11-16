using conduit.db.repositories;
using conduit.db;
using conduit.domain.repositories;
using Microsoft.Extensions.DependencyInjection;
using conduit.infrastructure.profiles;
using conduit.domain.services.interfaces;
using conduit.domain.services.implementations;
using conduit.infrastructure.repositories;
using StackExchange.Redis;

namespace conduit.infrastructure;

public static class UseConduit
{
    public static IServiceCollection AddConduit(this IServiceCollection services, 
                                                string dbConnectionString, string redisConnectionString,
                                                string tokenSigningKey, int tokenMinutesToLive)
    {
        return services
            .AddAutoMapper(typeof(UserEntityProfile))
            .AddAutoMapper(typeof(ArticleEntityProfile))
            .AddAutoMapper(typeof(CommentEntityProfile))

            .AddScoped(serviceProvider => new ConduitDbContext(dbConnectionString))
            .AddScoped<IConnectionMultiplexer>(serviceProvider => ConnectionMultiplexer.Connect(redisConnectionString))

            .AddScoped<IUserRepository, UserDbRepository>()
            .AddScoped<IArticleRepository, ArticleDbRepository>()
            .AddScoped<ICommentRepository, CommentDbRepository>()
            .AddScoped<ILoginRepository, LoginRedisRpository>()

            .AddScoped<IUserService, UserService>()
            .AddScoped<IArticleService, ArticleService>()
            .AddScoped<ICommentService, CommentService>()
            .AddScoped<ILoginService, JwtLoginService>()
            .AddSingleton<ITokenHandler>(serviceProvider => new JwtTokenHandler(tokenSigningKey, tokenMinutesToLive));
    }
}
