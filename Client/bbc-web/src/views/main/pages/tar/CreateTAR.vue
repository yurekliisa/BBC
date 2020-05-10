<template>
  <v-layout>
    <v-flex>
      <v-card class="mx-auto px-5 py-5" raised :v-if="data.id">
        <form>
          <v-col cols="12">
            <v-text-field
              v-model="data.title"
              label="Tarif ve Reçete Başlık"
              required
              :error-messages="titleErrors"
              @input="$v.titleErrors.$touch()"
              @blur="$v.titleErrors.$touch()"
              solo
            >
            </v-text-field>
          </v-col>
          <v-col cols="12">
            <div class="description">
              <tiptap-vuetify
                v-model="data.description"
                :extensions="extensions"
              />
            </div>
          </v-col>
          <v-col cols="12">
            <v-select
              v-model="value"
              :items="items"
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
              v-model="data.mainImage"
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

export default {
  name: "Create-TAR",
  components: { TiptapVuetify },
  mixins: [validationMixin],
  validations: {
    title: { required },
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
    items: ["foo", "bar", "fizz", "buzz"],
    value: ["foo", "bar", "fizz", "buzz"],
    submitStatus: null,
    data: {
      description: "",
      id: 0,
    },
  }),
  computed: {
    titleErrors() {
      const errors = [];
      if (!this.$v.title.$dirty) return errors;
      !this.$v.title.required && errors.push("E-mail is required");
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
    submit() {
      console.log(this.data);
      this.$v.$touch();
      if (this.$v.$invalid) {
        this.submitStatus = "ERROR";
      } else {
        this.submitStatus = "PENDING";
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
