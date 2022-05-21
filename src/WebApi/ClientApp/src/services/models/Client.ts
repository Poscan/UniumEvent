export interface IClient {
  id: number;

  lastName: string;
  firstName: string;
  patrName: string | null;
  mobilePhone: string | null;
  email: string;
  schoolNumber: string | null;
  birthday: Date | null;

  parentLastName: string | null;
  parentFirstName: string | null;
  parentPatrName: string | null;
  parentMobilePhone: string | null;
}

export default class Client implements IClient {
  constructor(
    client: IClient = {
      id: 0,
      lastName: "",
      firstName: "",
      patrName: null,
      mobilePhone: null,
      email: "",
      schoolNumber: null,
      birthday: null,
      parentLastName: null,
      parentFirstName: null,
      parentPatrName: null,
      parentMobilePhone: null,
    },
  ) {
    this.id = client.id;
    this.lastName = client.lastName;
    this.firstName = client.firstName;
    this.patrName = client.patrName;
    this.mobilePhone = client.mobilePhone;
    this.email = client.email;
    this.schoolNumber = client.schoolNumber;
    this.birthday = client.birthday;

    this.parentLastName = client.parentLastName;
    this.parentFirstName = client.parentFirstName;
    this.parentPatrName = client.parentPatrName;
    this.parentMobilePhone = client.parentMobilePhone;
  }

  id: number;

  lastName: string;
  firstName: string;
  patrName: string | null;
  mobilePhone: string | null;
  email: string;
  schoolNumber: string | null;
  birthday: Date | null;

  parentLastName: string | null;
  parentFirstName: string | null;
  parentPatrName: string | null;
  parentMobilePhone: string | null;
}
