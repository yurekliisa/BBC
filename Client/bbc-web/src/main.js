import Vue from "vue";
import App from "./App.vue";
import "./registerServiceWorker";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import './assets/styles/main.sass';
import Vuelidate from "vuelidate";

import './plugins/vuetify'
Vue.config.productionTip = false;
Vue.use(Vuelidate);

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App),
}).$mount("#app");
