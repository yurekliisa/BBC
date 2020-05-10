<template>
  <div class="px-5">
    <v-row class="create-button my-5">
      <v-btn text color="deep-purple accent-4" to="/tar/create"
        >Create TaR</v-btn
      >
    </v-row>
    <v-row style="background-color:red;height:10000px;width:1000px">
      <v-flex v-for="(item, i) in tarByProps" :key="i">
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
                <template v-slot:activator="{ on }">
                  <v-chip outlined v-on="on">
                    <v-avatar left>
                      <v-img :src="item.userPhoto"></v-img>
                    </v-avatar>
                    {{ item.userFullName }}
                  </v-chip>
                </template>
                <v-card width="300">
                  <v-list dark>
                    <v-list-item>
                      <v-list-item-avatar>
                        <v-img :src="item.userPhoto"></v-img>
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
                    <v-list-item @click="() => {}">
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
