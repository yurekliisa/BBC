import Vue from "vue";
import VueRouter from "vue-router";

import Layout from "../views/main/shared/Layout";
import AdminLayout from "../views/admin/shared/AdminLayout.vue";

import admin from "./admin";
import main from "./main";
Vue.use(VueRouter);

const GuardToken = (to, from, next) => {
  let go = localStorage.getItem('user');
  if (go==null) {
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
      path: "/admin",
      component: AdminLayout,
      children: admin.router,
      beforeEnter: GuardToken
    },
    {
      path: "/",
      component: Layout,
      children: main.router,
    },
    {
      path: '*',
      component: Layout,
      children: main.router,
    }
  ]
}
export default new VueRouter(router);
