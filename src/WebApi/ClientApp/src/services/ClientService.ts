import axios from "axios";
import { IClient } from "./models/Client";

export default {
  async getCurrentUser() {
    return await axios.get<IClient>("/api/Client/current-user");
  },
  
  async findCliens(searchString: string) {
    return await axios.get("/api/Client/find?searchString=" + searchString);
  },
  
  async SaveUser(client: IClient){
    return await axios.post("/api/Client", {
      "id": client.id,
      "userId": client.userId,
      "lastName": client.lastName,
      "firstName": client.firstName,
      "patrName": client.patrName,
      "mobilePhone": client.mobilePhone,
      "email": client.email,
      "schoolNumber": client.schoolNumber,
      "birthday": null,
      "parentLastName": client.parentLastName,
      "parentFirstName": client.parentFirstName,
      "parentPatrName": client.parentPatrName,
      "parentMobilePhone": client.parentMobilePhone
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
};
