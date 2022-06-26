import httpClient from "@/services/HttpClient";
import {IResponse} from "@/services/models/Response";
import {IRole} from "@/services/models/Role";

export default {
    async getRoles() : Promise<IResponse<IRole[], string>> {
        return await httpClient.get("/api/Role");
    },

    async addUserRole(userId: string, roleId: string) : Promise<IResponse<boolean, string>> {
        return await httpClient.post("/api/Role", {
            userId: userId,
            roleId: roleId
        });
    },

    async deleteUserRole(userId: string, roleId: string) : Promise<IResponse<boolean, string>> {
        return await httpClient.delete("/api/Role", {data: {userId: userId, roleId: roleId}});
    },

    async getUserRole(userId: string) : Promise<IResponse<IRole[], string>> {
        return await httpClient.get("/api/Role/user?userId=" + userId);
    },
};
