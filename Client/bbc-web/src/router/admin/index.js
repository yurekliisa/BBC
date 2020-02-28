
import AdminHome from "../../views/admin/pages/AdminHome.vue";
import AdminDashboard from "../../views/admin/pages/AdminDashboard.vue";

const router = [
    {
        path: "/",
        component: AdminHome
    },
    {
        path: "dashboard",
        component: AdminDashboard
    }
];

export default{
    router
}