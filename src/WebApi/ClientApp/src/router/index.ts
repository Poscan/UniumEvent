import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Home from "../views/Home.vue";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/event-page",
    name: "EventPage",
    component: () =>
      import("../views/EventPage.vue"),
  },
  {
    path: "/authorize",
    name: "Authorize",
    component: () =>
      import("../views/Authorize.vue"),
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
