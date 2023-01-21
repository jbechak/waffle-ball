<template>
    <div>

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
import TeamService from '@/services/TeamService';
import TeamStanding from './TeamStanding.vue';
export default {
    components: { TeamStanding },
    data() {
    return {
      teams: [],
      isLoading: true
    };
    
  },
    created() {
        TeamService.getAllTeams()
            .then((response) => {
                this.teams = response.data;
                this.isLoading = false;
            });
    }
}
</script>

<style>
#table-head {
    display: flex;
    justify-content: stretch;
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