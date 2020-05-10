<template>
  <v-layout :v-if="user">
    <v-flex class="customContainer">
      <v-card class="w-100 h-97">
        <v-container
          id="scroll-target"
          style="max-height: 80vh"
          class="overflow-y-auto"
        >
          <v-row
            v-scroll:#scroll-target="onScroll"
            align="center"
            justify="center"
            style="height: auto"
          >
          </v-row>
          <v-list-item>
            <v-list-item-avatar color="grey"></v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title class="headline">{{
                user.userName
              }}</v-list-item-title>
              <v-list-item-subtitle>{{ user.email }}</v-list-item-subtitle>
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
                  {{ i }}
                  <v-icon v-if="icons">mdi-phone</v-icon>
                </v-tab>

                <v-tab-item
                  v-for="i in tabs"
                  :key="i"
                  :value="'tab-' + i"
                  :v-if="user.id"
                >
                  <div v-if="i === 'Özet'">
                    <ProfileDashboard :id="user.id" />
                  </div>
                  <div v-else-if="i === 'Tarif ve Reçeteler'">
                    <ProfileTAR :id="user.id" />
                  </div>
                  <div v-else>
                    İletişim
                  </div>
                </v-tab-item>
              </v-tabs>
            </v-row>
          </v-card-text>
        </v-container>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import ProfileDashboard from "../../../components/main/Profile-dashboard";
import ProfileTAR from "../../../components/main/Profile-tar";
export default {
  name: "profile",
  components: {
    ProfileDashboard,
    ProfileTAR,
  },
  data() {
    return {
      tab: null,
      user: this.$store.getters.userInfo,
      icons: false,
      centered: true,
      grow: false,
      prevIcon: false,
      nextIcon: false,
      offsetTop: 0,
      tabs: ["Özet", "Tarif ve Reçeteler", "İletişim"],
    };
  },
  created() {
   console.log(this.user);
  },
  methods: {
    onScroll(e) {
      this.offsetTop = e.target.scrollTop;
    },
  },
};
</script>

<style lang="sass">
.w-100
  width: 100%
.h-97
  height: 97%
.profile-footer
  height: 10%
  bottom: 0
  position: absolute
</style>
