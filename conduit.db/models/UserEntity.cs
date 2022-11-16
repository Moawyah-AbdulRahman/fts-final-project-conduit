using System.Security.Cryptography;

namespace conduit.db.models;

public class UserEntity
{
    public long Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public ICollection<ArticleEntity>? PostedArticles { get; set; }

    public ICollection<ArticleEntity>? FavoriteArticles { get; set; }

    public ICollection<CommentEntity>? WrittenComments { get; set; }

    public ICollection<UserEntity>? FollowedUsers { get; set; }

    public ICollection<UserEntity>? Followers { get; set; }
}
