using LibraryApp.Api.Models;
using LibraryApp.Application.Users.Login;
using LibraryApp.Application.Users.Register;
using LibraryApp.Domain.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controller;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var query = new RegisterUserCommand(request.FullName, request.Email, request.Password, request.UniversityId);
        var result = await _mediator.Send(query);
        if (result.IsFailure)
        {
            var statusCode = result.Error.Type switch
            {
                ErrorType.Validation or ErrorType.Problem => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(
                statusCode: statusCode,
                title: result.Error.Code,
                detail: result.Error.Description
            );
        }

        return Ok(new { result = result.Value });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var query = new LoginUserCommand(request.Email, request.Password);
        var result = await _mediator.Send(query);
        if (result.IsFailure)
        {
            var statusCode = result.Error.Type switch
            {
                ErrorType.Validation or ErrorType.Problem => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(
                statusCode: statusCode,
                title: result.Error.Code,
                detail: result.Error.Description
            );
        }

        return Ok(new { token = result.Value });
    }
}