import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5082', // Correct port from launchSettings
    headers: {
        'Content-Type': 'application/json'
    }
});

api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default api;
