import Home from "../../views/main/pages/Home"
import LogIn from "../../views/main/pages/LogIn"
import RoTDetail from "../../views/main/pages/RoTDetail"
import Register from "../../views/main/pages/Register"
import Profile from '../../views/main/pages/Profile'

const GuardToken = (to, from, next) => {
    let go = localStorage.getItem('user');
    if (go==null) {
      next({
        path: '/',
      })
    }
    next();
  }

  
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
        component: Register
    },
    {
        path:"profile/:id",
        component:Profile,
        name:"Profile"

    }
];

export default {
    router
}