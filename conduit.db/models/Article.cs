namespace conduit.db.models;

public class Article
{
    public long Id { get; set; }

    public string Title { get; set; } = "";

    public string Content { get; set; } = "";

    public long CreatorId { get; set; }


    public User? Creator { get; set; }

    public ICollection<Comment>? Comments { get; set; }

    public ICollection<User>? FavouritingUsers { get; set; }
}
