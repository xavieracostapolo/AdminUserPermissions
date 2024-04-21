import axios from 'axios'

export const apiClient = axios.create({
    baseURL: 'http://138.197.116.22:8080/api',
    headers: {
        'Content-type': 'application/json',
    },
})
