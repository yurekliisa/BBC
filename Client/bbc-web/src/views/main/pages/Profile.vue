<template>
  <v-layout v-if="user.id">
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
          <v-col cols="12" v-if="id == $store.getters.userInfo.userId">
            <v-btn text color="deep-purple accent-4">Profilini Düzenle</v-btn>
          </v-col>
          <v-list-item>
            <v-list-item-avatar color="grey" v-if="user.photo">
              <v-img
                height="250"
                :src="'https://localhost:44308' + user.photo"
              ></v-img>
            </v-list-item-avatar>
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
import axios from "axios";
export default {
  name: "profile",
  components: {
    ProfileDashboard,
    ProfileTAR,
  },
  data() {
    return {
      tab: null,
      id: null,
      user: {},
      icons: false,
      centered: true,
      grow: false,
      prevIcon: false,
      nextIcon: false,
      offsetTop: 0,
      isYourProfile: false,
      tabs: ["Özet", "Tarif ve Reçeteler", "İletişim"],
    };
  },
  mounted() {
    this.id = this.$route.params["id"];
    axios.get("User/Get/" + this.id).then((res) => {
      if (res.status === 200) {
        this.user = res.data;
      }
    });
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
