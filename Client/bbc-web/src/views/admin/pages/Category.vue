<template>
  <v-layout warp align-center justify-center row fill-height>
    <v-flex xs12 md12>
      <v-btn outlined color="deep-purple" dark @click="showModal()"
        >Create Category</v-btn
      >
      <v-dialog v-model="dialog" persistent max-width="600px">
        <v-card>
          <v-card-title>
            <span class="headline">{{ title }}</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-form>
                <v-row>
                  <v-col cols="12">
                    <v-text-field
                      v-model="category.name"
                      label="Name*"
                      required
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-form>
            </v-container>
            <small>*indicates required field</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false"
              >Close</v-btn
            >
            <v-btn color="blue darken-1" text @click="createOrEditCategory()"
              >Save</v-btn
            >
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-flex>
    <v-flex xs12 md12 v-if="categories.length > 0">
      <v-data-table :headers="headers" :items="categories" :items-per-page="5">
        <template v-slot:item="row">
          <tr>
            <td style="width:200px;">
              <v-btn
                class="mx-2"
                fab
                dark
                small
                color="deep-purple"
                @click="selectedCategory(row.item)"
              >
                <v-icon dark>mdi-pencil</v-icon>
              </v-btn>

              <v-btn
                class="mx-2"
                fab
                dark
                small
                color="deep-purple"
                @click="deleteCategory(row.item)"
              >
                <v-icon dark>mdi-trash-can-outline</v-icon>
              </v-btn>
            </td>

            <td>{{ row.item.name }}</td>
          </tr>
        </template>
      </v-data-table>
    </v-flex>
  </v-layout>
</template>

<script>
import axios from "axios";
export default {
  name: "Category",
  created() {
    this.fetchData();
    this.$store.commit("SET_panelText", "Category");
  },
  data() {
    return {
      title: "Create ",
      currentIndex: -1,
      category: { name: "", id: 0 },
      dialog: false,
      headers: [
        {
          text: "Action"
        },
        {
          text: "Name",
          align: "start",
          sortable: false,
          value: "name"
        }
      ],
      categories: []
    };
  },
  methods: {
    showModal() {
      this.title = "Create New Category";
      this.category = { name: "", id: 0 };
      this.dialog = true;
    },
    selectedCategory(value) {
      this.category = this.categories.find(c => c.id == value.id);
      this.title = "Edit Category";
      this.dialog = true;
    },
    fetchData() {
      axios
        .get(
          "https://localhost:44308/api/Category/GetAllCategories",
          {},
          {
            headers: {
              "Content-type": "application/json",
              "Access-Control-Allow-Origin": "*"
            }
          }
        )
        .then(response => {
          this.categories = response.data;
        });
    },
    deleteCategory(value) {
      axios
        .get(
          "https://localhost:44308/api/Category/Delete?id=" + value.id,
          {},
          {
            headers: {
              "Content-type": "application/json",
              "Access-Control-Allow-Origin": "*"
            }
          }
        )
        .then(response => {
          this.fetchData();
        });
    },
    createOrEditCategory(){
      if(this.category.id !== 0)
      {
        this.editCategory();
      }
      else{
        this.addCategory();
      }
    },
    editCategory() {
      axios
        .post("https://localhost:44308/api/Category/Edit", this.category)
        .then(response => {
          if (response.status === 200) {
            this.fetchData();
          }
        })
        .catch(function(error) {
          console.log(error);
        });
      this.dialog = false;
      this.categories = { name: "" };
    },
    addCategory() {
      axios
        .post("https://localhost:44308/api/Category/Create", this.category)
        .then(response => {
          if (response.status === 200) {
            this.fetchData();
          }
        })
        .catch(function(error) {
          console.log(error);
        });
      this.dialog = false;
      this.categories = { name: "" };
    }
  }
};
</script>

<style scoped></style>
