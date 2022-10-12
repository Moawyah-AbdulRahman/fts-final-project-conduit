namespace conduit.api.Dtos;

public abstract class ModificationUserDto : GeneralUserDto
{
    public string Password { get; set; } = "";
}
