using Application.Contracts.Authorization;
using Application.Dto.Authorization;
using Application.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    public async Task<Response<IEnumerable<RoleData>>> GetAllRole()
    {
        var roles = await _roleManager.Roles.ToListAsync();

        return Response.Success(roles.Select(x => new RoleData(x.Id, x.Name)));
    }
    
    public async Task<Response<IEnumerable<RoleData>>> GetAllUserRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Response.Fail<IEnumerable<RoleData>>(ResponseError.New("Не удалось найти пользователя"));
        
        var roleNames = await _userManager.GetRolesAsync(user);
        var roles = await _roleManager.Roles.Where(x => roleNames.Contains(x.Name)).ToListAsync();
        
        return Response.Success(roles.Select(x => new RoleData(x.Id, x.Name)));
    } 

    public async Task<Response<bool>> AddRole(string userId, string roleId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Response.Fail<bool>(ResponseError.New("Не удалось найти пользователя"));
        
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
            return Response.Fail<bool>(ResponseError.New("Не удалось найти роль"));
        
        await _userManager.AddToRoleAsync(user, role.Name);

        return Response.Success(true);
    }

    public async Task<Response<bool>> DeleteRole(string userId, string roleId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Response.Fail<bool>(ResponseError.New("Не удалось найти пользователя"));
        
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
            return Response.Fail<bool>(ResponseError.New("Не удалось найти роль"));
        
        await _userManager.RemoveFromRoleAsync(user, role.Name);

        return Response.Success(true);
    }
}