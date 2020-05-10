<template>
  <div class="mt-4">
    <h1 class="homeTitle">Popular Tarifler</h1>
    <v-row>
      <v-flex v-for="(item, i) in tarByProps" :key="i">
        <v-card :loading="item.id" class="ma-1 pa-1 cardHover">
          <v-img
            height="100"
            :src="item.mainImage"
            class="white--text align-end"
          >
            <v-card-title style="color:white">{{ item.title }}</v-card-title>
          </v-img>
        </v-card>
      </v-flex>
    </v-row>
  </div>
</template>

<script>
import { rotData } from "../../assets/data/data";
import axios from "axios";

export default {
  data() {
    return {
      tarByProps: [],
    };
  },
  mounted() {
    axios
      .get("/Home/PopularTaRByCategory", {
        headers: {
          "Content-type": "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
      .then((result) => {
        console.log(result);
        if (result.status === 200) {
          this.tarByProps = result.data;
        }
      })
      .catch((err) => {
        console.log(err);
      });
  },
};
</script>
