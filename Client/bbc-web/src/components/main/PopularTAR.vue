<template>
  <div class="mt-4">
    <h1 class="homeTitle">Popular Tarifler</h1>
    <v-row>
      <v-flex v-for="(item, i) in eventsData" :key="i">
        <v-card :loading="item.isLoading" class="ma-1 pa-1 cardHover" >
          <v-img
            height="100"
            :src="item.kapakResim"
            class="white--text align-end"
          >
            <v-card-title style="color:white">{{ item.name }}</v-card-title>
          </v-img>
        </v-card>
      </v-flex>
    </v-row>
  </div>
</template>

<script>
import { rotData } from "../../assets/data/data";

export default {
  name: "PopularTAR",
  data() {
    const newData = rotData.slice(0,10);
    console.log(newData);
    return {
      eventsData: newData,
    };
  },
};
</script>
