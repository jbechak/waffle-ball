<template>
  <div>
    <div id="division-name">
        {{ division }}
    </div>
    

    <div id="table-head">
            <div id="team">Team</div>
            <div id="wins">W</div>
            <div id="losses">L</div>
            <div id="points">Pts</div>
        </div>
        <div v-for="team in teams" :key="team.id">
            <team-standing :team="team" />
        </div>
  </div>
</template>

<script>
import TeamStanding from './TeamStanding.vue';
export default {
    components: { TeamStanding },
    props: [ "division", "conference" ],
    data() {
        return {
            teams: []
        }
    },
    created() {
        this.teams = this.$store.state.teams.filter((team) => team.division == this.division && team.conference == this.conference);
    }
    
}
</script>

<style>

#division-name {
    margin-top: 20px;
    margin-bottom: 5px;
    text-align: left;
    font-weight: 600;
    font-size: 20px;
}
#table-head {
    display: flex;
    justify-content: stretch;
    font-weight: 600;
}

#team {
    /* flex: 5, 1, 0; */
    width: 250px;
    text-align: left; 
}

#wins {
    /* flex: 1, 1, 0; */
    width: 50px;
    text-align: left;
}

#losses {
    /* flex: 1, 1, 0; */
    width: 50px;
    text-align: left;
}

#points {
    /* flex: 1, 1, 0; */
    width: 50px;
    text-align: left;
}
</style>