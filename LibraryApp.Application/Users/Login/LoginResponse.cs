namespace LibraryApp.Application.Users.Login;

public class LoginResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public LoginResponse(string token, string refreshToken)
    {
        AccessToken = token;
        RefreshToken = refreshToken;
    }
}