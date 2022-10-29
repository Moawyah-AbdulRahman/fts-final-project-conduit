using AutoMapper;
using conduit.api.Dtos.Comment;
using conduit.domain.models;

namespace conduit.api.Profiles;

public class CommentDtoProfile : Profile
{
    public CommentDtoProfile()
    {
        CreateMap<Comment, CommentDto>();

        CreateMap<CreateCommentDto, Comment>();
    }
}
