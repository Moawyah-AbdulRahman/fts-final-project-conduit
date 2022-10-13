using AutoMapper;
using conduit.api.Dtos.Comment;
using conduit.db.models;
using conduit.db.repositories;
using Microsoft.AspNetCore.Mvc;

namespace conduit.api.Controllers;

[Route("api/articles/{articleId}/comments")]
[ApiController]
public class CommentsController : ControllerBase
{
	private readonly ICommentRepository commentRepository;
	private readonly IArticleRepository articleRepository;
	private readonly IMapper mapper;

	public CommentsController(ICommentRepository commentRepository, IArticleRepository articleRepository,
		IMapper mapper)
	{
		this.commentRepository = commentRepository;
		this.articleRepository = articleRepository;
		this.mapper = mapper;
	}

	[HttpGet("/api/comments/{id}")]
	public IActionResult GetSingleComment([FromRoute] long id)
	{
		var comment = commentRepository.ReadSingleComment(id);

		if (comment is null)
		{
			return NotFound();
		}

		return Ok(mapper.Map<CommentDto>(comment));
	}

	[HttpGet]
	public IActionResult GetArticleComments([FromRoute] long articleId, [FromQuery] int page = 1,
		[FromQuery] int size = 50)
	{
		if (!articleRepository.ContainsId(articleId))
		{
			return NotFound("Article does not exist");
		}

		size = Math.Max(Math.Min(size, 50), 1);
		page = Math.Max(1, page);

		var comments = commentRepository.ReadArticleComments(articleId, page, size);

		return Ok(mapper.Map<IEnumerable<CommentDto>>(comments));
	}

	[HttpPost]
	public IActionResult CreateComment([FromRoute] long articleId, [FromBody] CreateCommentDto createCommentDto)
	{
		if (!articleRepository.ContainsId(articleId))
		{
			return NotFound("Article not found.");
		}
		var comment = mapper.Map<Comment>(createCommentDto);
		comment.ArticleId = articleId;

		commentRepository.CreateComment(comment);

		return Ok($"api/comments/{comment.Id}");
	}

	[HttpDelete("/api/comments/{id}")]
	public IActionResult DeleteComment([FromRoute] long id)
	{
		if (commentRepository.ContainsId(id))
		{
			commentRepository.DeleteComment(id);
		}

		return Ok();
	}
}
