namespace conduit.api.Dtos.User;

public class UserDto : GeneralUserDto
{
    public long Id { get; set; }

    public IEnumerable<string> ArticlesUrls { get; set; } = Enumerable.Empty<string>();
}
