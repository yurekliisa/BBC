<template>
  <div class="about">
    <h1>Category</h1>
    <v-btn @click.stop="changeFooter()" icon>
        <v-icon>mdi-application</v-icon>
      </v-btn>
  </div>
</template>

<script>
export default {
  name: "Category",
  created(){
        this.$store.commit("SET_panelText", "Category");
  }
};
</script>

<style scoped>
.test{
  height: 1024px;
  background-color: red;
}
</style>
