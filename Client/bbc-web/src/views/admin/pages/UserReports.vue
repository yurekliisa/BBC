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
   
    this.$store.commit("SET_panelText", "UserReports");
  },
  data(){
    return {
     
    };
  },
  
 
  
};
</script>

<style scoped>
.test{
  height: 1024px;
  background-color: blue;
}
</style>
