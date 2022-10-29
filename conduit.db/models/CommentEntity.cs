namespace conduit.db.models;

public class CommentEntity
{
    public long Id { get; set; }

    public string Content { get; set; } = "";

    public long ArticleId { get; set; }

    public long WriterId { get; set; }


    public ArticleEntity? Article { get; set; }

    public UserEntity? Writer { get; set; }
}
