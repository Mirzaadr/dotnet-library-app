// using System.IdentityModel.Tokens.Jwt;
// using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using LibraryApp.Application.Abstractions.Authentication;
using LibraryApp.Application.Abstractions.Services;
using LibraryApp.Domain.Users;


using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Cryptography;

internal sealed class TokenProvider : ITokenProvider
{
  private readonly JwtSettings _jwtSettings;
  private readonly IDateTimeProvider _dateTimeProvider;

  public TokenProvider(IOptions<JwtSettings> jwtSettings, IDateTimeProvider dateTimeProvider)
  {
    _jwtSettings = jwtSettings.Value;
    _dateTimeProvider = dateTimeProvider;
  }

  public string Generate(User user)
  {
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Issuer = _jwtSettings.Issuer,
      Audience = _jwtSettings.Audience,
      Subject = new ClaimsIdentity([
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Role, user.Role.ToString())
      ]),
      Expires = _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
      SigningCredentials = credentials,
    };

    var handler = new JsonWebTokenHandler();

    return handler.CreateToken(tokenDescriptor);
  }

  public string GenerateRefreshToken()
  {
    using (var rng = RandomNumberGenerator.Create())
    {
      var randomBytes = new byte[32];
      rng.GetBytes(randomBytes);
      return Convert.ToBase64String(randomBytes);
    }
  }
}

public class JwtSettings
{
  public const string SectionName = "JwtSettings";
  public string SecretKey { get; init; } = null!;
  public int ExpiryInMinutes { get; init; }
  public string Issuer { get; init; } = null!;
  public string Audience { get; init; } = null!;
}