using Application.Contracts.Authorization;
using Application.Dto.Authorization;
using Application.Wrappers;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenFactory _tokenFactory;

    public AccountService(UserManager<IdentityUser> userManager, ITokenFactory tokenFactory)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _tokenFactory = tokenFactory ?? throw new ArgumentNullException(nameof(tokenFactory));
    }

    public async Task<Response<AuthorizationData>> CreateUserAsync(string userName, string password)
    {
        var user = new IdentityUser(userName);

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return Response.Fail<AuthorizationData>(result.Errors.Select(x => new ResponseError(x.Description)));

        await _userManager.AddToRoleAsync(user, "Ученик");
        var authorizationData = GetAuthorizationData(user.Id);

        return Response.Success(authorizationData);
    }

    private AuthorizationData GetAuthorizationData(string userId)
    {
        var accessToken = _tokenFactory.CreateAccessToken(userId);
        var refreshToken = _tokenFactory.CreateRefreshToken(userId);

        return new AuthorizationData(accessToken, refreshToken, userId);
    }
}