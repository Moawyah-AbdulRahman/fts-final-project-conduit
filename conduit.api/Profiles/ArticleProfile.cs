using AutoMapper;
using conduit.api.Dtos.Article;
using conduit.db.models;

namespace conduit.api.Profiles;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleDto>()
            .ForMember(
                dest => dest.FavouritingUsersIds,
                opt => opt.MapFrom(
                        src => (src.FavouritingUsers ?? Enumerable.Empty<User>()).Select(u => u.Id)
                    )
            )
            .ForMember(
                dest => dest.CommentsIds,
                opt => opt.MapFrom(
                        src => (src.Comments ?? Enumerable.Empty<Comment>()).Select(c => c.Id)
                    )
            );

        CreateMap<CreateArticleDto, Article>();

        CreateMap<UpdateArticleDto, Article>();
    }
}
