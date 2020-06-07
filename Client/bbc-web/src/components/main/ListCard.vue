<template>
  <v-card :loading="item.isLoading" class="ma-2 pa-1 my-2">
    <v-img height="250" :src="'https://bbc-api.azurewebsites.net/' + item.mainImage" @click="routing()"
    style="cursor:pointer"></v-img>
    <v-card-title>{{ item.title }}</v-card-title>
    <v-card-text>
      <v-row align="center" class="mx-0">
        <v-rating :value="item.puan" color="amber" dense half-increments readonly size="14"></v-rating>
        <div class="grey--text ml-4">{{ item.puan }} ({{ item.commentCount }})</div>
      </v-row>
      <router-link :to="'/profile/' + item.userId" style="text-decoration:none;">
        <v-chip outlined style="cursor:pointer;">
          <v-avatar left>
            <v-img :src="'https://bbc-api.azurewebsites.net/' + item.userPhoto"></v-img>
          </v-avatar>
          <span>{{ item.userFullName }}</span>
        </v-chip>
      </router-link>
      <div style="margin-top:1rem">{{ item.shortDescription }}</div>
    </v-card-text>

    <v-divider class="mx-4"></v-divider>
    <v-card-title>Malzemeler</v-card-title>

    <v-card-text>
      <v-chip-group v-model="item.anaUrun" active-class="deep-purple accent-4 white--text" column>
        <v-chip v-for="(urun, j) in item.categories" :key="j">
          {{
          urun
          }}
        </v-chip>
      </v-chip-group>
    </v-card-text>
    <v-card-actions>
      <v-btn text color="deep-purple accent-4" @click="routing()">Devamını Oku</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  name: "ListCard",
  props: {
    item: {
      type: Object
    }
  },
  methods: {
    routing() {
      this.$router.push({
        name: "TARDetail",
        params: {
          name: this.item.title.replace(/ /g, "&"),
          id: this.item.id
        }
      });
    }
  }
};
</script>

<style></style>
