import httpClient from "@/services/HttpClient";
import {IResponse} from "@/services/models/Response";
import {IEvent} from "@/services/models/Event";

export default {
  async GetAllEvents() : Promise<IResponse<IEvent[], string>> {
    return await httpClient.get("/api/Events");
  },
};
