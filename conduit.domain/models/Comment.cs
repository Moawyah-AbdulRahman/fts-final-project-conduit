namespace conduit.domain.models;

public class Comment
{
    public long? Id { get; set; }

    public long ArticleId { get; set; }

    public string Content { get; set; } = "";

    public long WriterId { get; set; }
}
