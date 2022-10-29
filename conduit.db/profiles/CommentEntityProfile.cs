using AutoMapper;
using conduit.db.models;
using conduit.domain.models;

namespace conduit.infrastructure.profiles;

public class CommentEntityProfile : Profile
{
	public CommentEntityProfile()
    {
        CreateMap<Comment, CommentEntity>();

        CreateMap<CommentEntity, Comment>();
    }
}
