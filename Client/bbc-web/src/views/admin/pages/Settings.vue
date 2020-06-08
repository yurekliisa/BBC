<template>
  <v-layout warp align-center justify-center row fill-height style="padding:1rem">
    <v-flex xs12 md12>
      <v-btn outlined color="deep-purple" dark @click="showModal()">Create Settings</v-btn>
      <v-dialog v-model="dialog" persistent max-width="600px">
        <v-card>
          <v-card-title>
            <span class="headline">{{ title }}</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12">
                  <v-text-field v-model="setting.key" label="Key" required></v-text-field>
                  <v-text-field v-model="setting.value" label="Value" required></v-text-field>
                </v-col>
              </v-row>
            </v-container>
            <small>*indicates required field</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false">Close</v-btn>
            <v-btn color="blue darken-1" text @click="createOrEditSettings()">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-flex>
    <v-flex xs12 md12>
      <v-data-table :headers="headers" :items="settings" :items-per-page="5">
        <template v-slot:item="row">
          <tr>
            <td style="width:200px;">
              <v-btn
                class="mx-2"
                fab
                dark
                small
                color="deep-purple"
                @click="selectedSettings(row.item)"
              >
                <v-icon dark>mdi-pencil</v-icon>
              </v-btn>
              <v-btn
                class="mx-2"
                fab
                dark
                small
                color="deep-purple"
                @click="deleteSettings(row.item)"
              >
                <v-icon dark>mdi-trash-can-outline</v-icon>
              </v-btn>
            </td>
            <td>{{row.item.key}}</td>
            <td>{{row.item.value}}</td>
          </tr>
        </template>
      </v-data-table>
    </v-flex>
  </v-layout>
</template>

<script>
import axios from "axios";
export default {
  name: "Settings",
  created() {
    this.fetchData();
    this.$store.commit("SET_panelText", "Settings");
  },
  data() {
    return {
      title: "Create ",
      currentIndex: -1,
      setting: { key: "", value: "", id: 0 },
      dialog: false,
      headers: [
        {
          text: "Action"
        },
        {
          text: "Key",
          align: "start",
          sortable: false,
          value: "keys"
        },
        {
          text: "Value",
          align: "start",
          sortable: false,
          value: "values"
        }
      ],
      settings: []
    };
  },
  methods: {
    showModal() {
      this.title = "Create New Settings";
      this.settings = { key: "", value: "" };
      this.dialog = true;
    },
    selectedSettings(obj) {
      this.settings = this.settings.find(c => c.id == obj.id);
      this.title = "Edit Settings";
      this.dialog = true;
    },
    fetchData() {
      axios
        .get("Settings/GetSettings", {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
            Authorization: `Bearer ${this.$store.getters.userInfo.token}`
          }
        })
        .then(response => {
          this.settings = response.data;
          console.log(this.settings)
        });
    },
    deleteSettings(item) {
      axios
        .get("Settings/DeleteSettings?id=" + item.id, {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
            Authorization: `Bearer ${this.$store.getters.userInfo.token}`
          }
        })
        .then(response => {
          this.fetchData();
        });
    },
    createOrEditSettings() {
      if (this.setting.id !== 0) {
        this.editSettings();
      } else {
        this.addSettings();
      }
    },
    editSettings(item) {
      axios
        .post("Settings/EditSettings", this.setting, {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
            Authorization: `Bearer ${this.$store.getters.userInfo.token}`
          }
        })
        .then(response => {
          if (response.status === 200) {
            this.fetchData();
          }
        })
        .catch(function(error) {
          console.log(error);
        });
      this.dialog = false;
      this.settings = { name: "" };
    },
    addSettings(item) {
      axios
        .post("Settings/CreateSettings", this.setting, {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
            Authorization: `Bearer ${this.$store.getters.userInfo.token}`
          }
        })
        .then(response => {
          if (response.status === 200) {
            this.fetchData();
          }
        })
        .catch(function(error) {
          console.log(error);
        });

      this.dialog = false;
      this.settings = { key: "" }; //????
    }
  }
};
</script>

<style scoped>
</style>
