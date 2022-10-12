using AutoMapper;
using conduit.api.Dtos;
using conduit.db.models;
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
    public IActionResult GetMultipleUsers([FromQuery] int page = 1, [FromQuery] int size = 50)
    {
        size = Math.Max(Math.Min(size, 50), 0);
        page = Math.Max(1, page);

        var usersWithArticles = userRepository.ReadUsersWithArticles(page, size);

        var userDtos = mapper.Map<IEnumerable<UserDto>>(usersWithArticles);

        return Ok(userDtos);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var user = mapper.Map<User>(createUserDto);

        userRepository.CreateUser(user);

        return Created($"api/users/{user.Id}", null);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser([FromRoute] long id, [FromBody] UpdateUserDto updateUserDto)
    {
        var user = mapper.Map<User>(updateUserDto);

        var userInDb = userRepository.GetUser(id);

        if (userInDb is null)
        {
            return NotFound(
                new
                {
                    Error = "Resource not found.",
                    SuggestedSolution = @"Use 'api\users' with post method to create a resource."
                });
        }

        if (userInDb.Username != updateUserDto.Username)
        {
            if (userRepository.ContainsUsername(updateUserDto.Username))
            {
                return BadRequest(
                    new
                    {
                        Error = "New username already in use.",
                        SuggestedSolution = "Try other username."
                    });
            }
        }

        user.Id = id;
        userRepository.UpdateUser(user);

        return Ok($"api/users/{user.Id}");
    }
}
