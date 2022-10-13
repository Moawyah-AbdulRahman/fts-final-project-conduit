namespace conduit.api.Dtos.Article;

public class ArticleDto : GeneralArticleDto
{
    public long Id { get; set; }

    public long CreatorId { get; set; }

    public IEnumerable<long> CommentsIds { get; set; } = Enumerable.Empty<long>();

    public IEnumerable<long> FavouritingUsersIds { get; set; } = Enumerable.Empty<long>();
}