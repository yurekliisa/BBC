import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/main/Home.vue";

import AdminLayout from "../views/admin/shared/AdminLayout.vue";
import admin from "./admin";
Vue.use(VueRouter);

const GuardToken = (to, from, next) => {
  //--- Check Token
  let go = false;
  if (go) {
    next({
      path: '/',
    })
  }
  next();
}

const router = {
  mode: "history",
  scrollBehavior: () => ({ y: 0 }),
  routes: [
    {
      path: "/",
      component: Home
    },
    {
      path: "/admin",
      component: AdminLayout,
      children: admin.router,
      beforeEnter: GuardToken
    },
    {
      path: '*',
      component: Home
    }
  ]
}
export default new VueRouter(router);
