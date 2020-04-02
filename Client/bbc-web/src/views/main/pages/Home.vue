<template>
  <v-layout
    wrap
    align-start
    justify-center
    row
    fill-height
    class="hidden-sm-and-down mb-4"
  >
    <v-flex xs3 sm6 md6 lg4 v-for="(item, i) in eventsData" :key="i">
      <ListCard :item="item" />
    </v-flex>
    <div class="text-center">
      <v-pagination
        v-model="page"
        :length="4"
        color="deep-purple accent-4"
        prev-icon="mdi-menu-left"
        next-icon="mdi-menu-right"
      ></v-pagination>
    </div>
  </v-layout>
</template>

<script>
import { mapGetters, mapMutations } from "vuex";
import ListCard from "../../../components/main/ListCard";
import { rotData } from "../../../assets/data/data";
export default {
  name: "Home",
  components: {
    ListCard
  },
  data() {
    return {
      page: 1,
      showLoader: true,
      eventsData: rotData
    };
  }
};
</script>
