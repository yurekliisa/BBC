<template>
  <div class="qweqwe">
    {{ $route.params.id }}
  </div>
</template>

<script>
import { rotData } from "../../../assets/data/data";
export default {
  name: "rotdetail",
  created() {
    let data = rotData.filter(x=>x.id === this.$route.params.id);
    console.log(data);
  }
};
</script>

<style>
.qweqwe {
  height: 1000px;
  background-color: red;
}
</style>
