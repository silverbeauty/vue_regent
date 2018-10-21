<template>
<div>
    <label>Create a new course:</label>
    <v-text-field v-model="description"
            label="Regular"
            placeholder="Placeholder"
            v-on:blur="createCourse"
          ></v-text-field>
</div>
 
</template>

<script>
    import Vue from 'vue'
    import axios from 'axios'
    import Vuetify from 'vuetify'
    Vue.use(Vuetify);
   // import Vuex from "vuex";

    Vue.use(Vuetify);

    /*const store = new Vuex.Store({
        state: {
            count: 0
        },
        mutations: {
            increment (state) {
                state.count++
            }
        },
        actions: {
            increment (context) {
                context.commit('increment')
            }
        }
    })*/
    export default {
        methods: {
            createCourse: function(){
                var desc = this.description;
                console.log(desc);
                this.$store.dispatch('courseModule/createCourse', {Description:  desc})
            }
            
        },
        data() {
            return {
                description: ''
            }
        },
        computed: {
             courses: function(){
                 return this.$store.courseModule.courses;
             }
        }
    }
</script>

<style>

#inputfield{
    background-color: red;
    width:100%;
}

   
</style>