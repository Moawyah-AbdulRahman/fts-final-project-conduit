using conduit.db.models;

namespace conduit.db.repositories;

public class CommentDbRepository : ICommentRepository
{
    private readonly ConduitDbContext dbContext;

    public CommentDbRepository(ConduitDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public bool ContainsId(long id)
    {
        return dbContext.Comments.Any(c => c.Id == id);
    }

    public void CreateComment(Comment comment)
    {
        dbContext.Comments.Add(comment);
        dbContext.SaveChanges();
    }

    public void DeleteComment(long id)
    {
        dbContext.Comments.Remove(new Comment { Id = id });
        dbContext.SaveChanges();
    }

    public IEnumerable<Comment> ReadArticleComments(long articleId, int page, int size)
    {
        return dbContext.Comments
            .Where(c => c.ArticleId == articleId)
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();
    }

    public Comment? ReadSingleComment(long id)
    {
        return dbContext.Comments.FirstOrDefault(c => c.Id == id);
    }

}