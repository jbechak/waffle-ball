<template>
    <div>
        <div id="listing">
            <div id="team">{{ team.city + " " + team.name }}</div>
            <div id="wins">{{ team.wins }}</div>
            <div id="losses">{{ team.losses }}</div>
            <div id="points">{{ team.points }}</div>

        </div>


        <!-- {{ team.city }} -->
    </div>
</template>

<script>
export default {
    props: ["team"]

}
</script>

<style>
#listing {
    display: flex;
}
</style>