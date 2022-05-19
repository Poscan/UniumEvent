import axios from "axios";

export default {
    async login(login: string, password: string) {
        return await axios.post("/api/Authorization/login", {
            userName: login,
            password: password
        });
    },

    async createAccount(login: string, password: string){
        return await axios.post("/api/Account", {
            userName: login,
            password: password
        })
    }
}