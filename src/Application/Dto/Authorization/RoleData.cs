namespace Application.Dto.Authorization;

public class RoleData
{
    public RoleData(string roleId, string roleName)
    {
        RoleId = roleId;
        RoleName = roleName;
    }

    public string RoleId { get; }
    public string RoleName { get; }
}