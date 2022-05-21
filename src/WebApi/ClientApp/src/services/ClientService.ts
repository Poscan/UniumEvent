import axios from "axios";
import { IClient } from "./models/Client";

export default {
  async getCurrentUser() {
    return await axios.get<IClient>("/api/Client/current-user");
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
};
