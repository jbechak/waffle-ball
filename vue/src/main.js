import Vue from 'vue'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import store from './store'

Vue.config.productionTip = false

axios.defaults.baseURL = 'https://localhost:7217';

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
