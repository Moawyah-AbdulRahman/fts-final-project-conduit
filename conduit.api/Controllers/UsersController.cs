using AutoMapper;
using conduit.api.Dtos;
using conduit.db.repositories;
using Microsoft.AspNetCore.Mvc;

namespace conduit.api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    [HttpGet("{id}")]
    public IActionResult GetSingleUser([FromRoute] long id)
    {
        var userWithArticles = userRepository.ReadUserWithPostedArticles(id);

        if (userWithArticles is null)
        {
            return NotFound();
        }

        var userDto = mapper.Map<UserDto>(userWithArticles);

        return Ok(userDto);
    }

    [HttpGet]
    public IActionResult GetAllUsers([FromQuery] int page = 1, [FromQuery] int size = 50)
    {
        size = Math.Max(Math.Min(size, 50), 0);
        page = Math.Max(1, page);

        var usersWithArticles = userRepository.ReadUsersWithArticles(page, size);

        var userDtos = mapper.Map<IEnumerable<UserDto>>(usersWithArticles);

        return Ok(userDtos);
    }

}
