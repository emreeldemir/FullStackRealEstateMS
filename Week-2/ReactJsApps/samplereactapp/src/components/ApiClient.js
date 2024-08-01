import axios from "axios";


const apiClient = axios.create({
    baseURL: "http://localhost:5227/api",
    timeout: 10000,
});


apiClient.interceptors.request.use(
    function (config) {
        config.headers.Authorization = "Bearer sample_token";
        console.log("Request: ", config);
        return config;
    },
    function (error) {
        return Promise.reject(error);
    }
);

apiClient.interceptors.response.use(
    function (response) {
        console.log("Response: ", response);
        return response.data;
    },
    function (error) {
        if (error.response && error.response.status === 401) {
            console.error("Yetkisiz Erisim!");
        } else {
            console.error("Beklenmeyen bir hata olustu!", error.message);
        }
    }
);

export default apiClient;