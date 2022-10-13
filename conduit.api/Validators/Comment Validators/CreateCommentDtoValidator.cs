using conduit.api.Dtos.Comment;
using conduit.db.repositories;
using FluentValidation;

namespace conduit.api.Validators.Comment_Validators;

public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
{
    public CreateCommentDtoValidator(IUserRepository userRepository)
    {
        RuleFor(c => c.Content)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.WriterId)
            .Must(writerId => userRepository.ContainsId(writerId));
    }
}
