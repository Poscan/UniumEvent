import axios from "axios";

export default {
  async GetAllEvents() {
    return await axios.get("/api/Events");
  },
};
