import Home from "../../views/main/pages/Home"
import LogIn from "../../views/main/pages/LogIn"
import RoTDetail from "../../views/main/pages/RoTDetail"
import Register from "../../views/main/pages/Register"
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
        path: "login",
        component: LogIn,
    },
    {
        path: "register",
        componnet: Register
    }
];

export default {
    router
}