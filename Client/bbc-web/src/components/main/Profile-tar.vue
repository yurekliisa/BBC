<template>
  <div class="px-5">
    <v-row class="create-button my-5">
      <v-btn text color="deep-purple accent-4" to="/tar/create"
        >Create TaR</v-btn
      >
    </v-row>
    <v-row>
      <v-flex v-for="(item, i) in tars" :key="i">
        <v-card :loading="item.isLoading" class="ma-2 pa-1 my-2" >
          <v-img height="250" :src="'https://localhost:44308/'+item.content.mainImage"></v-img>
          <v-card-title>{{ item.content.title }}</v-card-title>
          <v-card-text>
            <v-row align="center" class="mx-0">
              <v-rating
                :value="item.content.puan"
                color="amber"
                dense
                half-increments
                readonly
                size="14"
              ></v-rating>
              <div class="grey--text ml-4">
                {{ item.content.puan }} ({{ item.content.commentCount }})
              </div>
            </v-row>
            <div style="margin-top:1rem">{{ item.content.shortDescription }}</div>
          </v-card-text>
          <v-divider class="mx-4"></v-divider>
          <v-card-title>Kategoriler</v-card-title>
          <v-card-text>
            <v-chip-group
              active-class="deep-purple accent-4 white--text"
              column
            >
              <v-chip v-for="(urun, j) in item.categories" :key="j">{{
                urun.name
              }}</v-chip>
            </v-chip-group>
          </v-card-text>
          <v-card-actions>
            <v-btn text color="deep-purple accent-4" @click="reserve()"
              >Devamını Oku</v-btn
            >
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-row>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "ProfileTAR",
  props: ["id"],
  data() {
    return {
      tars: [],
    };
  },
  mounted() {
    this.fetchdata();
  },
  methods: {
    fetchdata() {
      axios
        .get("TaR/GetTarifAndReceteByUserId?userId=" + this.id, {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
          },
        })
        .then((result) => {
          console.log(result);
          if (result.status === 200) {
            this.tars = result.data;
          }
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
};
</script>

<style lang="sass" scoped>
.create-button
  display: flex
  justify-content: flex-end
</style>
