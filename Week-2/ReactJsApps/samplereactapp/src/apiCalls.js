// apiCalls.js
const axiosInstance = require('./components/ApiClient');

const fetchData = async () => {
    try {
        const response = await axiosInstance.get('/posts/1');
        console.log('Data:', response.data);
    } catch (error) {
        console.error('Error fetching data:', error);
    }
};

module.exports = { fetchData };
