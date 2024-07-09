import axios from "axios";

const api = axios.create({
    baseURL: "http://localhost:44300",
});

export default api;