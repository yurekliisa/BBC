const state = {
    userInfo: {},
    /* 
        id,
        userName,
        fullName,
        email
    */
};
const mutations = {
    SET_USER(state, user) {
        console.log("set")
        console.log(user)
        state.userInfo = user;
    },
};
const getters = {
    userInfo(state) {
        return state.userInfo;
    },
};
export default {
    state,
    getters,
    mutations
}
