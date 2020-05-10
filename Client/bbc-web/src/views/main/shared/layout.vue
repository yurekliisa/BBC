<template>
  <v-app>
    <v-card class="overflow-hidden mb-70">
      <!--ToolBar-->
      <ToolBar />
      <!--ToolBar-->
      <Slider />
      <!--Container-->
      <v-container
        class="pa-0"
        v-bind:class="[currentRouteName === 'Home' ? 'mt-3' : 'mt-5-rem']"
      >
        <router-view />
      </v-container>
      <!--Container-->
    </v-card>
    <Footer />
  </v-app>
</template>

<script>
import { mapGetters, mapMutations } from "vuex";

import ToolBar from "../../../components/main/ToolBar";
import Footer from "../../../components/main/Footer";
import Slider from "../../../components/main/Slider";

export default {
  name: "Layout",
  components: {
    ToolBar,
    Slider,
    Footer,
  },
  computed: {
    currentRouteName() {
      return this.$route.name;
    },
  },
  data() {
    return {
      showLoader: true,
    };
  },
};
</script>

<style lang="sass" scoped>
.mt-5-rem
  margin-top: 5rem !important
.mb-70
  margin-bottom: 70px
</style>
