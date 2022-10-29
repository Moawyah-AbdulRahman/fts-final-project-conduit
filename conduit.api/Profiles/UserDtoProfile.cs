using AutoMapper;
using conduit.api.Dtos.User;
using conduit.domain.models;

namespace conduit.api.Profiles;

public class UserDtoProfile : Profile
{
    public UserDtoProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(
                dest => dest.ArticlesUrls,
                opt => opt.MapFrom(
                    src => src.ArticlesIds.Select(articleId => $"/api/articles/{articleId}")
                )
            );

        CreateMap<ModificationUserDto, User>();
    }
}