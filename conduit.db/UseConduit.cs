using conduit.db.repositories;
using conduit.db;
using conduit.domain.repositories;
using Microsoft.Extensions.DependencyInjection;
using conduit.infrastructure.profiles;
using conduit.domain.services.interfaces;
using conduit.domain.services.implementations;

namespace conduit.infrastructure;

public static class UseConduit
{
    public static IServiceCollection AddConduit(this IServiceCollection services)
    {
        return services
            .AddAutoMapper(typeof(UserEntityProfile))
            .AddAutoMapper(typeof(ArticleEntityProfile))
            .AddAutoMapper(typeof(CommentEntityProfile))

            .AddScoped<ConduitDbContext>()
            .AddScoped<IUserRepository, UserDbRepository>()
            .AddScoped<IArticleRepository, ArticleDbRepository>()
            .AddScoped<ICommentRepository, CommentDbRepository>()

            .AddScoped<IUserService, UserService>()
            .AddScoped<IArticleService, ArticleService>()
            .AddScoped<ICommentService, CommentService>();
    }
}
