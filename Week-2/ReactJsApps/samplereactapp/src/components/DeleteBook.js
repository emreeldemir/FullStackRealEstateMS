import React, { useState, useEffect } from 'react';
import apiClient from './ApiClient';
import '../DeleteBook.css';

function DeleteBook() {
    const [books, setBooks] = useState([]);
    const [selectedBookId, setSelectedBookId] = useState('');
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(null);

    useEffect(() => {

        apiClient.get('/Book/list')
            .then(response => {
                setBooks(response);
            })
            .catch(error => {
                setError('Failed to fetch books: ' + error.message);
            });
    }, []);

    const handleDelete = (e) => {
        e.preventDefault();

        if (!selectedBookId) {
            setError('Please select a book to delete.');
            return;
        }

        // DELETE isteği gönder
        apiClient.delete(`/Book?id=${selectedBookId}`, {
            data: { isDeleted: 1 } // Kitabı silinmiş olarak işaretle
        })
            .then(response => {
                setSuccess('Book deleted successfully!');
                setError(null);
            })
            .catch(error => {
                setError('Failed to delete book: ' + error.message);
                setSuccess(null);
            });
    };

    return (
        <div className="delete-book-container">
            <h1>Delete Book</h1>
            <form className="delete-book-form" onSubmit={handleDelete}>
                <div className="form-group">
                    <label>Select Book:</label>
                    <select
                        className="book-select"
                        value={selectedBookId}
                        onChange={(e) => setSelectedBookId(e.target.value)}
                        required
                    >
                        <option value="">Select a book</option>
                        {books.map(book => (
                            <option key={book.id} value={book.id}>
                                {book.name}
                            </option>
                        ))}
                    </select>
                </div>
                <button type="submit" className="delete-button">Delete</button>
            </form>
            {error && <div className="error-message">{error}</div>}
            {success && <div className="success-message">{success}</div>}
        </div>
    );
}

export default DeleteBook;