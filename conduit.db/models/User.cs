namespace conduit.db.models;

public class User
{
    public long Id { get; set; }

    public string Username { get; set; } = "";

    public string Password { get; set; } = "";


    public ICollection<Article>? PostedArticles { get; set; }

    public ICollection<Article>? FavoriteArticles { get; set; }

    public ICollection<Comment>? WrittenComments { get; set; }

    public ICollection<User>? FollowedUsers { get; set; }

    public ICollection<User>? Followers { get; set; }
}
