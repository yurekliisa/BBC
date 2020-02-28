const state = {
  clipped: false,
  drawer: false,
  fixed: true,
  miniVariant: false,
  rightDrawer: false,
  panelText: 'Vuex',
  footer: "This is an footer"
};
const mutations = {
  SET_clipped(state, payload) {
    state.clipped = payload;
  },
  SET_drawer(state, payload) {
    state.drawer = payload;
  },
  SET_fixed(state, payload) {
    state.fixed = payload;
  },
  SET_miniVariant(state, payload) {
    state.miniVariant = payload;
  },
  SET_rightDrawer(state, payload) {
    state.rightDrawer = payload;
  },
  SET_panelText(state, payload) {
    state.panelText = payload;
  },
  SET_footer(state, payload) {
    state.footer = payload;
  }
};
const getters = {
  footer(state) {
    return state.footer;
  },
  clipped(state) {
    return state.clipped;
  },
  drawer(state) {
    return state.drawer;
  },
  fixed(state) {
    return state.fixed;
  },
  miniVariant(state) {
    return state.miniVariant;
  },
  rightDrawer(state) {
    return state.rightDrawer;
  },
  panelText(state) {
    return state.panelText;
  }
};

export default {
  state,
  getters,
  mutations
}
