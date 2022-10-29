using conduit.domain.models;

namespace conduit.domain.services.interfaces;

public interface IArticleService
{
    void CreateArticle(Article article);
    void DeleteArticle(long articleId);
    Article GetArticle(long id);
    IEnumerable<Article> GetArticles(int page, int size);
    void UpdateArticle(Article article);
}
