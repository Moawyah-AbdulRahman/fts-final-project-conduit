using AutoMapper;
using conduit.api.Dtos.Comment;
using conduit.api.Filters;
using conduit.domain.models;
using conduit.domain.services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace conduit.api.Controllers;

[ConduitExceptionFilter]
[Route("api/articles/{articleId}/comments")]
[ApiController]
[Authorize]
public class CommentsController : ControllerBase
{
	private readonly ICommentService service;
	private readonly IMapper mapper;

	public CommentsController(ICommentService service, IMapper mapper)
	{
		this.service = service;
		this.mapper = mapper;
	}

	[HttpGet("/api/comments/{id}")]
	public IActionResult GetSingleComment([FromRoute] long id)
	{
		return Ok(mapper.Map<CommentDto>(service.GetComment(id)));
	}

	[HttpGet]
	public IActionResult GetArticleComments([FromRoute] long articleId, [FromQuery] int page = 1,
		[FromQuery] int size = 50)
	{
		return Ok(mapper.Map<IEnumerable<CommentDto>>(service.GetComments(articleId, page, size)));
	}

	[HttpPost]
	public IActionResult CreateComment([FromRoute] long articleId, [FromBody] CreateCommentDto createCommentDto)
	{
		var comment = mapper.Map<Comment>(createCommentDto);
		comment.ArticleId = articleId;

		service.CreateComment(comment);

		return Ok($"api/comments/{comment.Id}");
	}

	[HttpDelete("/api/comments/{id}")]
	public IActionResult DeleteComment([FromRoute] long id)
	{
		service.DeleteComment(id);

		return Ok();
	}
}
