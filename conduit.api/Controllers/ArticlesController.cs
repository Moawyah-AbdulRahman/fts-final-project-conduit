using AutoMapper;
using conduit.api.Dtos.Article;
using conduit.api.Filters;
using conduit.domain.models;
using conduit.domain.services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace conduit.api.Controllers;

[ConduitExceptionFilter]
[Route("api/articles")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService service;
    private readonly IMapper mapper;

    public ArticlesController(IArticleService articleService, IMapper mapper)
    {
        this.service = articleService;
        this.mapper = mapper;
    }

    [HttpGet("{id}")]
    public IActionResult GetSingleArticle([FromRoute] long id)
    {
        return Ok(mapper.Map<ArticleDto>(service.GetArticle(id)));
    }

    [HttpGet]
    public IActionResult GetMultipleArticles([FromQuery] int page = 1, [FromQuery] int size = 50)
    {
        return Ok(mapper.Map<IEnumerable<ArticleDto>>(service.GetArticles(page, size)));
    }

    [HttpPost("/api/users/{userId}/articles")]
    public IActionResult CreateArticle([FromRoute] long userId, [FromBody] CreateArticleDto createArticleDto)
    {
        var article = mapper.Map<Article>(createArticleDto);
        article.CreatorId = userId;

        service.CreateArticle(article);

        return Created($"api/articles/{article.Id}", null);
    }

    [HttpPut("{articleId}")]
    public IActionResult UpdateArticle([FromRoute] long articleId, [FromBody] UpdateArticleDto updateArticleDto)
    {
        var article = mapper.Map<Article>(updateArticleDto);
        article.Id = articleId;

        service.UpdateArticle(article);

        return Ok($"api/articles/{article.Id}");
    }

    [HttpDelete("{articleId}")]
    public IActionResult DeleteArticle([FromRoute] long articleId)
    {
        service.DeleteArticle(articleId);
        return Ok();
    }
}