<template>
  <v-layout>
    <v-flex>
      <v-card class="mx-auto px-5 py-5" raised>
        <v-col cols="12">
          <h1 style="text-align:center">Reçete ve Tarif Oluşturma</h1>
        </v-col>
        <form>
          <v-col cols="12">
            <v-text-field
              v-model="title"
              label="Tarif ve Reçete Başlık"
              required
              :error-messages="titleErrors"
              @input="$v.title.$touch()"
              @blur="$v.title.$touch()"
              solo
            >
            </v-text-field>
          </v-col>
          <v-col cols="12">
            <v-text-field
              v-model="shortDescription"
              label="İçeriği özetlermisiniz"
              required
              :error-messages="shortDescriptionErrors"
              @input="$v.shortDescription.$touch()"
              @blur="$v.shortDescription.$touch()"
              solo
            >
            </v-text-field>
          </v-col>
          <v-col cols="12">
            <div class="description">
              <tiptap-vuetify v-model="contentText" :extensions="extensions" />
            </div>
          </v-col>
          <v-col cols="12">
            <v-select
              v-model="selectedCategories"
              :items="categories"
              item-text="name"
              item-value="id"
              chips
              label="Kategori"
              multiple
              solo
            ></v-select>
          </v-col>
          <v-col cols="12">
            <v-file-input
              accept="image/*"
              label="Kapak resmi"
              v-model="mainImage"
            ></v-file-input>
          </v-col>
          <v-card-actions style="justify-content:center;">
            <v-btn
              width="100"
              outlined
              color="primary"
              @click="submit"
              :disabled="submitStatus === 'PENDING'"
              >Oluştur</v-btn
            >
          </v-card-actions>
        </form>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import {
  TiptapVuetify,
  // extensions
  Heading,
  Bold,
  Italic,
  Strike,
  Underline,
  Code,
  Paragraph,
  BulletList,
  OrderedList,
  ListItem,
  Link,
  Blockquote,
  HardBreak,
  HorizontalRule,
  History,
  Image,
} from "tiptap-vuetify";
import { validationMixin } from "vuelidate";
import { required, minLength, email } from "vuelidate/lib/validators";
import axios from "axios";
import DomParser from "dom-parser";
import FormData from "form-data";

export default {
  name: "Create-TAR",
  components: { TiptapVuetify },
  mixins: [validationMixin],
  validations: {
    title: { required },
    shortDescription: { required },
  },
  data: () => ({
    extensions: [
      History,
      Blockquote,
      Link,
      Underline,
      Strike,
      Italic,
      ListItem,
      BulletList,
      OrderedList,
      Image,
      [
        Heading,
        {
          options: {
            levels: [1, 2, 3],
          },
        },
      ],
      Bold,
      Link,
      Code,
      HorizontalRule,
      Paragraph,
      HardBreak,
    ],
    categories: [],
    submitStatus: null,
    title: "",
    shortDescription: "",
    mainImage: undefined,
    selectedCategories: [],
    contentText: "",
  }),
  mounted() {
    let userData = JSON.parse(localStorage.getItem("user"));
    if (userData) {
      axios
        .get("/TaR/Create", {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
            Authorization: `Bearer ${userData.token}`,
          },
        })
        .then((categories) => {
          if (categories.status === 200) {
            this.categories = categories.data.categories;
          }
        });
    }
  },
  computed: {
    titleErrors() {
      const errors = [];
      if (!this.$v.title.$dirty) return errors;
      !this.$v.title.required && errors.push("Title is required");
      return errors;
    },
    shortDescriptionErrors() {
      const errors = [];
      if (!this.$v.shortDescription.$dirty) return errors;
      !this.$v.shortDescription.required &&
        errors.push("shortDescription is required");
      return errors;
    },
  },
  watch: {
    multiple(val) {
      if (val) this.model = [this.model];
      else this.model = this.model[0] || "Foo";
    },
  },
  methods: {
    test() {
      var parser = new DomParser();
      var parsedHtml = parser.parseFromString(this.content, "text/html");
      let pTags = parsedHtml.getElementsByTagName("img");
      pTags.forEach(function(item) {
        let attributeObject = item.attributes.find((x) => x.name === "src");
      });
    },
    submit(event) {
      let data = new FormData();
      for (let i = 0; i < this.selectedCategories.length; i++) {
        const element = this.selectedCategories[i];
        data.append("Categories[" + i + "]", element);
      }
      data.append("Content.Title", this.title);
      data.append("Content.MainImage", this.mainImage);
      data.append("Content.ContentText", this.contentText);
      data.append("Content.shortDescription", this.shortDescription);
      this.$v.$touch();
      if (this.$v.$invalid) {
        this.submitStatus = "ERROR";
      } else {
        this.submitStatus = "PENDING";
        axios
          .post("/TaR/CreateTaR", data, {
            headers: {
              "Content-type": "multipart/form-data",
              "Access-Control-Allow-Origin": "*",
              Authorization: `Bearer ${this.$store.getters.userInfo.token}`,
            },
          })
          .then((response) => {
            if (response.status === 200) {
              this.$router.push("/");
            }
          })
          .catch(function(error) {
            console.log(error);
          });
        this.submitStatus = "OK";
      }
    },
  },
};
</script>

<style lang="sass">
.description
  img
    width: 450px
    height: 300px
    display: flex
    margin: auto
    margin-top: 1rem
    margin-bottom: 1rem
</style>
