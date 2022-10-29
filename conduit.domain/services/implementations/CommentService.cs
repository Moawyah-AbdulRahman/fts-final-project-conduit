using conduit.domain.exceptions;
using conduit.domain.models;
using conduit.domain.repositories;
using conduit.domain.services.interfaces;

namespace conduit.domain.services.implementations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository commentRepository;
    private readonly IArticleRepository articleRepository;
    private readonly IUserRepository userRepository;

    public CommentService(ICommentRepository commentRepository, 
        IArticleRepository articleRepository, IUserRepository userRepository)
    {
        this.commentRepository = commentRepository;
        this.articleRepository = articleRepository;
        this.userRepository = userRepository;
    }

    public void CreateComment(Comment comment)
    {
        if(comment.Id is not null)
        {
            throw new ArgumentException("cannot create comment that already have an id, try update");
        }

        if (!articleRepository.ContainsId(comment.ArticleId))
        {
            throw new ConduitException("trying to add a comment to an article that does not exist", 
                HttpResponseCode.NotFound);
        }

        if (!userRepository.ContainsId(comment.WriterId)) 
        {
            throw new ConduitException("trying to add a comment with a user that does not exist",
            HttpResponseCode.NotFound);
        }

        commentRepository.Create(comment);
    }

    public void DeleteComment(long id)
    {
        if (commentRepository.ContainsId(id))
        {
            commentRepository.Delete(id);
        }
    }

    public Comment GetComment(long id)
    {
        return commentRepository.ReadById(id) ?? 
            throw new ConduitException("comment id does not exist", HttpResponseCode.NotFound);
    }

    public IEnumerable<Comment> GetComments(long articleId, int page, int size)
    {
        size = Math.Min(50, Math.Max(1, size));
        page = Math.Max(page, 1);

        return commentRepository.ReadMultiple(articleId, page, size) ?? 
            throw new ConduitException("trying to get comment of an article that does not exist", 
            HttpResponseCode.NotFound);
    }
}
