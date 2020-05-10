<template>
  <router-view />
</template>

<script>
import axios from "axios";
export default {
  name: "App",
  created() {
    const user = JSON.parse(localStorage.getItem("user"));
    if (user !== null) {
      const userData = user;
      axios
        .get("Manage/UserInfo", {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
            Authorization: `Bearer ${userData.token}`,
          },
        })
        .then((user) => {
          console.log("aq")
          console.log(user);
          this.$store.commit("SET_USER", user.data);
        });
    }
  },
};
</script>

<style lang="sass">

::-webkit-scrollbar
  width: 6px
  height: 6px

::-webkit-scrollbar-track 
  border-radius: 10px
  background: rgba(0,0,0,0.1)

::-webkit-scrollbar-thumb
  border-radius: 10px
  background: rgba(0,0,0,0.2)

::-webkit-scrollbar-thumb:hover
  background: rgba(0,0,0,0.4)

::-webkit-scrollbar-thumb:active
  background: rgba(0,0,0,.9)

</style>