namespace conduit.api.Dtos.Article;

public class ArticleDto : GeneralArticleDto
{
    public long Id { get; set; }

    public long CreatorId { get; set; }

    public string CommentsUrl { get; set; } = "";

    public IEnumerable<string> FavouritingUsersUrls { get; set; } = Enumerable.Empty<string>();
}