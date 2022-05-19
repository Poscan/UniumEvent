import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    user: {
      lastName: String,
      firstName: String,
      patrName: String
    }
  },
  mutations: {},
  actions: {},
  modules: {},
});
