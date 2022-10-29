using AutoMapper;
using conduit.db.models;
using conduit.domain.models;

namespace conduit.infrastructure.profiles;

public class ArticleEntityProfile : Profile
{
	public ArticleEntityProfile()
	{
		CreateMap<Article, ArticleEntity>();

        CreateMap<ArticleEntity, Article>()
            .ForMember(
                    dest => dest.CommentsIds,
                    opt => opt.MapFrom(src => src.Comments!.Select(c => c.Id))
                )
            .ForMember(
                    dest => dest.FavouritingUsersIds,
                    opt => opt.MapFrom(src => src.FavouritingUsers!.Select(u => u.Id))
                );
    }
}
