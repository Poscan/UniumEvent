import axios from "axios";

export default {
    async getRoles() {
        return await axios.get("/api/Role");
    },

    async addUserRole(userId: string, roleId: string) {
        return await axios.post("/api/Role", {
            userId: userId,
            roleId: roleId
        });
    },

    async deleteUserRole(userId: string, roleId: string) {
        return await axios.delete("/api/Role", {data: {userId: userId, roleId: roleId}});
    },

    async getUserRole(userId: string) {
        return await axios.get("/api/Role/user?userId=" + userId);
    },
};
