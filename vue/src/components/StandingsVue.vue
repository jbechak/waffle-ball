<template>
    <div>
        <div v-if="isLoading == true">
            Loading
        </div>
        <div v-else>
            <div id="standings-title">
                National Waffleball League Standings
            </div>
            <div v-for="conference in $store.state.conferences" :key="conference">
                <conference-standings :conference="conference" />
            </div>

        </div>


    </div>
</template>

<script>
import TeamService from '@/services/TeamService';
import ConferenceStandings from './ConferenceStandings.vue';

export default {
    components: { ConferenceStandings },
    data() {
        return {
            teams: [],
            isLoading: true
        };

    },
    created() {
        TeamService.getAllTeams()
            .then((response) => {
                this.$store.commit("SET_TEAMS", response.data);
                this.isLoading = false;
            });
    }
}
</script>

<style>
#standings-title {
    font-weight: 600;
    font-size: 25px;
}
</style>