import axios from "axios";

export default {
  async login(login: string, password: string) {
    return await axios.post("/api/Authorization/login", {
      userName: login,
      password: password,
    });
  },

  async createAccount(login: string, password: string, lastName: string, firstName: string, patrName: string) {
    return await axios.post("/api/Account", {
      userName: login,
      password: password,
      lastName: lastName,
      firstName: firstName,
      patrName: patrName,
    });
  },

  getAccessToken() {
    return sessionStorage.getItem("accessToken");
  },

  async renewToken() {
    const token = sessionStorage.getItem("refreshToken");

    return await axios.post("/api/Authorization/refresh", {
      refreshToken: token,
    });
  },
};
