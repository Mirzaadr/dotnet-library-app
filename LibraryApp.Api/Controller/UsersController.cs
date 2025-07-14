using LibraryApp.Api.Models;
using LibraryApp.Application.Users.Get;
using LibraryApp.Application.Users.GetById;
using LibraryApp.Domain.Common.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.Controller;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _mediator;

    public UsersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet] // admin
    public async Task<IActionResult> GetAllUsers(int page = 1, int pageSize = 10)
    {
        // implement function to get all user general data
        var result = await _mediator.Send(new GetUsersQuery(page, pageSize, null));
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

    [HttpGet("{id}")] // admin
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
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
      
        return Ok(result.Value.Adapt<UserDetailResponse>());
    }

    [HttpPut("{id}/status")] // admin
    public IActionResult UpdateUserStatus(Guid id, [FromBody] bool isActive)
    {
        //TODO: implement update user status (active / inactive)
        return Ok(id);
    }

    [HttpPut("{id}/role")] // admin
    public IActionResult UpdateUserRole(Guid id)
    {
        //TODO: implement update user role      
        return Ok(id);
    }
}