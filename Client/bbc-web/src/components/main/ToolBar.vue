<template>
  <v-app-bar absolute color="white">
    <v-toolbar-title class="ml-0 pl-1 mr-1">
      <router-link
        :to="'/'"
        class="google-font"
        style="text-decoration:none; color: rgba(0,0,0,.87);"
      >BBC</router-link>
    </v-toolbar-title>
    <v-spacer></v-spacer>
    <v-btn
      v-for="(link, i) in links"
      :key="i"
      :to="link.to"
      class="ml-2 google-font hidden-sm-and-down"
      style="text-transform: capitalize;"
      @click="onClick($event, link)"
    >{{ link.text }}</v-btn>
  </v-app-bar>
</template>

<script>
export default {
  name: "ToolBar",
  data() {
    return {
      links: [
        { text: "Ana Sayfa", to: "/", icon: "home" },
        { text: "Reçete ve Tarif", to: "/events", icon: "rounded_corner" },
        { text: "İş İlanları", to: "/team", icon: "group" },
        { text: "Login", to: "/login", icon: "toc" },
        { text: "Register", to: "/register"}
      ]
    };
  },
  methods: {
    onClick(e, item) {
      e.stopPropagation();
      if (item.to || !item.href) return;
      this.$vuetify.goTo(item.href);
    }
  }
};
</script>

<style>
</style>