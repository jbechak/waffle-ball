<template>
  <div>
    <div id="conference-name">
       {{ conference }} 
    </div>
    
    <div v-for="division in $store.state.divisions" :key="division">
        <Division-standings :division="division" :conference="conference" />
    </div>
  </div>

</template>

<script>
import DivisionStandings from './DivisionStandings.vue';
export default {
    components: { DivisionStandings },
    props: [ "conference" ]

}
</script>

<style>

#conference-name {
    margin-top: 30px;
    padding-top: 10px;
    padding-bottom: 10px;
    text-align: center;
    font-weight: 600;
    border-top: solid rgb(54, 54, 54) 2px;
    border-bottom: solid rgb(54, 54, 54) 1px;
}
</style>