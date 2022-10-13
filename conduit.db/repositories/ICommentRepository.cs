using conduit.db.models;

namespace conduit.db.repositories;

public interface ICommentRepository
{
    bool ContainsId(long id);
    void CreateComment(Comment comment);
    void DeleteComment(long id);
    IEnumerable<Comment> ReadArticleComments(long articleId, int page, int size);
    Comment? ReadSingleComment(long id);
}
