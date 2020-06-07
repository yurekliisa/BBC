<template>
  <div class="mt-4">
    <!--Widget 1 -->
    <h1 class="homeTitle">Popüler Chef</h1>
    <v-row>
      <v-flex>
        <v-tabs>
          <v-tabs-slider></v-tabs-slider>
          <v-tab
            v-for="i in ['Günlük', 'Haftalık', 'Aylık']"
            :key="i"
            :href="`#tab-${i}`"
          >
            {{ i }}
          </v-tab>
          <v-tab-item
            v-for="i in ['Günlük', 'Haftalık', 'Aylık']"
            :key="i"
            :value="'tab-' + i"
          >
            <v-card class="ma-1 pa-1" style="width:100%">
              <v-flex v-for="(item, i) in  recentChefs" :key="i">
                <v-list-item :to="'/profile/'+item.id" style="cursor:pointer">
                  <v-list-item-avatar>
                    <v-img
                      :src="'https://bbc-api.azurewebsites.net/'+item.photo"
                    ></v-img>
                  </v-list-item-avatar>
                  <v-list-item-content>
                    <v-list-item-title style="text-align:center;"
                      >{{item.fullName}}</v-list-item-title
                    >
                  </v-list-item-content>
                </v-list-item>
              </v-flex>
            </v-card>
          </v-tab-item>
        </v-tabs>
      </v-flex>
    </v-row>
    <!-- Widget 1-->
  </div>
</template>

<script>
import { rotData } from "../../assets/data/data";
import axios from "axios";

export default {
 /* [
  {
    "fullName": "string",
    "photo": "string",
    "id": 0
  }
]*/
  name: "PopularChef",
  data(){
    return{
      recentChefs:[]
    };
  },
  mounted(){
    this.fetchData();
  },
  methods:{
    
    
    fetchData(){
      axios
      .get("/Home/GetPopularChefs",{
        headers:{
         "Content-type": "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
      .then((result)=>{
        console.log(result)
        if (result.status===200){
          this.recentChefs=result.data;
        }
      })
      .catch((err)=>{
        console.log(err);
      });
    }
  }
};
</script>
