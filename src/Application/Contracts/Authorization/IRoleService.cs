using Application.Dto.Authorization;
using Application.Wrappers;

namespace Application.Contracts.Authorization;

public interface IRoleService
{
    Task<Response<IEnumerable<RoleData>>> GetAllRole();
    Task<Response<IEnumerable<RoleData>>> GetAllUserRoles(string userId);
    Task<Response<bool>> AddRole(string userId, string roleId);
    Task<Response<bool>> DeleteRole(string userId, string roleId);
}