import axios from "axios";
import {IEventUser} from "@/services/models/EventUser";

export default {
  async SignUpEvent(eventUser: IEventUser){
    return await axios.post("/api/EventUsers", {
      "userId": eventUser.client.id,
      "eventId": eventUser.event.id,
    });
  },

  async DeleteEventUser(eventUserId: number) {
    return await axios.delete("/api/EventUsers/" + eventUserId);
  },

  async GetAllUsers(eventId: number) {
    return await axios.get("/api/EventUsers/event-id/" + eventId);
  },

  async GetAllEvent(clientId: number) {
    return await axios.get("/api/EventUsers/client-id/" + clientId);
  },
};
