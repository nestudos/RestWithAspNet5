import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:44350',
});

export default api;