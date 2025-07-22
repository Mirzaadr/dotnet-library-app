using System.Security.Claims;
using LibraryApp.Api.Models;
using LibraryApp.Application.Users.GetById;
using LibraryApp.Application.Users.Login;
using LibraryApp.Application.Users.Refresh;
using LibraryApp.Application.Users.Register;
using LibraryApp.Domain.Common.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controller;

[ApiController]
[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
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

        return Ok(result.Value);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshRequest request)
    {
        var query = new RefreshUserCommand(request.RefreshToken);
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

        return Ok(result.Value);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        //implement get current user based on JWT
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Unauthorized",
                detail: "User identifier not found."
            );
        }
        var query = new GetUserByIdQuery(Guid.Parse(userId));
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
        return Ok(result.Value.Adapt<UserResponse>());
    }
}