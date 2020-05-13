<template>
  <div v-if="isActive">
     <v-row class="mx-auto">
      <v-col cols="4">
        <v-card class="mx-auto" color="#ff6b6b" dark max-width="400">
          <v-card-title>
            <span class="title font-weight-light">{{userReport.headerWidget.totalRecivedComment}}</span>
          </v-card-title>
          <v-card-text class="headline font-weight-bold">
            Toplam alınan yorum sayısı
          </v-card-text>
        </v-card>
      </v-col>
      <v-col cols="4">
        <v-card class="mx-auto" color="#1a535c" dark max-width="400">
          <v-card-title>
            <span class="title font-weight-light">{{userReport.headerWidget.totalComment}}</span>
          </v-card-title>
          <v-card-text class="headline font-weight-bold">
            Toplam yapılan yorum sayısı
          </v-card-text>
        </v-card>
      </v-col>
      <v-col cols="4">
        <v-card class="mx-auto" color="#4ecdc4" dark max-width="400">
          <v-card-title>
            <span class="title font-weight-light">{{userReport.headerWidget.totalTaR}}</span>
          </v-card-title>
          <v-card-text class="headline font-weight-bold">
            Toplam alınan yorum sayısı
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
   
    <v-row class="mx-auto">
      <v-col cols="12">
        <apexcharts
          width="100%"
          height="350"
          type="bar"
          :options="chartOptions"
          :series="series"
        ></apexcharts>
      </v-col>
    </v-row>
  </div>
</template>

<script>
// --https://apexcharts.com/docs/vue-charts
import VueApexCharts from "vue-apexcharts";
import axios from"axios";
export default {
  name: "ProfileDashboard",
  components: {
    apexcharts: VueApexCharts,
  },
  props: ["id"],
  data() {
    return {
      userReport: {},
      isActive:false,
      chartOptions: {
        chart: {
          id: "basic-bar",
          animations: {
            speed: 200,
          },
        },
        dataLabels: {
          enabled: false,
        },
        plotOptions: {
          bar: {
            distributed: true,
          },
        },
        xaxis: {
          categories: ['Ocak','Şubat','Mart','Nisan','Mayıs','Haziran','Temmuz','Agustos','Eylül','Ekim','Kasım','Aralık'],
        },
      },
      series: [
        {
          name: "series-1",
          data: [0,0,0,0,0,0,0,0,0,0,0,0],
        },
      ],
      dashboard: {},
    };
  },
  created(){
    console.log(this.id);
    axios 
      .get ("/User/UserReport?userId="+this.id,{
        headers:{
          "Content-type": "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
      .then((result)=>{
        console.log(result);
        if(result.status===200){
          for (let i = 0; i < result.data.monthlyTAR.length; i++) {
            const element = result.data.monthlyTAR[i];
            this.series[0].data[element.month-1]=element.totalTaR;
          }
          this.userReport = result.data;
          this.isActive=true;
        }
      })
      .catch((err)=>{
        console.log (err);
    });
  },
  methods: {
    updateTheme(e) {
      this.chartOptions = {
        theme: {
          palette: e.target.value,
        },
      };
    },
  },
};
</script>

<style></style>>
