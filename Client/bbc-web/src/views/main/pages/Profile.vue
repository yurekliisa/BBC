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

          <v-list-item>
            <v-list-item-avatar color="grey" v-if="user.photo">
              <v-img
                :src="'https://bbc-api.azurewebsites.net' + user.photo"
              ></v-img>
            </v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title class="headline"
                >{{ user.name }} {{ user.surName }}</v-list-item-title
              >
              <v-list-item-subtitle>{{ user.userName }}</v-list-item-subtitle>
            </v-list-item-content>
            <v-list-item-action v-if="id == $store.getters.userInfo.userId">
              <v-list-item-action-text>
                <v-btn
                  text
                  color="deep-purple accent-4"
                  :to="'/profile/edit/' + id"
                >
                  <v-icon color="indigo">mdi-account-settings-outline</v-icon
                  >Profilini Düzenle</v-btn
                >
              </v-list-item-action-text>
              <v-list-item-action-text>
                <v-btn text color="deep-purple accent-4" @click="logout()">
                  <v-icon color="indigo">mdi-logout</v-icon>Çıkış</v-btn
                >
              </v-list-item-action-text>
            </v-list-item-action>
          </v-list-item>

          <v-list-item>
            <v-list-item-icon>
              <v-icon color="indigo">mdi-email</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-subtitle>EMail</v-list-item-subtitle>
              <v-list-item-title>{{ user.email }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item>
            <v-list-item-icon>
              <v-icon color="indigo">mdi-phone</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-subtitle>Mobile</v-list-item-subtitle>
              <v-list-item-title>{{ user.phoneNumber }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item>
            <v-list-item-icon>
              <v-icon color="indigo">mdi-calendar-account</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-subtitle>Birthday</v-list-item-subtitle>
              <v-list-item-title>{{ user.birthday }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
          <v-divider inset></v-divider>

          <v-list-item>
            <v-list-item-icon> </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-subtitle>Social Media</v-list-item-subtitle>
              <v-list-item-title v-if="user.socialMedia">
                <v-btn icon color="primary" :href="user.socialMedia.twitterUrl">
                  <v-icon>mdi-twitter</v-icon>
                </v-btn>
                <v-btn
                  icon
                  color="primary"
                  :href="user.socialMedia.facebookUrl"
                >
                  <v-icon>mdi-facebook</v-icon>
                </v-btn>
                <v-btn
                  icon
                  color="primary"
                  :href="user.socialMedia.instagramUrl"
                >
                  <v-icon>mdi-instagram</v-icon>
                </v-btn>
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-divider inset></v-divider>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title class="headline" style="text-align:center">{{
                user.about
              }}</v-list-item-title>
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
        res.data.birthday = new Date(res.data.birthday).toLocaleDateString();
        this.user = res.data;
      }
    });
  },
  methods: {
    logout() {
      localStorage.removeItem("user");
      this.$store.commit("SET_USER", undefined);
      this.$router.push("/");
    },
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
