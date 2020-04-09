<template>
  <v-layout wrap align-start justify-center row fill-height class="mb-4"><!-- >hidden-sm-and-down -->
    <v-flex md9>
      <v-row>
        <v-flex xs3 sm6 md6 v-for="(item, i) in eventsData" :key="i">
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
      <v-row>
        <v-flex xs3 sm6 md6 v-for="(item, i) in eventsData" :key="i">
          <ListCard :item="item" />
        </v-flex>
      </v-row>
    </v-flex>
  </v-layout>
</template>

<script>
import { mapGetters, mapMutations } from "vuex";
import ListCard from "../../../components/main/ListCard";
import { rotData } from "../../../assets/data/data";
export default {
  name: "Home",
  components: {
    ListCard,
  },
  data() {
    return {
      page: 1,
      showLoader: true,
      eventsData: rotData,
    };
  },
};
</script>
