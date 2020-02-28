<template>
  <v-navigation-drawer v-model="rightDrawer" :right="true" temporary fixed>
    <v-list>
      <v-list-item-group v-model="group">
        <v-list-item>
          <v-icon light>mdi-repeat</v-icon>
          <v-list-item-title>Settings</v-list-item-title>
        </v-list-item>
      </v-list-item-group>
    </v-list>
  </v-navigation-drawer>
</template>

<script>
export default {
  name: "RightSidebar",
  watch: {
    "rightDrawer":{
      handler(val,oldVal){
        if(!val)
          this.$store.commit("SET_rightDrawer",val);
      },
      immediate:true,
    },
     "$store.getters.rightDrawer": {
      handler(val, oldVal) {
        this.rightDrawer = val;
      },
      immediate: true
    },
  },
  data() {
    return {
      group: null,
      rightDrawer: this.$store.getters.rightDrawer
    };
  }
};
</script>
