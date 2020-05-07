<template>
  <v-layout wrap align-start justify-center row fill-height class="mb-4"
    ><!-- >hidden-sm-and-down -->

    <v-flex md9>
      <h1 class="homeTitle">Son Paylaşılanlar</h1>
      <v-row>
        <v-flex md4 v-for="(item, i) in eventsData" :key="i">
          <ListCard :item="item" />
        </v-flex>
      </v-row>
      <v-row justify="center">
        <div class="text-center">
          <v-pagination
            v-model="page"
            :length="4"
            color="deep-purple accent-4"
            prev-icon="mdi-menu-left"
            next-icon="mdi-menu-right"
          ></v-pagination>
        </div>
      </v-row>
    </v-flex>

    <v-flex md2 offset-md-1>
      <v-col style="padding:0">
        <PopularTAR />
        <PopularTAR />
        <PopularChef />
        <PopularCategory />
      </v-col>
    </v-flex>
  </v-layout>
</template>

<script>
import { mapGetters, mapMutations } from "vuex";
import ListCard from "../../../components/main/ListCard";
import PopularTAR from "../../../components/main/PopularTAR";
import PopularCategory from "../../../components/main/PopularCategory";
import PopularChef from "../../../components/main/PopularChef";
import axios from "axios";
import { rotData } from "../../../assets/data/data";
export default {
  name: "Home",
  components: {
    ListCard,
    PopularTAR,
    PopularCategory,
    PopularChef,
  },
  created() {
    this.fetchData();
  },
  data() {
    // const data = rotData.slice(0, 18);
    return {
      page: 1,
      showLoader: true,
      eventsData:[],
    };
  },
  methods: {
    fetchData() {
      axios
        .get(
          "Home/GetHomeContent?page="+this.page,
          {},
          {
            headers: {
              "Content-type": "application/json",
              "Access-Control-Allow-Origin": "*",
            },
          }
        )
        .then((response) => {
          this.eventsData = response.data;
        });
    },
  },
};
</script>

<style lang="sass">
.v-slide-group__content
  margin-left: 1rem
.homeTitle
  display: flex
  justify-content: center
</style>
