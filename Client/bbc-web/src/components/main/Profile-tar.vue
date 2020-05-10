<template>
  <div class="px-5">
    <v-row class="create-button my-5">
      <v-btn text color="deep-purple accent-4" to="/tar/create"
        >Create TaR</v-btn
      >
    </v-row>
    <v-row>
      <v-flex v-for="(item, i) in tars" :key="i">
        <v-card :loading="item.isLoading" class="ma-2 pa-1 my-2">
          <v-img height="250" :src="item.mainImage"></v-img>
          <v-card-title>{{ item.title }}</v-card-title>
          <v-card-text>
            <v-row align="center" class="mx-0">
              <v-rating
                :value="item.puan"
                color="amber"
                dense
                half-increments
                readonly
                size="14"
              ></v-rating>
              <div class="grey--text ml-4">
                {{ item.puan }} ({{ item.commentCount }})
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
              </v-menu>
            </div>
            <div>{{ item.shortDescription }}</div>
          </v-card-text>

          <v-divider class="mx-4"></v-divider>
          <v-card-title>Malzemeler</v-card-title>

          <v-card-text>
            <v-chip-group
              v-model="item.anaUrun"
              active-class="deep-purple accent-4 white--text"
              column
            >
              <v-chip v-for="(urun, j) in item.urunler" :key="j">{{
                urun
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
export default {
  name: "ProfileTAR",
  props: ["id"],
  data() {
    return {
      tars: {},
    };
  },
};
</script>

<style lang="sass" scoped>
.create-button
  display: flex
  justify-content: flex-end
</style>
