import Client from "@/services/models/Client";
import Event from "@/services/models/Event";

export interface IEventUser {
    id: number;

    client: Client;
    event: Event;
}

export default class EventUser implements IEventUser {
  constructor(
    eventUser: IEventUser = {
      id: 0, 
      client: new Client(),
      event: new Event(),
    },
  ) {
    this.id = eventUser.id;
    
    this.client = eventUser.client;
    this.event = eventUser.event;
  }

  id: number;

  client: Client;
  event: Event;
}
