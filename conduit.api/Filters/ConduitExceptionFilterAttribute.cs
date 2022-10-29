using conduit.domain.exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace conduit.api.Filters;

public class ConduitExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        
        if(exception is null)
        {
            return;
        }

        var conduitException = exception as ConduitException;

        if (conduitException is ConduitException)
        {
            var response = new ObjectResult(new { Massage = conduitException.Massage });
            response.StatusCode = (int)conduitException.ResponseCode;

            context.Result = response;

        }
        else
        {
            base.OnException(context);
        }
    }
}
