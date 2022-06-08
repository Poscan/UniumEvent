using Application.Contracts.Authorization;
using Application.Dto;
using Application.Dto.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClientsResponse = Application.Wrappers.Response<System.Collections.Generic.IEnumerable<Application.Dto.ClientDto>>;

namespace WebApi.Controllers;

[Authorize]
public class RoleController : ApiControllerBase
{
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
    }

    private readonly IRoleService _roleService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleData>>> GetRoles()
    {
        return Ok( await _roleService.GetAllRole());
    }
    
    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<RoleData>>> GetUserRoles(string userId)
    {
        return Ok( await _roleService.GetAllUserRoles(userId));
    }
    
    [HttpPost]
    public async Task<ActionResult<bool>> AddRole([FromBody] UserRoleDto userRole)
    {
        return Ok( await _roleService.AddRole(userRole.UserId, userRole.RoleId));
    }
    
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteRole([FromBody] UserRoleDto userRole)
    {
        return Ok(await _roleService.DeleteRole(userRole.UserId, userRole.RoleId));
    }
}