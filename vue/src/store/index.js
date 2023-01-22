import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    teams: [],
    conferences: [ 'AWC', 'NWC' ],
    divisions: [ 'East', 'Central', 'West' ]
  },
  getters: {
  },
  mutations: {
    SET_TEAMS(state, teams) {
      this.state.teams = teams;
    }
  },
  actions: {
  },
  modules: {
  }
})
