using AutoMapper;
using conduit.db.models;
using conduit.domain.models;
using conduit.domain.repositories;

namespace conduit.db.repositories;

public class CommentDbRepository : ICommentRepository
{
    private readonly ConduitDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IArticleRepository articleRepository;

    public CommentDbRepository(ConduitDbContext dbContext, IMapper mapper, 
        IArticleRepository articleRepository)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.articleRepository = articleRepository;
    }

    public bool ContainsId(long id)
    {
        return dbContext.Comments.Any(c => c.Id == id);
    }

    public void Create(Comment comment)
    {
        var commentEntity = mapper.Map<CommentEntity>(comment);
        dbContext.Comments.Add(commentEntity);
        dbContext.SaveChanges();
        comment.Id = commentEntity.Id;
    }

    public void Delete(long id)
    {
        dbContext.Comments.Remove(new CommentEntity { Id = id});
        dbContext.SaveChanges();
    }

    public Comment? ReadById(long id)
    {
        return mapper.Map<Comment>(dbContext.Comments.FirstOrDefault(c => c.Id == id));
    }

    public IEnumerable<Comment>? ReadMultiple(long articleId, int page, int size)
    {
        if (!articleRepository.ContainsId(articleId))
        {
            return null;
        }

        return mapper.Map<IEnumerable<Comment>?>(
                dbContext.Comments
                .Where(c => c.ArticleId == articleId)
                .OrderBy(c => c.Id)
                .Skip((page-1)*size)
                .Take(size)
                .ToList()
            );
    }
}