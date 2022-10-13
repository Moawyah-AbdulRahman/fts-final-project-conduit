using conduit.db.models;

namespace conduit.db.repositories;

public interface IArticleRepository
{
    bool ContainsId(long id);
    void CreateArticle(Article article);
    void DeleteArticle(long id);
    IEnumerable<Article> ReadArticlesIncludeCommentsAndFavourates(int page, int size);
    Article? ReadSingleArticleIncludeCommentsAndFavourates(long id);
    void UpdateArticle(Article article);
}
