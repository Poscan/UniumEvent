import Client from "@/services/models/Client";
import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    client: new Client(),
    isLoading: false
  },
  mutations: {},
  actions: {},
  modules: {},
  getters: {
    getClient(context) {
      return context.client ?? new Client();
    }
  }
});
