<template>
  <v-card>
    <v-footer v-bind="localAttrs" :padless="true">
      <v-card flat tile width="100%" class="red lighten-1 text-center">
        <v-card-text class="white--text">
          <div style="line-height:36px;float:left;padding-bottom:10px;">
            <v-btn v-for="icon in icons" :key="icon" icon>
              <v-icon>{{ icon }}</v-icon>
            </v-btn>
          </div>
          <div style="line-height:36px;float:right;padding-bottom:10px;">
            {{ new Date().getFullYear() }} —
            <strong>Bana Bi' Chef</strong>
          </div>
        </v-card-text>
      </v-card>
    </v-footer>
  </v-card>
</template>

<script>
export default {
  name: "Footer",
  data() {
    return {
      icons: ["mdi-home", "mdi-email", "mdi-instagram", "mdi-facebook"]
    };
  },
  computed: {
    localAttrs() {
      const attrs = {};
      if (this.variant === "default") {
        attrs.absolute = false;
        attrs.fixed = false;
      } else {
        attrs[this.variant] = true;
      }
      return attrs;
    }
  }
};
</script>

<style></style>
