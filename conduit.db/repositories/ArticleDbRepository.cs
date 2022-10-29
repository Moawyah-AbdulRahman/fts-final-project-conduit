using AutoMapper;
using conduit.db.models;
using conduit.domain.models;
using conduit.domain.repositories;

namespace conduit.db.repositories;

public class ArticleDbRepository : IArticleRepository
{
    private readonly ConduitDbContext dbContext;
    private readonly IMapper mapper;

    public ArticleDbRepository(ConduitDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public bool ContainsId(long value)
    {
        return dbContext.Articles.Any(a => a.Id == value);
    }

    public void Create(Article article)
    {
        var articleEntity = mapper.Map<ArticleEntity>(article);
        dbContext.Articles.Add(articleEntity);
        dbContext.SaveChanges();
        article.Id = articleEntity.Id;
    }

    public void Delete(long articleId)
    {
        dbContext.Articles.Remove(new ArticleEntity { Id = articleId });
        dbContext.SaveChanges();
    }

    public Article? ReadById(long id)
    {
        return mapper.Map<Article?>(dbContext.Articles.FirstOrDefault(a => a.Id == id));
    }

    public IEnumerable<Article> ReadMultiple(int page, int size)
    {
        return mapper.Map<IEnumerable<Article>>(
                dbContext.Articles
                .OrderBy(a=>a.Id)
                .Skip((page-1)*size)
                .Take(size)
                .ToList()
            );
    }

    public void Update(Article article)
    {
        dbContext.Articles.Update(mapper.Map<ArticleEntity>(article));
        dbContext.SaveChanges();
    }
}