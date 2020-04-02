<template>
  <!--Slider-->
  <v-carousel
    v-if="currentRouteName === 'Home'"
    class="mt-12"
    cycle
    height="auto"
    hide-delimiter-background
    show-arrows-on-hover
  >
    <v-carousel-item v-for="(slide, i) in slides" :key="i">
      <v-sheet height="100%">
        <v-parallax height="400" :src="slide"></v-parallax>
      </v-sheet>
    </v-carousel-item>
  </v-carousel>
  <!--Slider-->
</template>

<script>
export default {
  name: "ToolBar",
  data() {
    return {
      slides: [
        "https://www.yemekyapmasanati.com/wp-content/uploads/2015/05/banner.jpg.jpg",
        "https://www.yemekyapmasanati.com/wp-content/uploads/2015/05/sebze-banner.jpg",
        "https://thebosporus.com/wp-content/themes/bosporus_theme/images/franchise-banner.jpg",
        "https://previews.123rf.com/images/nadianb/nadianb1707/nadianb170700135/82244490-italian-food-background-pasta-and-meat-long-banner-format-.jpg"
      ]
    };
  },
  computed: {
    currentRouteName() {
      return this.$route.name;
    }
  }
};
</script>

<style></style>
