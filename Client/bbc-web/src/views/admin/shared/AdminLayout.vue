<template>
  <v-app dark>
    <LeftSidebar :drawer="drawer" :miniVariant="miniVariant" :clipped="clipped" />

    <v-app-bar :clipped-left="clipped" fixed app>
      <v-app-bar-nav-icon @click.stop="changeDrawer()" />
      <v-btn v-if="drawer" @click.stop="changeMiniVariant()" icon>
        <v-icon>mdi-{{ `chevron-${miniVariant ? 'right' : 'left'}` }}</v-icon>
      </v-btn>
      <v-btn v-if="drawer || clipped" @click.stop="changeClipped()" icon>
        <v-icon>mdi-application</v-icon>
      </v-btn>
      <v-spacer />
      <v-toolbar-title center v-text="panelText" />
      <v-spacer />
      <v-btn @click.stop="changeRightDrawer()" icon>
        <v-icon>mdi-menu</v-icon>
      </v-btn>
    </v-app-bar>
    <v-content>
      <v-container>
        <router-view />
      </v-container>
    </v-content>
    <RightSidebar />
    <Footer />
  </v-app>
</template>

<script>
import LeftSidebar from "../../../components/admin/LeftSidebar";
import RightSidebar from "../../../components/admin/RightSidebar";
import Footer from "../../../components/admin/Footer";
export default {
  name: "AdminLayout",
  components: {
    LeftSidebar,
    RightSidebar,
    Footer
  },
  watch: {
    "$store.getters.panelText": {
      handler(newValue) {
        this.panelText = newValue;
      },
      immediate: true
    },
    "$store.getters.rightDrawer": {
      handler(newValue) {
        this.rightDrawer = newValue;
      },
      immediate: true
    }
  },
  data() {
    return {
      clipped: this.$store.getters.clipped,
      drawer: this.$store.getters.drawer,
      fixed: this.$store.getters.fixed,
      miniVariant: this.$store.getters.miniVariant,
      right: this.$store.getters.right,
      rightDrawer: this.$store.getters.rightDrawer,
      panelText: "default Text"
    };
  },
  methods: {
    changeDrawer() {
      this.drawer = !this.drawer;
      this.$store.commit("SET_drawer", this.drawer);
    },
    changeClipped() {
      this.clipped = !this.clipped;
      this.$store.commit("SET_clipped", this.clipped);
    },
    changeMiniVariant() {
      this.miniVariant = !this.miniVariant;
      this.$store.commit("SET_miniVariant", this.miniVariant);
    },
    changeRightDrawer() {
      this.rightDrawer = !this.rightDrawer;
      this.$store.commit("SET_rightDrawer", this.rightDrawer);
    }
  }
};
</script>
