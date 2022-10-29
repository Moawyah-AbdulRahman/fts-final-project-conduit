using AutoMapper;
using conduit.api.Dtos.User;
using conduit.api.Filters;
using conduit.domain.models;
using conduit.domain.services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace conduit.api.Controllers;
[ConduitExceptionFilter]
[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService service;
    private readonly IMapper mapper;

    public UsersController(IUserService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    [HttpGet("{id}")]
    public IActionResult GetSingleUser([FromRoute] long id)
    {
        return Ok(mapper.Map<UserDto>(service.GetUser(id)));
    }

    [HttpGet]
    public IActionResult GetMultipleUsers([FromQuery] int page = 1, [FromQuery] int size = 50)
    {
        return Ok(mapper.Map<IEnumerable<UserDto>>(service.GetUsers(page, size)));
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var user = mapper.Map<User>(createUserDto);

        service.CreateUser(user);

        return Created($"api/users/{user.Id}", null);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser([FromRoute] long id, [FromBody] UpdateUserDto updateUserDto)
    {
        var user = mapper.Map<User>(updateUserDto);
        user.Id = id;

        service.UpdateUser(user);

        return Ok($"api/users/{user.Id}");
    }
}
