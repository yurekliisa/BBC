<template>
  <v-layout warp align-center justify-center row fill-height>
    <v-flex xs12 md12>
      <v-btn outlined color="deep-purple" dark @click="showModal()"
        >Create Lobby</v-btn
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
                      v-model="lobby.name"
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
            <v-btn color="blue darken-1" text @click="createLobby()"
              >Save</v-btn
            >
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-flex>
    <v-flex xs12 md12 v-if="lobbies.length > 0">
      <v-data-table :headers="headers" :items="lobbies" :items-per-page="5">
        <template v-slot:item="row">
          <tr>
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
  name: "Lobby",
  created() {
    this.fetchData();
    this.$store.commit("SET_panelText", "Lobby");
  },
  data() {
    return {
      title: "Create ",
      currentIndex: -1,
      lobby: { name: "", id: 0 },
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
      lobbies: []
    };
  },
  methods: {
    showModal() {
      this.title = "Create New Lobby";
      this.lobby = { name: "", id: 0 };
      this.dialog = true;
    },
    
    fetchData() {
      axios
        .get(
          "Category/GetAllLobbies",
          {
            headers: {
              "Content-type": "application/json",
              "Access-Control-Allow-Origin": "*"
            }
          }
        )
        .then(response => {
          this.lobbies = response.data;
        });
    },
    
   
    createLobby() {
      axios
        .post("Lobby/Create", this.lobby)
        .then(response => {
          if (response.status === 200) {
            this.fetchData();
          }
        })
        .catch(function(error) {
          console.log(error);
        });
      this.dialog = false;
      this.lobby = { name: "" };
    }
  }
};
</script>

<style scoped></style>
