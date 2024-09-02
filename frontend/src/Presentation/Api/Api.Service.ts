import axios, { AxiosInstance } from 'axios';

const api: AxiosInstance = axios.create({
    baseURL: 'http://localhost:5035/api/v1/',
    timeout: 10000,
    headers: {
        'Content-Type': 'application/json',
    },
});

const token = localStorage.getItem('authToken');
if (token) {
    api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
}

api.interceptors.response.use(
    response => response,
    error => {
        if (error.response && error.response.status === 401) {
            window.location.href = '/';
        }
        return Promise.reject(error);
    }
);

export default api;
