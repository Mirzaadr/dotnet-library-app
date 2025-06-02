
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class ErrorController : ControllerBase
{
    [Route("error")]
    [HttpGet]
    public IActionResult HandleError()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error;

        // Customize response based on exception type
        int statusCode = 500;
        string message = "An unexpected error occurred.";

        switch (exception)
        {
            case ArgumentException:
                statusCode = 400;
                message = exception.Message;
                break;
            case UnauthorizedAccessException:
                statusCode = 401;
                message = "Unauthorized";
                break;
        }

        return Problem(
            statusCode: statusCode,
            title: message,
            detail: exception?.Message
        );
    }
}