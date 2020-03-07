<template>
  <div class="about">
    <h1>UserReports</h1>
    <v-btn @click.stop="changeFooter()" icon>
        <v-icon>mdi-application</v-icon>
      </v-btn>
  </div>
</template>

<script>
export default {
  name: "UserReports",
  created(){
        this.$store.commit("SET_panelText", "UserReports");
  }
};
</script>

<style scoped>
.test{
  height: 1024px;
  background-color: blue;
}
</style>
