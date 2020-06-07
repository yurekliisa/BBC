import * as signalR from '@aspnet/signalr';


const state = {
    userInfo: {},
    connection:{},
    /* 
        id,
        userName,
        fullName,
        email
    */
};
const actions = {

}

const mutations = {
    SET_USER(state, user) {
        state.userInfo = user;
        state.connection = new signalR.HubConnectionBuilder()
        .withUrl('https://bbc-api.azurewebsites.net/signalr')
        .build();
        console.log(state.connection);
    },
};
const getters = {
    userInfo(state) {
        return state.userInfo;
    },
    connection(state) {
        return state.connection;
    },
};
export default {
    state,
    getters,
    mutations
}
