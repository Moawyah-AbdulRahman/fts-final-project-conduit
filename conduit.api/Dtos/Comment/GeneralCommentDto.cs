namespace conduit.api.Dtos.Comment;

public abstract class GeneralCommentDto
{
    public string Content { get; set; } = "";

    public long WriterId { get; set; }
}