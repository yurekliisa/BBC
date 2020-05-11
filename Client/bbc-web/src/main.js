import Vue from "vue";
import App from "./App.vue";
import "./registerServiceWorker";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import './assets/styles/main.sass';
import Vuelidate from "vuelidate";

import './plugins/vuetify'
import Axios from "axios";


import { TiptapVuetifyPlugin } from "tiptap-vuetify";
import "tiptap-vuetify/dist/main.css";
import 'vuetify/dist/vuetify.min.css'
import '@mdi/font/css/materialdesignicons.css'
import '@fortawesome/fontawesome-free/css/all.css'

Vue.config.productionTip = false;
Vue.use(Vuelidate);
Vue.use(TiptapVuetifyPlugin, {
  vuetify,
  // "md" (default), "fa", "mdi"
  iconsGroup: 'mdi'// same as "iconsGroup: iconsGroup"
})


Axios.interceptors.response.use(response => {
  return response;
}, err => {
  const { config, response: { status, data } } = err;
  if (status === 401 && data.message == "Expired token" || status === 404) {
    if (localStorage.getItem('user') !== null) {
      localStorage.removeItem('user');
    }
  }
})

Axios.defaults.baseURL = "https://localhost:44308/api/";





new Vue({
  router,
  store,
  vuetify,
  render: h => h(App),
}).$mount("#app");
