<template>
  <div v-if="item !== undefined">
    <v-card class="ma-2 pa-1 my-2" v-if="item !== undefined"
      ><!--:loading="item.isLoading"-->
      <v-img height="250" :src="item.kapakResim"></v-img>
      <v-card-title>{{ item.name }}</v-card-title>
      <v-card-text>
        <v-row align="center" class="mx-0">
          <v-chip outlined>
            <v-avatar left>
              <v-img src="https://cdn.vuetifyjs.com/images/john.png"></v-img>
            </v-avatar>
            {{ item.actor.fullName }}
          </v-chip>
          <v-spacer />
          <v-rating
            :value="item.point"
            color="amber"
            dense
            half-increments
            readonly
            size="14"
          ></v-rating>
          <div class="grey--text ml-4">
            {{ item.point }}
            <v-icon class="ml-7 mr-2">mdi-comment-outline</v-icon
            >{{ item.commentCount }}
          </div>
        </v-row>

        <div class="my-4 subtitle-1" v-html="content"></div>
      </v-card-text>
      <v-divider class="mx-4"></v-divider>
      <v-card-title>Kategoriler</v-card-title>
      <v-card-text>
        <v-chip-group
          v-model="item.anaUrun"
          active-class="deep-purple accent-4 white--text"
          column
        >
          <v-chip v-for="(urun, j) in item.urunler" :key="j">{{ urun }}</v-chip>
        </v-chip-group>
      </v-card-text>
    </v-card>

    <Comment v-bind:id="$route.params.id"/>
  </div>
</template>

<script>
import { rotData } from "../../../assets/data/data";
import Comment from "../../../components/main/Comment";
export default {
  name: "rotdetail",
  components:{
    Comment
  },
  created() {
    console.log(this.$route.params);
    this.item = rotData.find(x => x.id === this.$route.params.id);
    console.log(this.item);
  },
  data() {
    return {
      item: undefined,
      content: `<h1>Most basic use</h1><ul><li><p>hjk</p></li><li><p>ghjkfgjh</p></li><li><p>yturt</p></li><li><p>uety</p></li><li><p>wert</p></li><li><p>wer</p></li><li><p>qwer</p></li><li><p>qwe</p></li><li><p>asf</p></li><li><p>dfg</p></li><li><p>hfdh</p></li><li><p>j</p></li></ul><hr><p>You can use the necessary extensions. The corresponding buttons are <strong>added automatically.</strong></p><p><code>&lt;tiptap-vuetify v-model="content" :extensions="extensions"/&gt;</code></p><p></p><h2>Icons</h2><p>Avaliable icons groups:</p><ol><li><p>Material Design <em>Official</em></p></li><li><p>Font Awesome (FA)</p></li><li><p>Material Design Icons (MDI)</p></li></ol><p></p><blockquote><p>This package is awesome!</p></blockquote><p></p>`
    };
  },
  methods: {
    like(val) {
      this.item.isLoading = true;
      setTimeout(() => {
        this.item.isLoading = false;
        if (!val) {
          this.item.isLike = true;
        } else {
          this.item.isLike = false;
        }
      }, 1000);
    }
  }
};
</script>
