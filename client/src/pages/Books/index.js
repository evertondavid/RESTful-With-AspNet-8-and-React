import React, { useState, useEffect } from "react"; // Import React and useState
import { Link, useNavigate } from "react-router-dom"; // Import Link from react-router-dom
import { FiPower, FiEdit, FiTrash2 } from "react-icons/fi"; // Feather Icons
import api from '../../services/api';
import { formatDate, authorization } from '../../utils/dateUtils';
import logoImage from '../../assets/logo.svg';
import './styles.css';

export default function Books() {
    const [books, setBooks] = useState([]);
    const [page, setPage] = useState(0);
    const userName = localStorage.getItem('userName');
    const accessToken = localStorage.getItem('accessToken');
    const navigate = useNavigate(); // Redirect user to another page

    useEffect(() => {
        api.get(`api/book/v1/asc/4/${page}`, authorization).then(response => {
            setBooks(response.data.list)
        })
    }, [accessToken]);
    //debugger;

    async function fetchMoreBooks() {
        const response = await api.get(`api/book/v1/asc/4/${page}`, authorization);
        setBooks([...books, ...response.data.list])
        setPage(page + 1)
    }

    async function editBook(id) {
        try {
            navigate(`/book/new/${id}`);
        } catch (err) {
            alert('Error editing book. Try again.');
        }
    }

    async function deleteBook(id) {
        try {
            await api.delete(`api/book/v1/${id}`, authorization);

            setBooks(books.filter(book => book.id !== id));
        } catch (err) {
            alert('Error deleting book. Try again.');
        }
    }

    async function logout() {
        try {
            await api.get('api/auth/v1/revoke', authorization);
            localStorage.clear();
            navigate('/');
        } catch (err) {
            alert('Error logging out. Try again.');
        }
    }

    return (
        <div className="book-container">
            <header>
                <img src={logoImage} alt="Be The Hero" />
                <span>Welcome, <strong>{userName}</strong></span>
                <Link className="button" to="../book/new/0">Add New Book</Link>
                <button onClick={logout} type="button">
                    <FiPower size={18} color="#251fc5" />
                </button>
            </header>
            <h1>Registred Books</h1>

            <ul>
                {books.map(book => (
                    <li key={book.id}>
                        <strong>Book:</strong>
                        <p>{book.title}</p>

                        <strong>Author:</strong>
                        <p>{book.author}</p>

                        <strong>Price:</strong>
                        <p>{Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(book.price)}</p>

                        <strong>Release Date:</strong>
                        <p>{formatDate(book.launchDate)}</p>
                        {/* <p>{Intl.DateTimeFormat('en-US').format(new Date(book.launchDate))}</p> */}

                        <button onClick={() => editBook(book.id)} type="button">
                            <FiEdit size={20} color="#251fc5" />
                        </button>
                        <button onClick={() => deleteBook(book.id)} type="button">
                            <FiTrash2 size={20} color="#251fc5" />
                        </button>
                    </li>
                ))}
            </ul>
            <button className="button" onClick={fetchMoreBooks} type="button" >Load More</button>
        </div>
    );
}