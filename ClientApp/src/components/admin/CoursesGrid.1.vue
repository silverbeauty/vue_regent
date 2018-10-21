<template>
<div>
    <label>Create a new course:</label>
      <v-text-field
            label="Regular"
            placeholder="Placeholder"
          ></v-text-field>

</div>
 
</template>

<script>
    import Vue from 'vue'
    import axios from 'axios'
     import Vuetify from 'vuetify'

     Vue.use(Vuetify);

    export default {
        methods: {
            createCourse: function(){
                var desc = this.description;
                this.$store.dispatch('createCourse', {description: desc})
            }
            
        },
        data() {
            return {
                description: ''
            }
        },
        computed: {
            // courses: function(){
            //     return this.$store.courseModule.courses;
            // }
        }
    }
</script>

<style>

#inputfield{
    background-color: red;
    width:100%;
}

   
</style>