<template>
  <v-layout>
    <v-flex justify-center class="customContainer">
      <v-card class="mx-auto formContainer" width="700" raised>
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
            hint="At least 8 chracters"
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
            hint="At least 8 chracters"
            counter
            @click:append="showPa = !showPa"
            @input="$v.passwordAgain.$touch()"
            @blur="$v.passwordAgain.$touch()"
            solo
          ></v-text-field>

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
    email: { required, email },
    password: { required, minLength: minLength(8) },
    passwordAgain: {
      required,
      minLength: minLength(8),
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
      showP: false,
      showPa: false,
      userName: "",
      email: "",
      password: "",
      passwordAgain: "",
      checkbox: false,
      submitStatus: null,
    };
  },
  computed: {
    userNameErrors() {
      const errors = [];
      if (!this.$v.userName.$dirty) return errors;
      !this.$v.userName.required && errors.push("User name is required");
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
        errors.push("Password must be at most 8 characters long");
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
        errors.push("Password must be at most 8 characters long");
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
  methods: {
    submit() {
      this.$v.$touch();
      if (this.$v.$invalid) {
        this.submitStatus = "ERROR";
      } else {
        this.submitStatus = "PENDING";
        axios
          .post("Auth/Register", {
            userName: this.userName,
            email: this.email,
            password: this.password,
            confirmPassword: this.confirmPassword,
          })
          .then((response) => {
            if (response.status === 200) {
              this.submitStatus = "OK";
              this.$router.push("/login");
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
