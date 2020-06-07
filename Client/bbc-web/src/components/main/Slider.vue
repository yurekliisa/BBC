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
      <v-row>
        <v-col
          cols="6"
          sm="4"
          class="cardHover"
          v-for="(yemek, j) in slide"
          :key="j"
        >
          <v-img :src="yemek.mainImage" @click="getDetail(yemek)">
            <v-row align="start" class="lightbox white--text pa-2 fill-height">
              <v-col>
                <div class="body-1" style="text-align:center">
                  {{ yemek.title }}
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
  name: "Slider",
  data() {
    return {
      dummyData: [
        {
          id: 1,
          title: "1-Yemek",
          mainImage:
            "https://www.yemekyapmasanati.com/wp-content/uploads/2015/05/banner.jpg.jpg",
        },
        {
          id: 2,
          title: "2-Yemek",
          mainImage:
            "https://previews.123rf.com/images/nadianb/nadianb1707/nadianb170700135/82244490-italian-food-background-pasta-and-meat-long-banner-format-.jpg",
        },
        {
          id: 3,
          title: "3-Yemek",
          mainImage:
            "https://www.yemekyapmasanati.com/wp-content/uploads/2015/05/sebze-banner.jpg",
        },
      ],
      slides: [],
    };
  },
  mounted() {
    this.fetchData();
  },
  computed: {
    currentRouteName() {
      return this.$route.name;
    },
  },
  methods: {
    getDetail(tar) {
      this.$router.push({
        name: "TARDetail",
        params: {
          name: tar.title.replace(/ /g, "&"),
          id: tar.id,
        },
      });
    },
    fetchData() {
      axios
        .get("/Home/GetSliderImages", {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
          },
        })
        .then((result) => {
          console.log(result);
          if (result.status === 200) {
            for (let i = 0; i < result.data.length / 3; i++) {
              let fakeArray = [];
              let counter = i * 3;
              for (let j = counter; j < counter + 3; j++) {
                const element = result.data[j];
                if (!element) {
                  break;
                }
                fakeArray.push(element);
              }
              this.slides.push(fakeArray);
            }
          }
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
};
</script>
