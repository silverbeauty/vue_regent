import Vue from "vue";
import Router from "vue-router";
import CreateCourse from "./components/admin/createCourse.vue"

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/CreateCourse",
      name: "CreateCourse",
      component: CreateCourse
    }
  ]
});
