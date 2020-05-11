<template>
  <v-app-bar absolute color="white">
    <v-toolbar-title class="ml-0 pl-1 mr-1">
      <router-link
        :to="'/'"
        class="google-font"
        style="text-decoration:none; color: rgba(0,0,0,.87);"
        >BBC</router-link
      >
    </v-toolbar-title>
    <v-spacer></v-spacer>
    <v-btn
      v-for="(link, i) in links"
      :key="i"
      :to="link.to"
      class="ml-2 google-font hidden-sm-and-down"
      style="text-transform: capitalize;"
      @click="onClick($event, link)"
      >{{ link.text }}</v-btn
    >
  </v-app-bar>
</template>

<script>
const orjMenu = [
  { text: "Ana Sayfa", to: "/", isAuth: "allow" },
  { text: "Reçete ve Tarif", to: "/tar", isAuth: "allow" },
  { text: "İş İlanları", to: "/team", isAuth: "allow" },
  { text: "Giriş", to: "/login", isAuth: "notUser" },
  { text: "Kayıt Ol", to: "/register", isAuth: "notUser" },
];
export default {
  name: "ToolBar",
  data() {
    let menu = orjMenu;
    if (JSON.parse(localStorage.getItem("user")) == null) {
      menu = orjMenu.filter((x) => x.isAuth !== "user");
    } else {
      menu = orjMenu.filter((x) => x.isAuth !== "notUser");
      menu.push({
        text: "register",
        to: "/register",
        isAuth: "user",
      });
    }
    return {
      links: menu,
    };
  },
  watch: {
    "$store.getters.userInfo": {
      handler(newValue) {
        this.links = this.filterMenu();
      },
      immediate: true,
    },
  },
  // mounted() {
  //   this.filterMenu();
  //   console.log(this.$store.getters.userInfo);
  // },
  methods: {
    filterMenu() {
      let userData = this.$store.getters.userInfo;//
      if (userData.userId == undefined) {
        return orjMenu.filter((x) => x.isAuth !== "user");
      } else {
        const menu = orjMenu.filter((x) => x.isAuth !== "notUser");
        menu.push({
          text: userData.userName,
          to: "/profile/" + userData.userId,
          isAuth: "user",
        });
        return menu;
      }
    },
    onClick(e, item) {
      e.stopPropagation();
      if (item.to || !item.href) return;
      this.$vuetify.goTo(item.href);
    },
  },
};
</script>

<style></style>
