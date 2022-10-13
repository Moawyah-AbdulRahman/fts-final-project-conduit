using AutoMapper;
using conduit.api.Dtos.Comment;
using conduit.db.models;

namespace conduit.api.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>();

        CreateMap<CreateCommentDto, Comment>();
    }
}
