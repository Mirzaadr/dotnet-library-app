using LibraryApp.Domain.Users;

namespace LibraryApp.Application.Abstractions.Authentication;

public interface ITokenProvider
{
  string Generate(User user);
}