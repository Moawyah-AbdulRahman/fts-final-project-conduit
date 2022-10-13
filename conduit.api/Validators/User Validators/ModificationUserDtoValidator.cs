using conduit.api.Dtos.User;
using FluentValidation;

namespace conduit.api.Validators;

public abstract class ModificationUserDtoValidator : AbstractValidator<ModificationUserDto>
{
    public ModificationUserDtoValidator()
    {
        RuleFor(u => u.Username)
            .NotNull()
            .Must(n => n.Length >= 3);

        RuleFor(u => u.Password)
            .NotNull()
            .Must(p => p.Length >= 8);
    }
}