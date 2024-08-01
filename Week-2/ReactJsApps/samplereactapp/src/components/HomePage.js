import React from 'react';
import { useNavigate } from 'react-router-dom';
import '../HomePage.css';

function HomePage() {
    const navigate = useNavigate();

    const handleButtonClick = (action) => {
        if (action === 'GET') {
            navigate('/get');
        } else if (action === 'POST') {
            navigate('/post');
        } else if (action === 'PUT') {
            navigate('/put');
        } else if (action === 'DELETE') {
            navigate('/delete');
        }
    }

    return (
        <div className="homepage-container">
            <h3>Welcome to Library</h3>
            <button onClick={() => handleButtonClick('GET')}>Get Book List</button>
            <button onClick={() => handleButtonClick('POST')}>Add Book</button>
            <button onClick={() => handleButtonClick('PUT')}>Update Book</button>
            <button onClick={() => handleButtonClick('DELETE')}>Delete Book</button>
        </div>
    );
}

export default HomePage;
