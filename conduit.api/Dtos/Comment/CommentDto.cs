namespace conduit.api.Dtos.Comment;

public class CommentDto : GeneralCommentDto
{
    public long Id { get; set; }

    public long ArticleId { get; set; }
}