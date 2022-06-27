import { IClient } from "./models/Client";
import httpClient from "@/services/HttpClient";
import {IResponse} from "@/services/models/Response";

export default {
  async getCurrentUser() : Promise<IResponse<IClient, string>> {
    return await httpClient.get("/api/Client/current-user");
  },
  
  async findClients(searchString: string) : Promise<IResponse<IClient[], string>> {
    return await httpClient.get("/api/Client/find?searchString=" + searchString);
  },
  
  async SaveUser(client: IClient) : Promise<IResponse<any, string>> {
    return await httpClient.post("/api/Client", {
      "id": client.id,
      "userId": client.userId,
      "lastName": client.lastName,
      "firstName": client.firstName,
      "patrName": client.patrName,
      "mobilePhone": client.mobilePhone,
      "email": client.email,
      "schoolNumber": client.schoolNumber,
      "birthday": client.birthday,
      "parentLastName": client.parentLastName,
      "parentFirstName": client.parentFirstName,
      "parentPatrName": client.parentPatrName,
      "parentMobilePhone": client.parentMobilePhone
    });
  },
};
