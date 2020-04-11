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
          icon: "mdi-earth",
          title: "Country",
          to: "/admin/country"
        },
        {
          icon: "mdi-account-card-details", // bu da ikonu fakat gözükmüyo
          title: "UserReports",
          to: "/admin/userreports"
        },
        {
          icon: "mdi-settings-box", //mdi-settings // gözükmüyo ama ikonu bu mdi-settings-box
          title: "Settings",
          to: "/admin/settings"
        },
      ]
    };
  },
 
};
</script>
