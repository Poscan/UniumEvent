export interface IRole {
    roleId: string;
    roleName: string;
}

export default class Role implements IRole {
    constructor(
        role: IRole = {
            roleId: "",
            roleName: "",
        },
    ) {
        this.roleId = role.roleId;

        this.roleName = role.roleName;
    }

    roleId: string;
    roleName: string;
}
