namespace Application.Dto.Authorization;

public class AuthorizationData
{
    public AuthorizationData(JwtToken accessToken, JwtToken refreshToken, string userId)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        UserId = userId;
    }

    public JwtToken AccessToken { get; }

    public JwtToken RefreshToken { get; }
    public string UserId { get; }
}