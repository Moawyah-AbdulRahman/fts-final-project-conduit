using AutoMapper;
using conduit.api.Dtos.Article;
using conduit.db.models;
using conduit.db.repositories;
using Microsoft.AspNetCore.Mvc;

namespace conduit.api.Controllers;

[Route("api/articles")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IArticleRepository articleRepository;
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public ArticlesController(IArticleRepository articleRepository, IUserRepository userRepository, IMapper mapper)
    {
        this.articleRepository = articleRepository;
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    [HttpGet("{id}")]
    public IActionResult GetArticle([FromRoute] long id)
    {
        var article = articleRepository.ReadSingleArticleIncludeCommentsAndFavourates(id);

        if (article is null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<ArticleDto>(article));
    }

    [HttpGet]
    public IActionResult GetMultipleArticles([FromQuery] int page = 1, [FromQuery] int size = 50)
    {
        size = Math.Max(Math.Min(size, 50), 1);
        page = Math.Max(1, page);

        var articles = articleRepository.ReadArticlesIncludeCommentsAndFavourates(page, size);

        return Ok(mapper.Map<IEnumerable<ArticleDto>>(articles));
    }

    [HttpPost("/api/users/{userId}/articles")]
    public IActionResult CreateArticle([FromRoute] long userId, [FromBody] CreateArticleDto createArticleDto)
    {
        if (!userRepository.ContainsId(userId))
        {
            return NotFound("User id not found.");
        }

        var article = mapper.Map<Article>(createArticleDto);
        article.CreatorId = userId;

        articleRepository.CreateArticle(article);

        return Created($"api/articles/{article.Id}", null);
    }

    [HttpPut("{articleId}")]
    public IActionResult UpdateArticle([FromRoute] long articleId, [FromBody] UpdateArticleDto updateArticleDto)
    {
        if (!userRepository.ContainsId(updateArticleDto.CreatorId))
        {
            return NotFound("User id not found.");
        }

        if (!articleRepository.ContainsId(articleId))
        {
            return NotFound(
                new
                {
                    Error = "Article id not found.",
                    SuggestedSolution = @"Use 'api/users/{userId}/articles' with post method to create an Article."
                });
        }

        var article = mapper.Map<Article>(updateArticleDto);
        article.Id = articleId;

        articleRepository.UpdateArticle(article);

        return Ok($"api/articles/{article.Id}");
    }

    [HttpDelete("{articleId}")]
    public IActionResult DeleteArticle([FromRoute] long articleId)
    {
        if (articleRepository.ContainsId(articleId))
        {
            articleRepository.DeleteArticle(articleId);
        }

        return Ok();
    }
}