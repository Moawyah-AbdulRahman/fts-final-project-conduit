namespace conduit.domain.models;

public class User
{
    public long? Id { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public IEnumerable<long> ArticlesIds { get; set; } = Enumerable.Empty<long>();
}
