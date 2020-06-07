<template>
  <v-layout>
    <v-flex justify-center class="customContainer">
      <v-card class="mx-auto formContainer" width="700" raised>
        <form>
          <v-text-field
            v-model="email"
            label="E-mail"
            required
            :error-messages="emailErrors"
            @input="$v.email.$touch()"
            @blur="$v.email.$touch()"
            solo
          >
          </v-text-field>
          <v-text-field
            v-model="password"
            :error-messages="passwordErrors"
            :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
            :type="show ? 'text' : 'password'"
            name="password"
            hint="At least 8 chracters"
            counter
            label="Password"
            @click:append="show = !show"
            @input="$v.password.$touch()"
            @blur="$v.password.$touch()"
            solo
          >
          </v-text-field>

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
              >Giriş</v-btn
            >
          </v-card-actions>
        </form>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required, minLength, email } from "vuelidate/lib/validators";
import axios from "axios";

export default {
  mixins: [validationMixin],
  validations: {
    password: { required, minLength: minLength(6) },
    email: { required, email },
    checkbox: {
      checked(val) {
        return val;
      },
    },
  },
  data() {
    return {
      show: false,
      password: "",
      email: "",
      checkbox: false,
      submitStatus: null,
    };
  },

  computed: {
    checkboxErrors() {
      const errors = [];
      if (!this.$v.checkbox.$dirty) return errors;
      !this.$v.checkbox.checked && errors.push("You must agree to continue!");
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
  },
  methods: {
    submit() {
      this.$v.$touch();
      if (this.$v.$invalid) {
        this.submitStatus = "ERROR";
      } else {
        this.submitStatus = "PENDING";
        axios
          .post(
            "Auth/Login",
            {
              email: this.email,
              password: this.password,
            },
            {
              headers: {
                "Content-type": "application/json",
                "Access-Control-Allow-Origin": "http:localhost:8080/",
              },
            }
          )
          .then((response) => {
            if (response.status === 200) {
              this.submitStatus = "OK";
              localStorage.setItem("user", JSON.stringify(response.data));
              this.$store.commit("SET_USER", response.data);
              this.$router.push("/");
            }
          })
          .catch(function(error) {
            this.submitStatus = "ERROR";
          });
      }
    },
  },
};
</script>

<style lang="sass">
.login
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
