using conduit.domain.models;

namespace conduit.domain.repositories;

public interface IArticleRepository
{
    void Create(Article article);
    bool ContainsId(long value);
    void Delete(long articleId);
    Article? ReadById(long id);
    IEnumerable<Article> ReadMultiple(int page, int size);
    void Update(Article article);
}
