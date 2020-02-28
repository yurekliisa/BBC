<template>
  <div class="about">
    <h1>Home</h1>
    <div class="test"></div>
  </div>
</template>

<script>
export default {
  name: "AdminHome",
  created(){
        this.$store.commit("SET_panelText", "Home");
  }
};
</script>

<style scoped>
.test{
  height: 1024px;
  background-color: red;
}
</style>
