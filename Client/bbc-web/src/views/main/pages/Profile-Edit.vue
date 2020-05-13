<template>
  <v-layout>
    <v-flex>
      <v-card class="mx-auto formContainer" width="900" raised>
        <v-col cols="12">
          <h1 style="justify-content: center">Profilini Düzenle</h1>
        </v-col>
        <form>
          <v-col cols="12">
            <v-text-field
              v-model="userName"
              :error-messages="userNameErrors"
              label="User name"
              required
              @input="$v.userName.$touch()"
              @blur="$v.userName.$touch()"
              solo
            ></v-text-field>
            <v-row>
              <v-col cols="6">
                <v-text-field
                  v-model="firstName"
                  :error-messages="userNameErrors"
                  label="First Name"
                  required
                  @input="$v.userFirstName.$touch()"
                  @blur="$v.userFirstName.$touch()"
                  solo
                ></v-text-field>
              </v-col>
              <v-col cols="6">
                <v-text-field
                  v-model="surname"
                  :error-messages="userSurnameErrors"
                  label="Surname"
                  required
                  @input="$v.userSurname.$touch()"
                  @blur="$v.userSurname.$touch()"
                  solo
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="6">
                <v-text-field
                  v-model="userPhone"
                  :error-messages="userPhoneErrors"
                  label="Phone Number"
                  required
                  @input="$v.userPhone.$touch()"
                  @blur="$v.userPhone.$touch()"
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
                  <v-date-picker v-model="userBirthday" no-title @input="menu1 = false"></v-date-picker>
                </v-menu>
              </v-col>
            </v-row>
            <v-textarea
              v-model="aboutMe"
              :error-messages="userAboutMeErors"
              label="About me"
              @input="$v.userAboutMe.$touch()"
              @blur="$v.userAboutMe.$touch()"
              solo
            ></v-textarea>
            <v-card-actions style="justify-content:center;">
              <v-btn
                width="100"
                outlined
                color="primary"
                @click="submit"
                :disabled="submitStatus === 'PENDING'"
              >Submit</v-btn>
            </v-card-actions>
          </v-col>
        </form>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import { validationMixin } from "vuelidate";
import {required} from "vuelidate/lib/validators";
import axios from "axios";
export default {
  // mixins:[validationsMixin],
//   validations:{
//     userName:{required},
//     userName:{required},
//     userSurname:{required},
//     userPhone:{required}
      // userBirthday:{required}
//   },
data(){
  return{
    userBirhtday:new Date().toISOString().substr(0, 10),
    dateFormatted: this.formatDate(new Date().toISOString().substr(0, 10)),
    menu1: false,
    userName: "",
    userFirstName:"",
    userSurname:"",
    userPhone:"",
    userAboutMe:"",
    submitStatus:null
  };
},
watch: {
    date(val) {
      this.dateFormatted = this.formatDate(this.date);
    },
  },
  methods:{
    formatDate(date){
      if (!date)return null;

      const [year, month, day] = date.split("-");
      return `${month}/${day}/${year}`;
    },
    parseDate(date) {
      if (!date) return null;

      const [month, day, year] = date.split("/");
      return `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`;
    },
     submit() {
      this.$v.$touch();

      if (this.$v.$invalid) {
        this.submitStatus = "ERROR";
      } else {
        this.submitStatus = "PENDING";
        let data = {
          userName: this.userName,
          userFirstName: this.userFirstName,
          userSurname: this.userSurname,
          userPhone: this.userPhone,
          userAboutMe: this. userAboutMe ,
          userBirhtday: this.userBirhtday,
        };
        // axios
        //    .post("Auth/Register", data)
        //    .then((response) => {
        //      if (response.status === 200) {
        //        this.submitStatus = "OK";
        //        this.$router.push("/login");
        //     } else {
        //        this.submitStatus = "ERROR";
        //      }
        //    })
        //   .catch(function(error) {
        //      console.log(error);
        //   });
  
 };
</script>

<style></style>
