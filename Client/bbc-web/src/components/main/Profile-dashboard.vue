<template>
  <div>
    <!--https://apexcharts.com/docs/vue-charts/-->
    <h5>Profile Dashboard</h5>
    <apexcharts
      width="100%"
      height="350"
      type="bar"
      :options="chartOptions"
      :series="series"
    ></apexcharts>
  </div>
</template>

<script>
import VueApexCharts from "vue-apexcharts";

export default {
  name: "ProfileDashboard",
  components: {
    apexcharts: VueApexCharts,
  },
  props: ["id"],
  data() {
    return {
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
          categories: [1991, 1992, 1993, 1994, 1995],
        },
      },
      series: [
        {
          name: "series-1",
          data: [30, 40, 45, 30, 49],
        },
      ],
      dashboard: {},
    };
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
