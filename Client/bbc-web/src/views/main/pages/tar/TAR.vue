<template>
  <v-layout wrap align-start justify-center row fill-height class="mb-4">
    <v-flex md12>
      <h1 class="homeTitle">Son Paylaşılanlar</h1>
      <v-row>
        <v-flex md4 v-for="(item, i) in tars" :key="i">
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
  </v-layout>
</template>

<script>
import ListCard from "../../../../components/main/ListCard";
import axios from "axios";
export default {
  name: "TAR",
  components: {
    ListCard,
  },
  created() {
    this.fetchData();
  },
  data() {
    return {
      page: 1,
      showLoader: true,
      tars: [],
    };
  },
  methods: {
    fetchData() {
      axios
        .get("Home/GetHomeContent?page=" + this.page, {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
          },
        })
        .then((response) => {
          this.tars = response.data;
        });
    },
  },
};
</script>
