using conduit.api.Dtos;
using FluentValidation;

namespace conduit.api.Validators;

public class ModificationUserDtoValidator : AbstractValidator<ModificationUserDto>
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