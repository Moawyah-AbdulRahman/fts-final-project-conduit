using conduit.domain.models;
using conduit.domain.services.interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Net;

public class CheckLoginMiddleware : IMiddleware
{
    private readonly ILoginService loginService;
    private readonly IEnumerable<PathString> excecludedPaths;

    public CheckLoginMiddleware(ILoginService loginService, IEnumerable<string> excecludedPaths)
    {
        this.loginService = loginService;
        this.excecludedPaths = excecludedPaths.Select(path => new PathString(path));
    }

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if(!context.Request.Headers.TryGetValue("Authorization", out var tokenValue)||
            tokenValue.IsNullOrEmpty())
        {
            if (excecludedPaths.Contains(context.Request.Path))
            {
                return next(context);
            }
            
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        }
        tokenValue = tokenValue.ToString().Split(" ")[1];
        if (!loginService.IsValidToken(new Token(tokenValue)))
        {
            context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        }

        return next(context);
    }   
}