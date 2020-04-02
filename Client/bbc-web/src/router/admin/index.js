
import Dashboard from "../../views/admin/pages/Dashboard.vue";
import Category from "../../views/admin/pages/Category.vue";
import Country from "../../views/admin/pages/Country.vue";
import UserReports from "../../views/admin/pages/UserReports.vue";
const router = [
    {
        path: "/",
        component: Dashboard
    },
    {
        path: "dashboard",
        component: Dashboard
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