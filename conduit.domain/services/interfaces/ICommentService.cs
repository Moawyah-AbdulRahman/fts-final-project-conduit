using conduit.domain.models;

namespace conduit.domain.services.interfaces;

public interface ICommentService
{
    void CreateComment(Comment comment);
    void DeleteComment(long id);
    Comment GetComment(long id);
    IEnumerable<Comment> GetComments(long articleId, int page, int size);
}
