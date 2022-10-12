namespace conduit.db.models;

public class Comment
{
    public long Id { get; set; }

    public string Content { get; set; } = "";

    public long ArticleId { get; set; }

    public long WriterId { get; set; }


    public Article? Article { get; set; }

    public User? Writer { get; set; }
}
