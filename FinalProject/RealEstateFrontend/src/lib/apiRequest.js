import axios from "axios";

const apiRequest = axios.create({
  baseURL: "http://localhost:5104/api",
  withCredentials: true,
});

apiRequest.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default apiRequest;