namespace conduit.db.models;

public class UserEntity
{
    public long Id { get; set; }

    public string Username { get; set; } = "";

    public string Password { get; set; } = "";


    public ICollection<ArticleEntity>? PostedArticles { get; set; }

    public ICollection<ArticleEntity>? FavoriteArticles { get; set; }

    public ICollection<CommentEntity>? WrittenComments { get; set; }

    public ICollection<UserEntity>? FollowedUsers { get; set; }

    public ICollection<UserEntity>? Followers { get; set; }
}
