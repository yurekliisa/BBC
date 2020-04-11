<template>
  <v-layout warp align-center justify-center row fill-height>
    <v-flex xs12 md12>
      <v-dialog v-model="dialog" persistent max-width="600px">
        <template v-slot:activator="{ on }">
          <v-btn outlined color="deep-purple" dark v-on="on">CREATE SETTİNGS</v-btn>
        </template>
        <v-card>
          <v-card-title>
            <span class="headline">New Settings</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12">
                  <v-text-field v-model="newSettings" label="Key" required></v-text-field>
                  <v-text-field v-model="newSettings" label="Value" required></v-text-field>
                </v-col>
              </v-row>
            </v-container>
            <small>*indicates required field</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false">Close</v-btn>
            <v-btn color="blue darken-1" text @click="addSettings()">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-flex>
    <v-flex xs12 md12>
      <v-data-table :headers="headers" :items="settings" :items-per-page="5">
        <template v-slot:item="row">
          <tr>
            <td style="width:200px;">
              <v-btn class="mx-2" fab dark small color="deep-purple" @click="editSettings(row.item)">
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
          </tr>
        </template>
      </v-data-table>
    </v-flex>
  </v-layout>
</template>

<script>
export default {
  name: "Settings",
  created(){
        this.$store.commit("SET_panelText", "Settings");
  },
  data(){
    return{
      currentIndex: -1,
      newSettings: "",
      dialog: false,
      headers:[
        {
          text: "Action"
        },
        {
          text: "Key",
          align: "start",
          sortable: false,
          value: "id",
        },
        {
          text: "Value",
          align: "start",
          sortable: false,
          value: "idValue",
        },
      ],
      keys: [
        {
          id:"000001"
        },
        {
          id:"000002"
        },
        {
          id:"000003"
        },
        {
          id:"000004"
        },
        {
          id:"000005"
        }
      ],
       values: [
        {
          idValue:"12123"
        },
        {
          idValue:"12124"
        },
        {
          idValue:"12125"
        },
        {
          idValue:"12126"
        },
        {
          idValue:"12127"
        }
      ]
    };
  },
  methods: {
    deleteSettings(item){
      let index = this.keys.findIndex(x => x.id == item.id);
      this.keys.splice(index, 1);
    },
    editSettings(item)  {
       this.newSettings = this.keys.find(c => c.id == item.id).id;
      this.currentIndex = this.keys.findIndex(c => c.id == item.id);
      this.dialog = true;
    },
    addSettings(item){
       if (this.currentIndex != -1) {
        this.keys[this.currentIndex].id= this.newSettings;
        this.currentIndex = -1;
        this.keys = [...this.keys];
      } else {
        this.keys.push({ id: this.newSettings });
      }
      this.dialog = false;
      this.newSettings = "";
    }
  }
};
</script>

<style scoped>
</style>
