import axios from "axios";
import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import AuthService from "./services/AuthService";
import store from "./store";
// @ts-ignore
import VueTheMask from 'vue-the-mask'

Vue.use(VueTheMask);
Vue.config.productionTip = false;

const authService = AuthService;

axios.interceptors.request.use((request: any) => {
  const token = authService.getAccessToken();

  if (token) {
    request.headers["Authorization"] = `Bearer ${token}`;
  }
  return request;
});

function createAxiosResponseInterceptor() {
  const interceptor = axios.interceptors.response.use(
    (response) => response,
    (error) => {
      const status = error.response ? error.response.status : null;

      if (status !== 401) {
        return Promise.reject(error);
      }

      axios.interceptors.response.eject(interceptor);

      authService
        .renewToken()
        .then(() => {
          const token = authService.getAccessToken();

          if (token) {
            error.response.config.headers["Authorization"] = `Bearer ${token}`;
            return axios(error.response.config);
          }
          else {
              const pageName = router.currentRoute.name;
              if(pageName != "Home" && pageName != "EventPage"){
                  router.push("/authorize");    
              }
          }
        })
        .catch((error: any) => {
          sessionStorage.clear();

          const pageName = router.currentRoute.name;
          if(pageName != "Home" && pageName != "EventPage") {
              router.push("/authorize");
          }
          return Promise.reject(error);
        })
        .finally(createAxiosResponseInterceptor);
    },
  );
}

createAxiosResponseInterceptor();

new Vue({
    router,
    store,
    render: (h) => h(App),
}).$mount("#app");