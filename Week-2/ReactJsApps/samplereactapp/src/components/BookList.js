import React, { useState, useEffect } from 'react';
import apiClient from './ApiClient';
import '../BookList.css';

function BookList() {
    const [books, setBooks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        apiClient.get('/Book/list')
            .then(response => {
                setBooks(response);
                console.log(books)
                setLoading(false);
            })

            .catch(error => {
                setError(error);
                setLoading(false);
            });
    }, []);

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error.message}</div>;

    return (
        <div className="book-list-container">
            <h1>Books List</h1>
            <ul className="book-list">
                {books.length > 0 ? books.map(book => (
                    <li key={book.id} className="book-item">
                        <div className="book-name">{book.name}</div>
                        <div className="book-details">
                            <span className="book-author">Author: {book.authorFirstName} {book.authorLastName}</span>
                            <span className="book-genre">Genre: {book.genreName}</span>
                            <span className="book-publisher">Publisher: {book.publisherName}</span>
                            <span className="book-publisher">Customer: {book.customerFirstName} {book.customerLastName}</span>
                        </div>
                    </li>
                )) : <li className="no-books">No books found</li>}
            </ul>
        </div>
    );

}

export default BookList;

