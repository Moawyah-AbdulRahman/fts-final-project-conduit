using conduit.api.Dtos.Article;
using FluentValidation;

namespace conduit.api.Validators.Article_Validators;

public abstract class GeneralArticleDtoValidator : AbstractValidator<GeneralArticleDto>
{
    public GeneralArticleDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Content)
            .NotNull()
            .NotEmpty();
    }
}
