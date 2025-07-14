using System.Threading.Tasks;
using LibraryApp.Api.Models;
using LibraryApp.Application.Users.Activate;
using LibraryApp.Application.Users.Deactivate;
using LibraryApp.Application.Users.Get;
using LibraryApp.Application.Users.GetById;
using LibraryApp.Application.Users.UpdateRole;
using LibraryApp.Domain.Common.Models;
using LibraryApp.Domain.Users;
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
    public async Task<IActionResult> UpdateUserStatus(Guid id, [FromBody] bool isActive)
    {
        //implement update user status (active / inactive)
        Result result;
        if (isActive)
        {
            result = await _mediator.Send(new ActivateUserCommand(id));
        }
        else
        {
            result = await _mediator.Send(new DeactivateUserCommand(id));
        }

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
        return NoContent();
    }

    [HttpPut("{id}/role")] // admin
    public async Task<IActionResult> UpdateUserRole(Guid id, RoleEnum role)
    {
        //implement update user role   
        var result = await _mediator.Send(new UpdateUserRoleCommand(id, role));
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
        return NoContent();
    }
}