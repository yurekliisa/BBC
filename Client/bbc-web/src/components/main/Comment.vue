<template>
  <div>
    <div v-if="false">
      <v-card class="ma-2 pa-1 my-2">
        <v-list three-line>
          <template v-for="(item, index) in items">
            <v-subheader
              v-if="item.header"
              :key="item.header"
              v-text="item.header"
            ></v-subheader>

            <v-divider
              v-else-if="item.divider"
              :key="index"
              :inset="item.inset"
            ></v-divider>

            <v-list-item v-else :key="item.title">
              <v-list-item-avatar>
                <v-img :src="item.avatar"></v-img>
              </v-list-item-avatar>

              <v-list-item-content>
                <v-list-item-title v-html="item.title"></v-list-item-title>
                <v-list-item-subtitle
                  v-html="item.subtitle"
                ></v-list-item-subtitle>
              </v-list-item-content>
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
        >Comment</v-btn
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

//  "commentDtos": [
//     {
//       "taRId": 0,
//       "comment": "string",
//       "puan": 0,
//       "userId": 0,
//       "userPhoto": "string",
//       "userName": "string",
//       "commentDate": "2020-05-12T01:49:04.231Z"
//     }
//   ],
export default {
  name: "Comment",
  props: ["comments", "tarId"],
  data() {
    return {
      comment: "",
      puan: 5,
      isComment: false,
    };
  },
  methods: {
    insertComment() {
      let data = {
        taRId: this.tarId,
        comment: this.comment,
        puan: this.puan,
        userId: this.$store.getters.userInfo.userId,
      };
      console.log(data);
      debugger;
      axios
        .post("TaR/Comments", data, {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
            Authorization: `Bearer ${this.$store.getters.userInfo.token}`,
          },
        })
        .then((result) => {
          console.log(result);
          if (result.status === 200) {
            this.isComment = !this.isComment;
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
