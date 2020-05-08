<template>
  <router-view />
</template>

<script>
import axios from "axios";
export default {
  name: "App",
  created() {
    if (localStorage.getItem("user")) {
      console.log("2");
      const userData = JSON.parse(localStorage.getItem("user"));
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
