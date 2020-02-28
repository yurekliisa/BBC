<template>
  <v-footer :absolute="$store.getters.fixed" app>
    <span>&copy; {{$store.getters.footer}}</span>
  </v-footer>
</template>

<script>
export default {
  name: "Footer",
};
</script>
