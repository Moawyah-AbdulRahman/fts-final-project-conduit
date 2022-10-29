using AutoMapper;
using conduit.api.Dtos.Article;
using conduit.domain.models;

namespace conduit.api.Profiles;

public class ArticleDtoProfile : Profile
{
    public ArticleDtoProfile()
    {
        CreateMap<Article, ArticleDto>()
            .ForMember(
                dest => dest.FavouritingUsersUrls,
                opt => opt.MapFrom(
                        src => src.FavouritingUsersIds.Select(id => $"/api/users/{id}")
                    )
            )
            .ForMember(
                dest => dest.CommentsUrl,
                opt => opt.MapFrom(src => $"/api/articles/{src.Id}/comments")
            );

        CreateMap<CreateArticleDto, Article>();

        CreateMap<UpdateArticleDto, Article>();
    }
}