import Vue from "vue";
import Vuex from "vuex";
import courseModule from './modules/courseModule'
Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  modules: { 
    courseModule: courseModule
  }
});
