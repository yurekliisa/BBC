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
  { text: "Reçete ve Tarif", to: "/events", isAuth: "allow" },
  { text: "İş İlanları", to: "/team", isAuth: "allow" },
  { text: "Giriş", to: "/login", isAuth: "notUser" },
  { text: "Kayıt Ol", to: "/register", isAuth: "notUser" },
];
export default {
  name: "ToolBar",
  data() {
    let menu = orjMenu;
    if (localStorage.getItem("user") == null) {
      menu = orjMenu.filter((x) => x.isAuth !== "user");
    } else {
      menu = orjMenu.filter((x) => x.isAuth !== "notUser");
      menu.push({
        text: this.$store.getters.userInfo?.userName,
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
        console.log("setlendi");
        this.links = this.filterMenu();
        console.log(this.links);
      },
      immediate: true,
    },
  },
  methods: {
    filterMenu() {
      console.log("1");
      console.log(this.$store.userInfo);
      if (localStorage.getItem("user") == null) {
        return orjMenu.filter((x) => x.isAuth !== "user");
        console.log(menu);
      } else {
        const menu = orjMenu.filter((x) => x.isAuth !== "notUser");
        menu.push({
          text: this.$store.getters.userInfo.userName,
          to: "/profile/"+this.$store.getters.userInfo.id,
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
