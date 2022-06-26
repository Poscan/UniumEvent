import httpClient from "@/services/HttpClient";
import {IResponse} from "@/services/models/Response";

export default {
  async login(login: string, password: string) : Promise<IResponse<any, string>> {
    return await httpClient.post("/api/Authorization/login", {
      userName: login,
      password: password,
    });
  },

  async createAccount(login: string, password: string, lastName: string, firstName: string, patrName: string) : Promise<IResponse<any, string>> {
    return await httpClient.post("/api/Account", {
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

  async renewToken() : Promise<IResponse<any, string>> {
    const token = sessionStorage.getItem("refreshToken");

    return await httpClient.post("/api/Authorization/refresh", {
      refreshToken: token,
    });
  },
};
