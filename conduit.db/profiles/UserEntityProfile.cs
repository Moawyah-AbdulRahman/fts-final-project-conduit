using AutoMapper;
using conduit.db.models;
using conduit.domain.models;

namespace conduit.infrastructure.profiles;

public class UserEntityProfile : Profile
{
	public UserEntityProfile()
	{
		CreateMap<User, UserEntity>();

		CreateMap<UserEntity, User>()
			.ForMember(
				dest => dest.ArticlesIds,
				opt => opt.MapFrom(src => src.PostedArticles!.Select(a => a.Id))
			);
	}
}
