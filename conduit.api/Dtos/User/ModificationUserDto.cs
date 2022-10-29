namespace conduit.api.Dtos.User;

public abstract class ModificationUserDto : GeneralUserDto
{
    public string Password { get; set; } = "";
}
