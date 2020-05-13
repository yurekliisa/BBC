<template>
  <div class="about">
    <h1>UserReports</h1>
    <v-btn @click.stop="changeFooter()" icon>
        <v-icon>mdi-application</v-icon>
      </v-btn>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "UserReports",
  created(){
    this.fetchData();
    this.$store.commit("SET_panelText", "UserReports");
  },
  data(){
    return {
      userReport[],
    };
  },
  mounted(){
    this.fetchData();
  },
  methods:{
    fetchData(){
      axios 
      .get ("/User/UserReport",{
        headers:{
          "Content-type": "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
      .then((result)=>{
        console.log(result);
        if(result.status===200){
          this.recentCat = result.data;
        }
      })
      .catch((err)=>{
        console.log (err);
    });
  }
  }
};
</script>

<style scoped>
.test{
  height: 1024px;
  background-color: blue;
}
</style>
