import {IEventUser} from "@/services/models/EventUser";
import httpClient from "@/services/HttpClient";
import {IResponse} from "@/services/models/Response";

export default {
  async SignUpEvent(eventUser: IEventUser) : Promise<IResponse<IEventUser, string>>{
    return await httpClient.post("/api/EventUsers", {
      "userId": eventUser.client.id,
      "eventId": eventUser.event.id,
    });
  },

  async DeleteEventUser(eventUserId: number) : Promise<IResponse<any, string>> {
    return await httpClient.delete("/api/EventUsers/" + eventUserId);
  },

  async GetAllUsers(eventId: number) : Promise<IResponse<IEventUser[], string>> {
    return await httpClient.get("/api/EventUsers/event-id/" + eventId);
  },

  async GetAllEvent(clientId: number) : Promise<IResponse<IEventUser[], string>> {
    return await httpClient.get("/api/EventUsers/client-id/" + clientId);
  },
};
