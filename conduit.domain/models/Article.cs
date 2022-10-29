namespace conduit.domain.models;

public class Article
{
    public long? Id { get; set; }

    public string Title { get; set; } = "";

    public string Content { get; set; } = "";

    public long? CreatorId { get; set; }

    public IEnumerable<long> CommentsIds { get; set; } = Enumerable.Empty<long>();

    public IEnumerable<long> FavouritingUsersIds { get; set; } = Enumerable.Empty<long>();
}
