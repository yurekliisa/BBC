
import Dashboard from "../../views/admin/pages/Dashboard.vue";
import Category from "../../views/admin/pages/Category.vue";
import Lobby from "../../views/admin/pages/Lobby.vue";
import Settings from "../../views/admin/pages/Settings.vue";
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
        path:  "lobby",
        component: Lobby
    },
    {
        path: "settings",
        component: Settings
    },
];

export default{
    router
}