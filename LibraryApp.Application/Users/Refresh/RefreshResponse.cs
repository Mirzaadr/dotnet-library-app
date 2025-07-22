namespace LibraryApp.Application.Users.Refresh;

public class RefreshResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public RefreshResponse(string token, string refreshToken)
    {
        AccessToken = token;
        RefreshToken = refreshToken;
    }
}