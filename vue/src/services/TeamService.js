import axios from 'axios';

export default {

    getAllTeams() {
        return axios.get('/Team');
    }

}