<template>
  <div v-if="item !== undefined">
    <v-card class="ma-2 pa-1 my-2" v-if="item !== undefined"
      ><!--:loading="item.isLoading"-->
      <v-img height="250" :src="item.kapakResim"></v-img>
      <v-card-title>{{ item.name }}</v-card-title>
      <v-card-text>
        <v-row align="center" class="mx-0">
          <v-rating
            :value="item.point"
            color="amber"
            dense
            half-increments
            readonly
            size="14"
          ></v-rating>
          <div class="grey--text ml-4">
            {{ item.point }} ({{ item.commentCount }})
          </div>
        </v-row>

        <div class="my-4 subtitle-1">
          <v-menu
            v-model="item.menu"
            bottom
            right
            transition="scale-transition"
            origin="top left"
          >
            <template v-slot:activator="{ on }">
              <v-chip outlined v-on="on">
                <v-avatar left>
                  <v-img
                    src="https://cdn.vuetifyjs.com/images/john.png"
                  ></v-img>
                </v-avatar>
                {{ item.actor.fullName }}
              </v-chip>
            </template>
            <v-card width="300">
              <v-list dark>
                <v-list-item>
                  <v-list-item-avatar>
                    <v-img
                      src="https://cdn.vuetifyjs.com/images/john.png"
                    ></v-img>
                  </v-list-item-avatar>
                  <v-list-item-content>
                    <v-list-item-title>{{
                      item.actor.fullName
                    }}</v-list-item-title>
                    <v-list-item-subtitle>{{
                      item.actor.job
                    }}</v-list-item-subtitle>
                  </v-list-item-content>
                  <v-list-item-action>
                    <v-btn icon @click="item.menu = false">
                      <v-icon>mdi-close-circle</v-icon>
                    </v-btn>
                  </v-list-item-action>
                </v-list-item>
              </v-list>
              <v-list>
                <v-list-item>
                  <v-list-item-action>
                    <v-icon>mdi-briefcase</v-icon>
                  </v-list-item-action>
                  <v-list-item-subtitle>{{
                    item.actor.email
                  }}</v-list-item-subtitle>
                </v-list-item>
              </v-list>
            </v-card>
          </v-menu>
        </div>
        <div>{{ item.summary }}</div>
      </v-card-text>

      <v-divider class="mx-4"></v-divider>
      <v-card-title>Malzemeler</v-card-title>

      <v-card-text>
        <v-chip-group
          v-model="item.anaUrun"
          active-class="deep-purple accent-4 white--text"
          column
        >
          <v-chip v-for="(urun, j) in item.urunler" :key="j">{{ urun }}</v-chip>
        </v-chip-group>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn icon v-if="!item.isLike" @click="like(false)">
          <v-icon>mdi-heart</v-icon>
        </v-btn>
        <v-btn icon v-if="item.isLike" @click="like(true)">
          <v-icon color="red">mdi-heart</v-icon>
        </v-btn>
        <v-btn icon>
          <v-icon>mdi-share-variant</v-icon>
        </v-btn>
      </v-card-actions>
    </v-card>
  </div>
</template>

<script>
import { rotData } from "../../../assets/data/data";
export default {
  name: "rotdetail",
  created() {
    console.log(this.item);
    this.item = rotData.find((x) => x.id === this.$route.params.id);
    console.log(this.item);
  
  },
  data() {
    return {
      item:undefined,
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
    },
  },
};
</script>
