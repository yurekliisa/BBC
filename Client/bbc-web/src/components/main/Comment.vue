<template>
  <div>
    <div>
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

            <v-list-item v-else :key="item.title" @click="">
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

    <div class="mx-2 my-2 commentBtn" v-if="!isComment">
      <v-btn outlined color="primary" @click="isComment = !isComment"
        >Primary</v-btn
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

        <div class="commentBtn">
          <v-btn
            outlined
            class="mx-2"
            color="primary"
            @click="isComment = !isComment"
            >Cancel</v-btn
          >
          <v-btn
            outlined
            class="mx-2"
            color="primary"
            @click="isComment = !isComment"
            >Yorum Yap</v-btn
          >
        </div>
      </v-sheet>
    </div>
  </div>
</template>

<script>
export default {
  name: "Comment",
  props: {
    id: {
      required: true,
      type: Number
    }
  },
  data() {
    return {
      comment: "",
      isComment: false,
      items: [
        { header: "Today" },
        {
          avatar: "https://cdn.vuetifyjs.com/images/lists/1.jpg",
          title: "Brunch this weekend?",
          subtitle:
            "<span class='text--primary'>Ali Connors</span> &mdash; I'll be in your neighborhood doing errands this weekend. Do you want to hang out?"
        },
        { divider: true, inset: true },
        {
          avatar: "https://cdn.vuetifyjs.com/images/lists/2.jpg",
          title: 'Summer BBQ <span class="grey--text text--lighten-1">4</span>',
          subtitle:
            "<span class='text--primary'>to Alex, Scott, Jennifer</span> &mdash; Wish I could come, but I'm out of town this weekend."
        },
        { divider: true, inset: true },
        {
          avatar: "https://cdn.vuetifyjs.com/images/lists/3.jpg",
          title: "Oui oui",
          subtitle:
            "<span class='text--primary'>Sandra Adams</span> &mdash; Do you have Paris recommendations? Have you ever been?"
        },
        { divider: true, inset: true },
        {
          avatar: "https://cdn.vuetifyjs.com/images/lists/4.jpg",
          title: "Birthday gift",
          subtitle:
            "<span class='text--primary'>Trevor Hansen</span> &mdash; Have any ideas about what we should get Heidi for her birthday?"
        },
        { divider: true, inset: true },
        {
          avatar: "https://cdn.vuetifyjs.com/images/lists/5.jpg",
          title: "Recipe to try",
          subtitle:
            "<span class='text--primary'>Britta Holt</span> &mdash; We should eat this: Grate, Squash, Corn, and tomatillo Tacos."
        }
      ]
    };
  },
  created() {
    console.log(this.id);
  }
};
</script>

<style lang="sass">
.commentBtn
    display: flex
    justify-content: flex-end
</style>
