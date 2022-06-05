using Application.Contracts.Authorization;
using Application.Dto.Authorization;
using Application.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthenticationService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<AuthenticationData> AuthenticateAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
            throw new ApiException("Неверный логин или пароль");
        
        var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, password);
        if (passwordIsCorrect == false)
            throw new ApiException("Неверный логин или пароль");

        return new AuthenticationData(user.Id);
    }
}