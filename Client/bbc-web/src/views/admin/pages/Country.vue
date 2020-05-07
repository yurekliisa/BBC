<template>
  <v-layout warp align-center justify-center row fill-height>
    <v-flex xs12 md12>
       <v-btn outlined color="deep-purple" dark @click="showModal()"
          >Create Country</v-btn>
      <v-dialog v-model="dialog" persistent max-width="600px">
       <v-card>
          <v-card-title>
            <span class="headline">{{ title }}</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12">
                  <v-text-field 
                  v-model="country.name" 
                  label="Name*" 
                  required
                  ></v-text-field>
                </v-col>
              </v-row>
            </v-container>
            <small>*indicates required field</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false"
            >Close</v-btn>
            <v-btn color="blue darken-1" text @click="createOrEditCountry()"
            >Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-flex>
    <v-flex xs12 md12>
      <v-data-table :headers="headers" :items="countries" :items-per-page="5">
        <template v-slot:item="row">
          <tr>
            <td style="width:200px;">
              <v-btn class="mx-2"
                fab 
                dark 
                small 
                color="deep-purple" 
                @click="selectedCountry(row.item)">
                <v-icon dark>mdi-pencil</v-icon>
              </v-btn>
              <v-btn
                class="mx-2"
                fab
                dark
                small
                color="deep-purple"
                @click="deleteCountry(row.item)"
              >
                <v-icon dark>mdi-trash-can-outline</v-icon>
              </v-btn>
            </td>
            <td>{{row.item.name}}</td>
          </tr>
        </template>
      </v-data-table>
    </v-flex>
  </v-layout>
</template>

<script>
import axios from "axios";
export default {
  name: "Country",
  created() {
    this.$store.commit("SET_panelText", "Country");
  },
  data() {
    return {
      title: "Create ",
      currentIndex: -1,
      country:{name: "", id:0},
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
      countries: []
    };
  },
  methods: {
    showModal() {
      this.title = "Create New Country";
      this.country = { name: "", id: 0 };
      this.dialog = true;
    },
    selectedCountry(value) {
      this.country = this.countries.find(c => c.id == value.id);
      this.title = "Edit Country";
      this.dialog = true;
    },
    fetchData() {
      axios
        .get(
          "http://localhost:5000/api/Country/GetAllCountries",
          {},
          {
            headers: {
              "Content-type": "application/json",
              "Access-Control-Allow-Origin": "*"
            }
          }
        )
        .then(response => {
          this.countries = response.data;
        });
    },
    deleteCountry(item) {
       axios
        .get(
          "http://localhost:5000/api/Country/Delete?id=" + value.id,
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
     createOrEditCountry(){
      if(this.country.id !== 0)
      {
        this.editCountry();
      }
      else{
        this.addCountry();
      }
    },
    editCountry(item) {
      axios
        .post("http://localhost:5000/api/Country/Edit", this.country)
        .then(response => {
          if (response.status === 200) {
            this.fetchData();
          }
        })
        .catch(function(error) {
          console.log(error);
        });
      this.dialog = false;
      this.countries = { name: "" };
    },
    addCountry() {
     axios
        .post("http://localhost:5000/api/Country/Create", this.country)
        .then(response => {
          if (response.status === 200) {
            this.fetchData();
          }
        })
        .catch(function(error) {
          console.log(error);
        });
      this.dialog = false;
      this.countries ={name:""};
    }
  }
};
</script>

<style scoped></style>
