<template>
  <v-layout warp align-center justify-center row fill-height>
    <v-flex xs12 md12>
      <v-dialog v-model="dialog" persistent max-width="600px">
        <template v-slot:activator="{ on }">
          <v-btn outlined color="deep-purple" dark v-on="on">Create Category</v-btn>
        </template>
        <v-card>
          <v-card-title>
            <span class="headline">New Category</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12">
                  <v-text-field v-model="newCategory" label="Name*" required></v-text-field>
                </v-col>
              </v-row>
            </v-container>
            <small>*indicates required field</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false">Close</v-btn>
            <v-btn color="blue darken-1" text @click="addCategory()">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-flex>
    <v-flex xs12 md12>
      <v-data-table :headers="headers" :items="categories" :items-per-page="5">
        <template v-slot:item="row">
          <tr>
            <td style="width:200px;">
              <v-btn class="mx-2" fab dark small color="deep-purple" @click="editCategory(row.item)">
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
            <td>{{row.item.name}}</td>
          </tr>
        </template>
      </v-data-table>
    </v-flex>
  </v-layout>
</template>

<script>
export default {
  name: "Category",
  created(){
        this.$store.commit("SET_panelText", "Category");
  },
  data(){
    return{
      currentIndex: -1,
      newCategory: "",
      dialog: false,
      headers:[
        {
          text: "Action"
        },
        {
          text: "Name",
          align: "start",
          sortable: false,
          value: "name",
        },
      ],
      categories: [
        {
          name:"vegetables"
        },
        {
          name:"meaty"
        },
        {
          name:"hot"
        },
        {
          name:"before eating"
        },
        {
          name:"vegetables"
        }
      ]
    };
  },
  methods: {
    deleteCategory(item){
      let index = this.categories.findIndex(x => x.name == item.name);
      this.categories.splice(index, 1);
    },
    editCategory(item)  {
       this.newCategory = this.categories.find(c => c.name == item.name).name;
      this.currentIndex = this.categories.findIndex(c => c.name == item.name);
      this.dialog = true;
    },
    addCategory(item){
       if (this.currentIndex != -1) {
        this.categories[this.currentIndex].name= this.newCategory;
        this.currentIndex = -1;
        this.categories = [...this.categories];
      } else {
        this.categories.push({ name: this.newCategory });
      }
      this.dialog = false;
      this.newCategory = "";
    }
  }
};
</script>

<style scoped>
</style>
