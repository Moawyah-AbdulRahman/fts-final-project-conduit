using conduit.db.models;
using Microsoft.EntityFrameworkCore;

namespace conduit.db.repositories;

public class ArticleDbRepository : IArticleRepository
{
    private readonly ConduitDbContext dbContext;

    public ArticleDbRepository(ConduitDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public bool ContainsId(long id)
    {
        return dbContext.Articles.Any(a => a.Id == id);
    }

    public void CreateArticle(Article article)
    {
        dbContext.Add(article);
        dbContext.SaveChanges();
    }

    public void DeleteArticle(long id)
    {
        dbContext.Articles.Remove(new Article { Id = id });
        dbContext.SaveChanges();
    }

    public IEnumerable<Article> ReadArticlesIncludeCommentsAndFavourates(int page, int size)
    {
        return dbContext.Articles
            .Include(a => a.Comments)
            .Include(a => a.FavouritingUsers)
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();
    }

    public Article? ReadSingleArticleIncludeCommentsAndFavourates(long id)
    {
        return dbContext.Articles
            .Include(a => a.Comments)
            .Include(a => a.FavouritingUsers)
            .FirstOrDefault(a => a.Id == id);
    }

    public void UpdateArticle(Article article)
    {
        dbContext.Update(article);
        dbContext.SaveChanges();
    }
}