<template>
  <v-navigation-drawer v-model="drawer" :mini-variant="miniVariant" :clipped="clipped" fixed app>
    <v-list>
         <v-list-item v-for="(item, i) in items" :key="i" :to="item.to" router exact>
          <v-list-item-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title v-text="item.title" />
          </v-list-item-content>
        </v-list-item>
   
    </v-list>
  </v-navigation-drawer>
</template>

<script>
export default {
  name: "LeftSidebar",
  props:[
    'drawer',
    'miniVariant',
    'clipped',
  ],
  data() {
    return {
   
      items: [
        {
          icon: "mdi-view-dashboard",
          title: "Dashboard",
          to: "/admin/dashboard"
        },
        {
          icon: "mdi-format-list-bulleted",
          title: "Category",
          to: "/admin/category"
        },
        {
          icon: "mdi-message-text-outline", // bu da ikonu fakat gözükmüyo
          title: "Lobby",
          to: "/admin/lobby"
        },
        {
          icon: "mdi-multiplication", //mdi-settings // gözükmüyo ama ikonu bu mdi-settings-box
          title: "Settings",
          to: "/admin/settings"
        },
        {
          icon: "mdi-logout", // bu da ikonu fakat gözükmüyo
          title: "Çıkış",
          to: "/"
        },
      ]
    };
  },
 
};
</script>
