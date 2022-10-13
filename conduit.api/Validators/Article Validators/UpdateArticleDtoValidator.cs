using conduit.api.Dtos.Article;
using conduit.db.repositories;
using FluentValidation;
namespace conduit.api.Validators.Article_Validators;

public class UpdateArticleDtoValidator : GeneralArticleDtoValidator
{
    public UpdateArticleDtoValidator(IUserRepository userRepository) : base()
    {
        RuleFor(a => (a as UpdateArticleDto).CreatorId)
            .Must(creatorId => userRepository.ContainsId(creatorId));
    }
}
