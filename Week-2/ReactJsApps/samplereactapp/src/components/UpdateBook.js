import React, { useState } from 'react';
import apiClient from './ApiClient';
import '../UpdateBook.css';

function UpdateBook() {
    const [id, setId] = useState('');
    const [name, setName] = useState('');
    const [customerId, setCustomerId] = useState('');
    const [authorId, setAuthorId] = useState('');
    const [genreId, setGenreId] = useState('');
    const [publisherId, setPublisherId] = useState('');
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(null);

    const handleSubmit = (e) => {
        e.preventDefault();

        const updatedBook = {
            name,
            authorId: parseInt(authorId),
            genreId: parseInt(genreId),
            publisherId: parseInt(publisherId),
            customerId: parseInt(customerId),
            id: parseInt(id)
        };

        apiClient.put('/Book', updatedBook) // Endpoint güncellenmiş.
            .then(response => {
                setSuccess('Book updated successfully!');
                setError(null);
            })
            .catch(error => {
                setError('Failed to update book: ' + error.message);
                setSuccess(null);
            });
    };

    return (
        <div className="update-book-container">
            <h1>Update Book</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Book ID:</label>
                    <input
                        type="number"
                        value={id}
                        onChange={(e) => setId(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Name:</label>
                    <input
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                </div>
                <div>
                    <label>Customer ID:</label>
                    <input
                        type="number"
                        value={customerId}
                        onChange={(e) => setCustomerId(e.target.value)}
                    />
                </div>
                <div>
                    <label>Author ID:</label>
                    <input
                        type="number"
                        value={authorId}
                        onChange={(e) => setAuthorId(e.target.value)}
                    />
                </div>
                <div>
                    <label>Genre ID:</label>
                    <input
                        type="number"
                        value={genreId}
                        onChange={(e) => setGenreId(e.target.value)}
                    />
                </div>
                <div>
                    <label>Publisher ID:</label>
                    <input
                        type="number"
                        value={publisherId}
                        onChange={(e) => setPublisherId(e.target.value)}
                    />
                </div>
                <button type="submit">Update Book</button>
            </form>
            {error && <div className="error">{error}</div>}
            {success && <div className="success">{success}</div>}
        </div>
    );
}

export default UpdateBook;
