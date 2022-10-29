using conduit.domain.exceptions;
using conduit.domain.models;
using conduit.domain.repositories;
using conduit.domain.services.interfaces;

namespace conduit.domain.services.implementations;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository articleRepository;
    private readonly IUserRepository userRepository;

    public ArticleService(IArticleRepository articleRepository, IUserRepository userRepository)
    {
        this.articleRepository = articleRepository;
        this.userRepository = userRepository;
    }

    public void CreateArticle(Article article)
    {
        if (article.Id is not null)
        {
            throw new ArgumentException("cannot create article that already have an id, try update");
        }

        if (article.CreatorId is null) 
        {
            throw new ArgumentException("cannot create an article without a creator");
        }

        if (!userRepository.ContainsId(article.CreatorId!.Value))
        {
            throw new ConduitException("trying to add an article to a user that does not exist", 
                HttpResponseCode.NotFound);
        }

        articleRepository.Create(article);
    }

    public void DeleteArticle(long articleId)
    {
        if (articleRepository.ContainsId(articleId))
        {
            articleRepository.Delete(articleId);
        }
    }

    public Article GetArticle(long id)
    {
        return articleRepository.ReadById(id) ?? 
            throw new ConduitException("article id does not exist", HttpResponseCode.NotFound);
    }

    public IEnumerable<Article> GetArticles(int page, int size)
    {
        size = Math.Min(50, Math.Max(1, size));
        page = Math.Max(page, 1);

        return articleRepository.ReadMultiple(page, size);
    }

    public void UpdateArticle(Article article)
    {
        if (article.Id is null || !articleRepository.ContainsId(article.Id.Value))
        {
            throw new ConduitException("trying to update an article that does not exists",
                HttpResponseCode.NotFound);
        }

        if(article.CreatorId is null)
        {
            article.CreatorId = articleRepository.ReadById(article.Id.Value)!.CreatorId;
        }

        articleRepository.Update(article);
    }
}
