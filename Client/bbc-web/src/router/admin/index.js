
import AdminHome from "../../views/admin/pages/AdminHome.vue";
import AdminDashboard from "../../views/admin/pages/AdminDashboard.vue";
import Category from "../../views/admin/pages/Category.vue";
import Country from "../../views/admin/pages/Country.vue";
import UserReports from "../../views/admin/pages/UserReports.vue";
const router = [
    {
        path: "/",
        component: AdminHome
    },
    {
        path: "dashboard",
        component: AdminDashboard
    },
    {
        path: "category",
        component: Category
    },
    {
        path:  "country",
        component: Country
    },
    {
        path:  "userreports",
        component: UserReports
    },
];

export default{
    router
}