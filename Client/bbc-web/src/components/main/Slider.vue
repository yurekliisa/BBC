<template>
  <!--Slider-->
  <v-carousel
    v-if="currentRouteName === 'Home'"
    class="mt-12"
    cycle
    height="150px"
    hide-delimiter-background
    show-arrows-on-hover
  >
    <v-carousel-item v-for="(slide, i) in slides" :key="i">
      <!-- <v-sheet height="100%">
        <v-parallax height="400" :src="slide"></v-parallax>
      </v-sheet> -->
      <v-row>
        <v-col cols="6" sm="4" class="cardHover" v-on:click="test()">
          <v-img
            :src="slide"
            gradient="to top right, rgba(100,115,201,.33), rgba(25,32,72,.7)"
          >
            <v-row align="end" class="lightbox white--text pa-2 fill-height ">
              <v-col>
                <div class="subheading" style="text-align:center">
                  Jonathan Lee
                </div>
                <div class="body-1" style="text-align:center">
                  heyfromjonathan@gmail.com
                </div>
              </v-col>
            </v-row>
          </v-img>
        </v-col>

        <v-col cols="6" sm="4" class="cardHover">
          <v-img :src="slides[i]">
            <!-- <div class="fill-height bottom-gradient"></div> -->
            <v-row align="end" class="lightbox white--text pa-2 fill-height">
              <v-col>
                <div class="subheading" style="text-align:center">
                  Jonathan Lee
                </div>
                <div class="body-1" style="text-align:center">
                  heyfromjonathan@gmail.com
                </div>
              </v-col>
            </v-row>
          </v-img>
        </v-col>

        <v-col cols="6" sm="4" class="cardHover">
          <v-img :src="slides[i]">
            <!-- <div class="fill-height repeating-gradient"></div> -->
            <v-row align="end" class="lightbox white--text pa-2 fill-height">
              <v-col>
                <div class="subheading" style="text-align:center">
                  Jonathan Lee
                </div>
                <div class="body-1" style="text-align:center">
                  heyfromjonathan@gmail.com
                </div>
              </v-col>
            </v-row>
          </v-img>
        </v-col>
      </v-row>
    </v-carousel-item>
  </v-carousel>
  <!--Slider-->
</template>

<script>
import axios from "axios";
export default {
  /*
  [
  {
    "id": 0,
    "mainImage": "string",
    "title": "string"
  }
]
*/
  name: "ToolBar",
  data() {
    return {
      slides: [
        "https://www.yemekyapmasanati.com/wp-content/uploads/2015/05/banner.jpg.jpg",
        "https://www.yemekyapmasanati.com/wp-content/uploads/2015/05/sebze-banner.jpg",
        "https://thebosporus.com/wp-content/themes/bosporus_theme/images/franchise-banner.jpg",
        "https://previews.123rf.com/images/nadianb/nadianb1707/nadianb170700135/82244490-italian-food-background-pasta-and-meat-long-banner-format-.jpg",
      ],
    };
  },
  computed: {
    currentRouteName() {
      return this.$route.name;
    },
  },
  methods:{
    test(){
      console.log("qweqwe")
    }
  }
};
</script>


