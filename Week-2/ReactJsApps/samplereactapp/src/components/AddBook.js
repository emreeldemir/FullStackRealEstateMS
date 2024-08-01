import React, { useState } from 'react';
import apiClient from './ApiClient';
import '../Addbook.css';

function AddBook() {
    const [name, setName] = useState('');
    const [customerId, setCustomerId] = useState('');
    const [authorId, setAuthorId] = useState('');
    const [genreId, setGenreId] = useState('');
    const [publisherId, setPublisherId] = useState('');
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(null);

    const handleSubmit = (e) => {
        e.preventDefault();

        const bookData = {
            name,
            customerId: parseInt(customerId),
            authorId: parseInt(authorId),
            genreId: parseInt(genreId),
            publisherId: parseInt(publisherId)
        };

        apiClient.post('/Book', bookData)
            .then(response => {
                setSuccess('Book added successfully!');
                setError(null);
            })
            .catch(error => {
                setError('Failed to add book: ' + error.message);
                setSuccess(null);
            });
    };

    return (
        <div className="post-book-container">
            <h1>Add New Book</h1>
            <form className="post-book-form" onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Book Name:</label>
                    <input
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Author ID:</label>
                    <input
                        type="number"
                        value={authorId}
                        onChange={(e) => setAuthorId(e.target.value)}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Genre ID:</label>
                    <input
                        type="number"
                        value={genreId}
                        onChange={(e) => setGenreId(e.target.value)}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Publisher ID:</label>
                    <input
                        type="number"
                        value={publisherId}
                        onChange={(e) => setPublisherId(e.target.value)}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Customer ID:</label>
                    <input
                        type="number"
                        value={customerId}
                        onChange={(e) => setCustomerId(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" style={{ backgroundColor: "#4CAF50" }} className="submit-button">Add Book</button>
            </form>
            {error && <div className="error-message">{error}</div>}
            {success && <div className="success-message">{success}</div>}
        </div>
    );
}

export default AddBook;
