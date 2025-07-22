using LibraryApp.Domain.Common.Models;

namespace LibraryApp.Domain.Users;

public static class UserErrors
{
  public static Error NotFound(Guid userId) => Error.NotFound(
      "Users.NotFound",
      $"User with id '{userId}' was not found.");

  public static Error Unauthorized() => Error.Failure(
      "users.Unauthorized",
      $"You are not authorized to perform this action");
      
  public static Error InvalidRefreshToken = Error.Failure(
        "users.Unauthorized",
        $"Your refresh token is invalid or expired. Please log in again.");

  public readonly static Error NotFoundByEmail = Error.NotFound(
        "Users.NotFoundByEmail",
        $"User with the specified email does not exist.");

  public readonly static Error EmailNotUnique = Error.Conflict(
      "Users.EmailNotUnique",
      $"User with the specified email already exist.");
}