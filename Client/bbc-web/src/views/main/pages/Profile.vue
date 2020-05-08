<template>
  <v-layout>
    <v-flex class="customContainer">
      <v-card class="w-100 h-97">
        <v-list-item>
          <v-list-item-avatar color="grey"></v-list-item-avatar>
          <v-list-item-content>
            <v-list-item-title class="headline">User Name</v-list-item-title>
            <v-list-item-subtitle>user e mail</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
        <v-card-text>
          <v-row justify="space-around">
            <v-tabs
              v-model="tab"
              background-color="deep-purple accent-4"
              class="elevation-5"
              dark
              centered
              grow
              :prev-icon="
                prevIcon ? 'mdi-arrow-left-bold-box-outline' : undefined
              "
              :next-icon="
                nextIcon ? 'mdi-arrow-right-bold-box-outline' : undefined
              "
            >
              <v-tabs-slider></v-tabs-slider>

              <v-tab v-for="i in tabs" :key="i" :href="`#tab-${i}`">
                Tab {{ i }}
                <v-icon v-if="icons">mdi-phone</v-icon>
              </v-tab>

              <v-tab-item v-for="i in tabs" :key="i" :value="'tab-' + i">
                <v-card flat tile>
                  <v-card-text>{{ text }}</v-card-text>
                </v-card>
              </v-tab-item>
            </v-tabs>
          </v-row>
        </v-card-text>

        <v-card-actions class="profile-footer">
          <v-btn text color="deep-purple accent-4">
            Read
          </v-btn>
          <v-btn text color="deep-purple accent-4">
            Bookmark
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn icon>
            <v-icon>mdi-heart</v-icon>
          </v-btn>
          <v-btn icon>
            <v-icon>mdi-share-variant</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import axios from "axios";

export default {
  name: "profile",
  data() {
    return {
      tab: null,
      text:
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
      icons: false,
      centered: true,
      grow: false,
      vertical: false,
      prevIcon: false,
      nextIcon: false,
      right: false,
      tabs: 3,
    };
  },
};
</script>

<style lang="sass">
.w-100
  width: 100%
.h-97
  height: 97%
.profile-content
.profile-footer
  height: 10%
  bottom: 0
  position: absolute
</style>
