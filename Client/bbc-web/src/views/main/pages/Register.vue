<template>
  <v-layout>
    <v-flex justify-center class="customContainer">
      <v-card class="mx-auto formContainer" width="700" raised>
        <v-col cols="12">
          <h1 style="text-align:center">Kayıt Ol</h1>
        </v-col>
        <form>
          <v-text-field
            v-model="userName"
            :error-messages="userNameErrors"
            label="User name"
            required
            @input="$v.userName.$touch()"
            @blur="$v.userName.$touch()"
            solo
          ></v-text-field>

          <v-text-field
            v-model="email"
            :error-messages="emailErrors"
            label="E-mail"
            required
            @input="$v.email.$touch()"
            @blur="$v.email.$touch()"
            solo
          ></v-text-field>

          <v-text-field
            v-model="password"
            :error-messages="passwordErrors"
            :append-icon="showP ? 'mdi-eye' : 'mdi-eye-off'"
            :type="showP ? 'text' : 'password'"
            label="Password"
            name="password"
            hint="At least 6 chracters"
            counter
            @click:append="showP = !showP"
            @input="$v.password.$touch()"
            @blur="$v.password.$touch()"
            solo
          ></v-text-field>

          <v-text-field
            v-model="passwordAgain"
            :error-messages="passwordAgainErrors"
            :append-icon="showPa ? 'mdi-eye' : 'mdi-eye-off'"
            :type="showPa ? 'text' : 'password'"
            label="Password Again"
            name="passwordAgain"
            hint="At least 6 chracters"
            counter
            @click:append="showPa = !showPa"
            @input="$v.passwordAgain.$touch()"
            @blur="$v.passwordAgain.$touch()"
            solo
          ></v-text-field>

          <v-text-field
            v-model="phone"
            :error-messages="phoneErrors"
            label="Phone Number"
            required
            @input="$v.phone.$touch()"
            @blur="$v.phone.$touch()"
            solo
          ></v-text-field>

          <v-text-field
            v-model="name"
            :error-messages="nameErrors"
            label="Name"
            required
            @input="$v.name.$touch()"
            @blur="$v.name.$touch()"
            solo
          ></v-text-field>

          <v-text-field
            v-model="surname"
            :error-messages="surnameErrors"
            label="Surname"
            required
            @input="$v.surname.$touch()"
            @blur="$v.surname.$touch()"
            solo
          ></v-text-field>
          <v-col cols="12">
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
                  @blur="birthday = parseDate(dateFormatted)"
                  v-on="on"
                ></v-text-field>
              </template>
              <v-date-picker
                v-model="birthday"
                no-title
                @input="menu1 = false"
              ></v-date-picker>
            </v-menu>
          </v-col>

          <v-checkbox
            v-model="checkbox"
            label="Robot Değilim"
            required
            @change="$v.checkbox.$touch()"
            @blur="$v.checkbox.$touch()"
            :error-messages="checkboxErrors"
            solo
          ></v-checkbox>

          <v-card-actions style="justify-content:center;">
            <v-btn
              width="100"
              outlined
              color="primary"
              @click="submit"
              :disabled="submitStatus === 'PENDING'"
            >
              Register</v-btn
            >
          </v-card-actions>
        </form>
      </v-card>
    </v-flex>
  </v-layout>
</template>
<script>
import { validationMixin } from "vuelidate";
import { required, minLength, email, sameAs } from "vuelidate/lib/validators";
import axios from "axios";

export default {
  mixins: [validationMixin],
  validations: {
    userName: { required },
    phone: { required },
    name: { required },
    surname: { required },
    email: { required, email },
    password: { required, minLength: minLength(6) },
    passwordAgain: {
      required,
      minLength: minLength(6),
      sameAsPassword: sameAs("password"),
    },
    checkbox: {
      checked(val) {
        return val;
      },
    },
  },
  data() {
    return {
      birthday: new Date().toISOString().substr(0, 10),
      dateFormatted: this.formatDate(new Date().toISOString().substr(0, 10)),
      menu1: false,
      menu2: false,
      showP: false,
      showPa: false,
      userName: "",
      surname: "",
      name: "",
      phone: "",
      email: "",
      password: "",
      passwordAgain: "",
      checkbox: false,
      submitStatus: null,
    };
  },
  computed: {
    computedDateFormatted() {
      return this.formatDate(this.date);
    },
    userNameErrors() {
      const errors = [];
      if (!this.$v.userName.$dirty) return errors;
      !this.$v.userName.required && errors.push("User name is required");
      return errors;
    },
    phoneErrors() {
      const errors = [];
      if (!this.$v.phone.$dirty) return errors;
      !this.$v.phone.required && errors.push("Phone is required");
      return errors;
    },
    nameErrors() {
      const errors = [];
      if (!this.$v.name.$dirty) return errors;
      !this.$v.name.required && errors.push("Name is required");
      return errors;
    },
    surnameErrors() {
      const errors = [];
      if (!this.$v.surname.$dirty) return errors;
      !this.$v.surname.required && errors.push("Surname is required");
      return errors;
    },
    emailErrors() {
      const errors = [];
      if (!this.$v.email.$dirty) return errors;
      !this.$v.email.email && errors.push("Must be valid e-mail");
      !this.$v.email.required && errors.push("E-mail is required");
      return errors;
    },
    passwordErrors() {
      const errors = [];
      if (!this.$v.password.$dirty) {
        return errors;
      }
      !this.$v.password.minLength &&
        errors.push("Password must be at most 6 characters long");
      !this.$v.password.required && errors.push("Password is required.");
      return errors;
    },
    passwordAgainErrors() {
      const errors = [];
      if (!this.$v.passwordAgain.$dirty) {
        return errors;
      }
      !this.$v.passwordAgain.sameAsPassword &&
        errors.push("Şifreler aynı olmak zorundadır.");
      !this.$v.passwordAgain.minLength &&
        errors.push("Password must be at most 6 characters long");
      !this.$v.passwordAgain.required && errors.push("Password is required.");
      return errors;
    },
    checkboxErrors() {
      const errors = [];
      if (!this.$v.checkbox.$dirty) return errors;
      !this.$v.checkbox.checked && errors.push("You must agree to continue!");
      return errors;
    },
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
      this.$v.$touch();

      if (this.$v.$invalid) {
        this.submitStatus = "ERROR";
      } else {
        this.submitStatus = "PENDING";
        let data = {
          userName: this.userName,
          email: this.email,
          password: this.password,
          confirmPassword: this.passwordAgain,
          phoneNumber: this.phone,
          name: this.name,
          surName: this.surname,
          birthday: this.birthday,
        };
        axios
          .post("Auth/Register", data)
          .then((response) => {
            if (response.status === 200) {
              this.submitStatus = "OK";
              this.$router.push("/login");
            } else {
              this.submitStatus = "ERROR";
            }
          })
          .catch(function(error) {
            console.log(error);
          });
      } 
    },
  },
};
</script>

<style lang="sass">
.register
  height: 1000px
.customContainer
  height: calc(100vh - (64px + 8px + 71px))
  align-items: center
  display: flex
  .formContainer
    flex-direction: column
    display: flex
    padding: 2rem
</style>
