using conduit.domain.models;

namespace conduit.domain.repositories;

public interface ICommentRepository
{
    bool ContainsId(long id);
    void Create(Comment comment);
    void Delete(long id);
    Comment? ReadById(long id);
    IEnumerable<Comment>? ReadMultiple(long articleId, int page, int size);
}
