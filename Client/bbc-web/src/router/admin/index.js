
import AdminHome from "../../views/admin/pages/AdminHome.vue";
import AdminDashboard from "../../views/admin/pages/AdminDashboard.vue";
import Category from "../../views/admin/pages/Category.vue";
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
];

export default{
    router
}