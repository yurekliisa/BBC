<template>
  <div class="about" v-if="report">
    <h1>Dashboard</h1>
    <v-row>
      <v-col cols="12" md="4">
        <v-card class="mx-auto" color="#26c6da" dark>
          <v-card-title>
            <span class="title font-weight-light">{{report.totalTar}}</span>
          </v-card-title>
          <v-card-text class="headline font-weight-bold">Toplam Tarif ve Reçete</v-card-text>
        </v-card>
      </v-col>
      <v-col cols="12" md="4">
        <v-card class="mx-auto" color="#26c6da" dark>
          <v-card-title>
            <span class="title font-weight-light">{{report.totalUser}}</span>
          </v-card-title>
          <v-card-text class="headline font-weight-bold">Toplam Kullanıcı Sayısı</v-card-text>
        </v-card>
      </v-col>
      <v-col cols="12" md="4">
        <v-card class="mx-auto" color="#26c6da" dark>
          <v-card-title>
            <span class="title font-weight-light">{{report.totalComment}}</span>
          </v-card-title>
          <v-card-text class="headline font-weight-bold">>Toplam Yorum Sayısı</v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" md="6">
        <v-card class="mx-auto" color="#26c6da" dark>
          <v-card-title>
            <span class="title font-weight-light">{{report.totalLobby}}</span>
          </v-card-title>
          <v-card-text class="headline font-weight-bold">Toplam Lobi Sayısı</v-card-text>
        </v-card>
      </v-col>
      <v-col cols="12" md="6">
        <v-card class="mx-auto" color="#26c6da" dark>
          <v-card-title>
            <span class="title font-weight-light">{{report.totalCategory}}</span>
          </v-card-title>
          <v-card-text class="headline font-weight-bold">Toplam Kategori Sayısı</v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script>
export default {
  name: "Dashboard",
  data() {
    return {
      report
    };
  },
  created() {
    this.$store.commit("SET_panelText", "Dashboard");
  },
  methods: {
    fetchData() {
      axios
        .get("/AdminReport/AdminReport", {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*"
          }
        })
        .then(result => {
          console.log(result);
          if (result.status === 200) {
            this.recentChefs = result.data;
          }
        })
        .catch(err => {
          console.log(err);
        });
    }
  }
};
</script>
