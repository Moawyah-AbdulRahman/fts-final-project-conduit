namespace conduit.db.models;

public class ArticleEntity
{
    public long Id { get; set; }

    public string Title { get; set; } = "";

    public string Content { get; set; } = "";

    public long CreatorId { get; set; }


    public UserEntity? Creator { get; set; }

    public ICollection<CommentEntity>? Comments { get; set; }

    public ICollection<UserEntity>? FavouritingUsers { get; set; }
}
