import Home from "../../views/main/pages/Home"
import LogIn from "../../views/main/pages/LogIn"
import TARDetail from "../../views/main/pages/tar/TARDetail"
import Register from "../../views/main/pages/Register"
import Profile from '../../views/main/pages/Profile'
import ProfileEdit from '../../views/main/pages/Profile-Edit'
import CreateTAR from "../../views/main/pages/tar/CreateTAR"
import EditTAR from "../../views/main/pages/tar/EditTAR"
import TAR from "../../views/main/pages/tar/TAR"
import Chat from "../../views/main/pages/Chat"

const GuardToken = (to, from, next) => {
    let go = localStorage.getItem('user');
    if (go == null) {
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
        path: "TAR-Detail/:name-:id",
        component: TARDetail,
        name: "TARDetail"
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
        path: "profile/:id",
        component: Profile,
        name: "Profile"
    },
    {
        path: "profile/edit/:id",
        component: ProfileEdit,
        name: "ProfileEdit"
    },
    {
        path: "tar/create",
        component: CreateTAR,
        name: "CreateTAR"
    },
    {
        path: "tar/edit/:id",
        component: EditTAR,
        name: "EditTAR"
    },
    {
        path: "tar",
        component: TAR,
        name: "TAR"
    },
    {
        path:"chat",
        component:Chat,
        name:"chat",
        beforeEnter: GuardToken
    }
];

export default {
    router
}