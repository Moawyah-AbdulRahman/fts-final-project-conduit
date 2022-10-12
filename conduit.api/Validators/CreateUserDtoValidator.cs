using conduit.db.repositories;
using FluentValidation;

namespace conduit.api.Validators;

public class CreateUserDtoValidator : ModificationUserDtoValidator
{
    public CreateUserDtoValidator(IUserRepository userRepository) : base()
    {
        RuleFor(u => u.Username)
            .Must(n => !userRepository.ContainsUsername(n));
    }
}
