namespace conduit.api.Dtos.User;

public class LoginUserDto : GeneralUserDto
{
    public string Password { get; set; } = string.Empty;
}
