namespace conduit.api.Dtos;

public class UserDto
{
    public string Username { get; set; } = "";


    public IEnumerable<string> ArticlesUrls { get; set; } = Enumerable.Empty<string>();
}
