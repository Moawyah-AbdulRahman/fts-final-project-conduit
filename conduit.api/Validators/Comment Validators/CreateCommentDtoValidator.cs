using conduit.api.Dtos.Comment;
using FluentValidation;

namespace conduit.api.Validators.Comment_Validators;

public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
{
    public CreateCommentDtoValidator()
    {
        RuleFor(c => c.Content)
            .NotNull()
            .NotEmpty();
    }
}
