export interface IEvent {
    id: number;

    name: string;
    pageName: string;
    startDate: Date;
    endDate: Date;
}

export default class Event implements IEvent {
  constructor(
    event: IEvent = {
      id: 0, 
      name: "",
      pageName: "",
      startDate: new Date(),
      endDate: new Date(),
    },
  ) {
    this.id = event.id;
    
    this.name = event.name;
    this.pageName = event.pageName;
    this.startDate = new Date(event.startDate);
    this.endDate = new Date(event.endDate);
  }

  id: number;

  name: string;
  pageName: string;
  startDate: Date;
  endDate: Date;
}
