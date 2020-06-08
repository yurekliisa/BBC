<template>
  <div>
    <div v-if="true">
      <v-card class="ma-2 pa-1 my-2">
        <v-list three-line>
          <v-subheader
            class="text--primary"
            style="justify-content:center;font-size:22px"
            >YORUMLAR</v-subheader
          >
          <template v-for="(comment, index) in comments">
            <v-list-item :key="index">
              <router-link :to="'/profile/' + comment.userId">
                <v-list-item-avatar>
                  <v-img
                    :src="
                      'https://bbc-api.azurewebsites.net/' + comment.userPhoto
                    "
                  ></v-img>
                </v-list-item-avatar>
              </router-link>

              <v-list-item-content>
                <v-list-item-title>{{ comment.userName }}</v-list-item-title>
                <v-list-item-subtitle class="text--primary">{{
                  comment.comment
                }}</v-list-item-subtitle>
              </v-list-item-content>

              <v-list-item-action>
                <v-list-item-action-text
                  v-text="comment.commentDate"
                ></v-list-item-action-text>

                <v-list-item-action-text>
                  <v-rating v-model="comment.puan"></v-rating>
                </v-list-item-action-text>
              </v-list-item-action>
            </v-list-item>
          </template>
        </v-list>
      </v-card>
    </div>

    <div
      class="mx-2 my-2 commentBtn"
      v-if="!isComment && $store.getters.userInfo.userId"
    >
      <v-btn outlined color="primary" @click="isComment = !isComment"
        >Yorum Yap</v-btn
      >
    </div>
    <div class="mx-2 my-2" v-if="isComment">
      <v-sheet elevation="12" class="pa-4">
        <v-textarea
          v-model="comment"
          :hint="'Yemek Hakkında Bir yorum yazınız'"
          :loading="false"
          :no-resize="false"
          :outlined="true"
          :placeholder="'Yemek Hakkında Bir yorum yazınız'"
          :row-height="24"
          :rows="1"
          :shaped="true"
          :solo="true"
        ></v-textarea>
        <div class="text-center">
          <label>Puan veriniz </label>
          <v-rating v-model="puan"></v-rating>
        </div>
        <div class="commentBtn">
          <v-btn
            outlined
            class="mx-2"
            color="primary"
            @click="isComment = !isComment"
            >Cancel</v-btn
          >
          <v-btn outlined class="mx-2" color="primary" @click="insertComment"
            >Yorum Yap</v-btn
          >
        </div>
      </v-sheet>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "Comment",
  props: ["comments", "tarId"],
  data() {
    return {
      header: "Yorumlar",
      comment: "",
      puan: 5,
      isComment: false,
    };
  },
  mounted() {
    console.log(this.$store.getters.userInfo.token);
  },
  methods: {
    insertComment() {
      console.log(this.$store.getters.userInfo.token);
      let data = {
        taRId: this.tarId,
        comment: this.comment,
        puan: this.puan,
        userId: this.$store.getters.userInfo.userId,
      };
      axios
        .post("TaR/Comments", data, {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
            Authorization: `Bearer ${this.$store.getters.userInfo.token}`,
          },
        })
        .then((result) => {
          if (result.status === 200) {
            axios
              .get("TaR/GetAllComments?tarId=" + this.tarId, {
                headers: {
                  "Content-type": "application/json",
                  "Access-Control-Allow-Origin": "*",
                  Authorization: `Bearer ${this.$store.getters.userInfo.token}`,
                },
              })
              .then((res) => {
                if (res.status === 200) {
                  this.comments = res.data;
                  this.comment = "";
                  this.puan = 5;
                  this.isComment = false;
                }
              });
          }
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
};
</script>

<style lang="sass">
.commentBtn
    display: flex
    justify-content: flex-end
</style>
