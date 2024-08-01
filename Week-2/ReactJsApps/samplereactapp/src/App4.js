// src/components/App.js
import React, { useEffect } from 'react';
import axiosInstance from './components/ApiClient';

const App = () => {
    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axiosInstance.get('/data');
                console.log('Data:', response.data);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        const postData = async (data) => {
            try {
                const response = await axiosInstance.post('/data', data);
                console.log('Data posted:', response.data);
            } catch (error) {
                console.error('Error posting data:', error);
            }
        };

        fetchData();
        postData({ name: 'Test Data' });
    }, []);

    return <div>App Component</div>;
};

export default App;
