import React, { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { FiArrowLeft } from "react-icons/fi";
import api from '../../services/api';
import { formatDate, authorization } from '../../utils/dateUtils';
import logoImage from '../../assets/logo.svg';
import './styles.css';

export default function NewBook() {

    const userName = localStorage.getItem('userName');
    const [id, setId] = useState(null);
    const [author, setAuthor] = useState('');
    const [title, setTitle] = useState('');
    const [launchDate, setLaunchDate] = useState('');
    const [price, setPrice] = useState('');
    const { bookId } = useParams();
    const navigate = useNavigate(); // Redirect user to another page
    const accessToken = localStorage.getItem('accessToken');


    useEffect(() => {
        if (bookId === '0') return;
        else loadBook();
    }, [bookId]);

    async function loadBook() {
        try {
            const response = await api.get(`api/book/v1/${bookId}`, authorization);
            const book = response.data;
            let ajustedDate = book.launchDate.split("T", 10)[0];
            setId(book.id);
            setAuthor(book.author);
            setTitle(book.title);
            setLaunchDate(ajustedDate);
            setPrice(book.price);
        } catch (err) {
            alert('Error recovering book. Try again.');
            navigate('/books');
        }
    }

    async function saveOrUpdate(e) {
        e.preventDefault();
        const formattedDate = formatDate(launchDate);
        const data = {
            author,
            title,
            launchDate: formattedDate,
            price
        };
        console.log(accessToken);
        try {
            if (bookId === '0') {
                await api.post('/api/book/v1', data, authorization);
            } else {
                data.id = id;
                await api.put(`/api/book/v1`, data, authorization);
            }
        } catch (err) {
            alert('Error creating new book. Try again.');
        }
        navigate('/books');
    }

    return (
        <div className="new-book-container">
            <div className="content">
                <section className="form">
                    <span>Hello, <strong>{userName}</strong></span>
                    <img src={logoImage} alt="Be The Hero" />
                    <h1>{bookId === '0' ? 'Add New' : 'Update'} Book</h1>
                    <p>Enter the information and click on <strong>{bookId === '0' ? `'Add'` : `'Update'`}</strong>.</p>

                    <p><Link className="back-link" to="/books">
                        <FiArrowLeft size={16} color="#251fc5" />
                        Back to Books
                    </Link></p>
                </section>
                <form onSubmit={saveOrUpdate} >
                    <input
                        placeholder="Title"
                        value={title}
                        onChange={e => setTitle(e.target.value)}
                    />
                    <input
                        placeholder="Author"
                        value={author}
                        onChange={e => setAuthor(e.target.value)}
                    />
                    <input
                        placeholder="Date"
                        type="date"
                        value={launchDate}
                        onChange={e => setLaunchDate(e.target.value)}
                    />
                    <input
                        placeholder="Price"
                        value={price}
                        onChange={e => setPrice(e.target.value)}
                    />
                    <button className="button" type="submit">{bookId === '0' ? 'Add' : 'Update'}</button>
                </form>
            </div>
        </div>
    );
}