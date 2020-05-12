<template>
  <v-card :loading="item.isLoading" class="ma-2 pa-1 my-2">
    <v-img
      height="250"
      :src="'https://localhost:44308/' + item.mainImage"
    ></v-img>
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

      <v-chip outlined>
        <v-avatar left>
          <v-img
            :src="'https://localhost:44308/' + item.userPhoto"
          ></v-img>
        </v-avatar>
        {{ item.userFullName }}
      </v-chip>
      <div style="margin-top:1rem">{{ item.shortDescription }}</div>
    </v-card-text>

    <v-divider class="mx-4"></v-divider>
    <v-card-title>Malzemeler</v-card-title>

    <v-card-text>
      <v-chip-group
        v-model="item.anaUrun"
        active-class="deep-purple accent-4 white--text"
        column
      >
        <v-chip v-for="(urun, j) in item.categories" :key="j">{{ urun }}</v-chip>
      </v-chip-group>
    </v-card-text>
    <v-card-actions>
      <v-btn text color="deep-purple accent-4" @click="reserve()"
        >Devamını Oku</v-btn
      >
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  name: "ListCard",
  props: {
    item: {
      type: Object,
    },
  },
  methods: {
    reserve() {
      this.item.isLoading = true;
      setTimeout(() => {
        this.item.isLoading = false;
        this.$router.push({
          name: "TARDetail",
          params: {
            name: this.item.title.replace(/ /g, "&"),
            id: this.item.id,
          },
        });
      }, 2000);
    },
  },
};
</script>

<style></style>
