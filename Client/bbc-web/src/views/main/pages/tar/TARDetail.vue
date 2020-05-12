<template>
  <div v-if="data !== undefined">
    <v-card class="ma-2 pa-1 my-2" v-if="data">
      <v-img
        v-if="data.mainImage"
        height="250"
        :src="'https://localhost:44308/' + data.mainImage"
      ></v-img>
      <v-card-title>{{ data.title }}</v-card-title>
      <v-card-text>
        <v-row align="center" class="mx-0">
          <v-chip outlined to="/">
            <v-avatar left v-if="data.userPhoto">
              <v-img :src="'https://localhost:44308/' + data.userPhoto"></v-img>
            </v-avatar>
            {{ data.userFullName }}
          </v-chip>
          <v-spacer />
          <v-rating
            :value="data.puan"
            color="amber"
            dense
            half-increments
            readonly
            size="14"
          ></v-rating>
          <div class="grey--text ml-4">
            {{ data.puan }}
            <v-icon class="ml-7 mr-2">mdi-comment-outline</v-icon
            >{{ data.commentCount }}
          </div>
        </v-row>

        <div
          class="my-4 subtitle-1 description"
          v-html="data.contentText"
        ></div>
      </v-card-text>
      <v-divider class="mx-4"></v-divider>
      <v-card-title>Kategoriler</v-card-title>
      <v-card-text>
        <v-chip-group active-class="deep-purple accent-4 white--text" column>
          <v-chip v-for="(urun, j) in data.categories" :key="j">{{
            urun
          }}</v-chip>
        </v-chip-group>
      </v-card-text>
    </v-card>
    <div v-if="data.commentDtos && data.id">
    <Comment :comments="data.commentDtos" :tarId="data.id"/>
    </div>
  </div>
</template>

<script>
import Comment from "../../../../components/main/Comment";
import axios from "axios";

export default {
  name: "TARDetail",
  components: {
    Comment,
  },
  created() {
    console.log(this.$route.params.id);
  },
  data() {
    return {
      data: {},
      /*
      {
  "title": "string",
  "contentText": "string",
  "puan": 0,
  "commentDtos": [
    {
      "taRId": 0,
      "comment": "string",
      "puan": 0
    }
  ],
  "mainImage": "string",
  "shortDescription": "string",
  "userFullName": "string",
  "userPhoto": "string",
  "userId": 0,  
  "id": 0
}
      */
    };
  },
  mounted() {
    axios
      .get("TaR/GetTarifAndReceteDetails?tarId=" + this.$route.params.id)
      .then((res) => {
        if (res.status === 200) {
          this.data = res.data;
          console.log(this.data);
        }
      });
  },
  methods: {
    like(val) {
      this.data.isLoading = true;
      setTimeout(() => {
        this.data.isLoading = false;
        if (!val) {
          this.data.isLike = true;
        } else {
          this.data.isLike = false;
        }
      }, 1000);
    },
  },
};
</script>

<style lang="sass" scoped>
.description
  img
    width: 500px
    height: 300px
    display: flex
    margin: auto
    margin-top: 1rem
    margin-bottom: 1rem
</style>
