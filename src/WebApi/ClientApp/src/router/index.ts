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
    component: () => import("../views/EventPage.vue"),
  },
  {
    path: "/authorize",
    name: "Authorize",
    component: () => import("../views/Authorize.vue"),
  },
  {
    path: "/account",
    name: "Account",
    component: () => import("../views/Account.vue"),
    children: [
      {
        path: "user-profile",
        component: () => import("../views/UserProfile.vue"),
      },
      {
        path: "edit-user-profile",
        component: () => import("../views/EditUserProfile.vue"),
      },
      {
        path: "user-events",
        component: () => import("../views/UserEvents.vue"),
      },
    ],
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
