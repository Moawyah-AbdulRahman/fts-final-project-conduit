namespace conduit.api.Dtos;

public class UserDto : GeneralUserDto
{
    public IEnumerable<string> ArticlesUrls { get; set; } = Enumerable.Empty<string>();
}
