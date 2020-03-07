<template>
  <v-layout warp align-center justify-center row fill-height>
    <v-flex xs12 md12>
      <v-dialog v-model="dialog" persistent max-width="600px">
        <template v-slot:activator="{ on }">
          <v-btn outlined color="deep-purple" dark v-on="on">Create Country</v-btn>
        </template>
        <v-card>
          <v-card-title>
            <span class="headline">New Country</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12">
                  <v-text-field v-model="newCountry" label="Name*" required></v-text-field>
                </v-col>
              </v-row>
            </v-container>
            <small>*indicates required field</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false">Close</v-btn>
            <v-btn color="blue darken-1" text @click="addCountry()">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-flex>
    <v-flex xs12 md12>
      <v-data-table :headers="headers" :items="countries" :items-per-page="5">
        <template v-slot:item="row">
          <tr>
            <td style="width:200px;">
              <v-btn class="mx-2" fab dark small color="deep-purple" @click="editCountry(row.item)">
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
export default {
  name: "Country",
  created() {
    this.$store.commit("SET_panelText", "Country");
  },
  data() {
    return {
      currentIndex: -1,
      newCountry: "",
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
      countries: [
        {
          name: "Tr"
        },
        {
          name: "En"
        },
        {
          name: "Tr"
        },
        {
          name: "En"
        },
        {
          name: "Tr"
        },
        {
          name: "En"
        },
        {
          name: "Tr"
        },
        {
          name: "En"
        }
      ]
    };
  },
  methods: {
    deleteCountry(item) {
      let index = this.countries.findIndex(x => x.name == item.name);
      this.countries.splice(index, 1);
    },
    editCountry(item) {
      this.newCountry = this.countries.find(c => c.name == item.name).name;
      this.currentIndex = this.countries.findIndex(c => c.name == item.name);
      this.dialog = true;
    },
    addCountry() {
      if (this.currentIndex != -1) {
        this.countries[this.currentIndex].name= this.newCountry;
        this.currentIndex = -1;
        this.countries = [...this.countries];
      } else {
        this.countries.push({ name: this.newCountry });
      }
      this.dialog = false;
      this.newCountry = "";
    }
  }
};
</script>

<style scoped>
</style>
