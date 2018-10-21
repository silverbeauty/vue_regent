import axios from "axios";

// initial state
export const state = {
  courses:[],
  units:[],
  topics: []
}


// actions
export const actions = {

  //Fetches data from endpoint and intiializes the state
  initializeFromServer ({ commit }) {
     axios.get('/Admin/CourseManager')
       .then((response) => {
         var courses = response.data.courses;
         var units = response.data.units;
         var topics = response.data.topics;
         commit('setCourses', { courses: courses })
         commit('setUnits', { units: units })
         commit('setTopics', {topics: topics})
   })
       .catch((response) => {
         console.log(response);

     });
  },
 
  
  coursesFromServer ({ commit }) {
    axios.get('/Admin/Courses')
      .then((response) => {
        var courses = response.data.courses;
        commit('setCourses', { courses: courses })
  })
      .catch((response) => {
        console.log(response);

    });
 },
  createCourse({commit}, c) {
    axios.post('/Admin/CreateCourse', c)
      .then(function (response) {
        console.log(response);
        axios.get('/Admin/Courses')
          .then((response) => {
            var courses = response.data.courses;
            commit('setCourses', { courses: courses })
          })
        .catch((response) => {
        console.log(response);
          })
          .catch((response) => {
            console.log(response);
        });
      });
  },
  editCourse(c) {
    axios.post('/Admin/EditCourse', c)
      .then(function (response) {
        console.log(response);
      });
  },
  deleteCourse(c) {
    axios.post('/Admin/DeleteCourse', c)
      .then(function (response) {
        console.log(response);
      });
  }
}

// mutations
export const mutations = {

  setCourses: function(state, payload){
    state.courses = payload.courses;
  },

  setUnits: function(state, payload){
    state.units = payload.units;
  },

  setTopics: function(state, payload){
    state.topics = payload.topics;
  }
}

export const getters = {
  getUnitsForCourse(state, payload) {
    return state.units.find(unit => unit.courseId === payload.id)
  },

  getTopicsForUnit(state, payload) {
    return state.topics.find(topic => topic.unitId === payload.id)
  }
}

export default {
  namespaced: true,
  state,
  actions,
  mutations
}