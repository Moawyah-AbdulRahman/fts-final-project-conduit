using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace conduit.api.Filters;

public class ValidateRequestFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(
                new
                {
                    Error = "Request object is not valid.",
                    Details = context.ModelState.Select(e => $"{e.Key}: {e.Value}")
                });
            return;
        }

        await next();
    }
}