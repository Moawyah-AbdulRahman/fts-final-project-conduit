﻿using AutoMapper;
using conduit.api.Dtos;
using conduit.db.models;

namespace conduit.api.Profiles;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<User, UserDto>()
			.ForMember(
				dest => dest.ArticlesUrls,
				opt => opt.MapFrom(
					src => (src.PostedArticles ?? Enumerable.Empty<Article>())
							.Select(article => $"api/articles/{article.Id}")
				)
			);
	}
}