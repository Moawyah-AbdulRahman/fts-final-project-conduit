using conduit.api.Dtos;
using conduit.api.Dtos.User;
using conduit.api.Filters;
using conduit.domain.models;
using conduit.domain.services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace conduit.api.Controllers;

[Route("api")]
[ApiController]
[ConduitExceptionFilter]
public class LoginController : ControllerBase
{
    private readonly ILoginService loginService;

    public LoginController(ILoginService loginService)
    {
        this.loginService = loginService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginUserDto loginUserDto)
    {
        return Ok(
            new TokenDto {
                Token = "Bearer " + 
                        loginService.Login(loginUserDto.Username, loginUserDto.Password).Value 
            }
        );
    }

    [HttpPost("logout")]
    public IActionResult Logout([FromBody] TokenDto token)
    {
        loginService.Logout(new Token(token.Token.Split(" ")[1] ));

        return Ok("Logged out successfully.");
    }
}
