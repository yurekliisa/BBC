import Home from "../../views/main/pages/Home"
import About from "../../views/main/pages/About"
import RoTDetail from "../../views/main/pages/RoTDetail"
const router = [
    {
        path: "/",
        component: Home,
        name: "Home"
    },
    {
        path: "RoT-Detail/:name-:id",
        component: RoTDetail,
        name:"RoTDetail"
    },
    {
        path: "about",
        component: About,
    }
];

export default {
    router
}