<template>
  <v-layout v-if="userId > 0">
    <v-flex>
      <v-card class="mx-auto formContainer" width="900" raised>
        <v-col cols="12">
          <h1 style="justify-content: center">Profilini Düzenle</h1>
        </v-col>
        <form>
          <v-col cols="12">
            <v-text-field
              v-model="userName"
              label="User name"
              solo
            ></v-text-field>
            <v-row>
              <v-col cols="6">
                <v-text-field
                  v-model="userFirstName"
                  label="First Name"
                  solo
                ></v-text-field>
              </v-col>
              <v-col cols="6">
                <v-text-field
                  v-model="userSurname"
                  label="Surname"
                  solo
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="6">
                <v-text-field
                  v-model="userPhone"
                  label="Phone Number"
                  solo
                ></v-text-field>
              </v-col>
              <v-col cols="6">
                <v-menu
                  ref="menu1"
                  v-model="menu1"
                  :close-on-content-click="false"
                  transition="scale-transition"
                  offset-y
                  max-width="290px"
                  min-width="290px"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      v-model="dateFormatted"
                      label="Date"
                      persistent-hint
                      @blur="userBirthday = parseDate(dateFormatted)"
                      v-on="on"
                      solo
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    v-model="userBirhtday"
                    no-title
                    @input="menu1 = false"
                  ></v-date-picker>
                </v-menu>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="4">
                <v-text-field
                  v-model="facebookUrl"
                  label="facebookUrl"
                  solo
                ></v-text-field>
              </v-col>
              <v-col cols="4">
                <v-text-field
                  v-model="instagramUrl"
                  label="instagramUrl"
                  solo
                ></v-text-field>
              </v-col>
              <v-col cols="4">
                <v-text-field
                  v-model="twitterUrl"
                  label="twitterUrl"
                  solo
                ></v-text-field>
              </v-col>
            </v-row>
            <v-textarea
              v-model="userAboutMe"
              label="About me"
              solo
            ></v-textarea>
            <v-card-actions style="justify-content:center;">
              <v-btn
                width="100"
                outlined
                color="primary"
                @click="submit"
                :disabled="submitStatus === 'PENDING'"
                >Submit</v-btn
              >
            </v-card-actions>
          </v-col>
        </form>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      userBirhtday: new Date().toISOString().substr(0, 10),
      dateFormatted: this.formatDate(new Date().toISOString().substr(0, 10)),
      menu1: false,
      userId: 0,
      userName: "",
      userFirstName: "",
      userSurname: "",
      userPhone: "",
      userAboutMe: "",
      facebookUrl: "",
      instagramUrl: "",
      twitterUrl: "",
      userFakePhoto: "",
      submitStatus: null,
    };
  },
  created() {
    let _id = this.$route.params["id"];
    axios
      .get("User/Get/" + _id, {
        headers: {
          "Content-type": "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
      .then((user) => {
        if (user.status === 200) {
          this.userName = user.data.userName;
          this.userSurname = user.data.surName;
          this.userFirstName = user.data.name;
          this.userPhone = user.data.phoneNumber;
          this.userFakePhoto = user.data.photo;
          this.userAboutMe = user.data.about;
          this.userBirhtday = user.data.birthday;
          this.facebookUrl = user.data.socialMedia.facebookUrl;
          this.instagramUrl = user.data.socialMedia.instagramUrl;
          this.twitterUrl = user.data.socialMedia.twitterUrl;
          this.userId = user.data.id;
        }
      });
  },
  watch: {
    date(val) {
      this.dateFormatted = this.formatDate(this.date);
    },
  },
  methods: {
    formatDate(date) {
      if (!date) return null;

      const [year, month, day] = date.split("-");
      return `${month}/${day}/${year}`;
    },
    parseDate(date) {
      if (!date) return null;

      const [month, day, year] = date.split("/");
      return `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`;
    },
    submit() {
      this.submitStatus = "PENDING";
      let data = {
        id: this.userId.toString(),
        userName: this.userName,
        name: this.userFirstName,
        surName: this.userSurname,
        phoneNumber: this.userPhone,
        about: this.userAboutMe,
        birthday: this.userBirhtday,
        photo: this.userFakePhoto,
        socialMedia: {
          facebookUrl: this.facebookUrl,
          instagramUrl: this.instagramUrl,
          twitterUrl: this.twitterUrl,
        },
      };

      axios
        .post("User/Update/" + this.userId.toString(), data, {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
            Authorization: `Bearer ${this.$store.getters.userInfo.token}`,
          },
        })
        .then((result) => {
          if (result.status === 200) {
            this.$router.push("/");
          }
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
};
</script>
