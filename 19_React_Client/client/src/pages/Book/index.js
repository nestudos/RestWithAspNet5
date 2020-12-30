import React, { useState, useEffect } from 'react';
import { Link, useHistory } from 'react-router-dom';
import api from '../../services/api';

import './styles.css';
import logo from '../../assets/logo.svg';
import { FiPower, FiEdit, FiTrash2 } from 'react-icons/fi'

export default function Book() {

    const [books, setBooks] = useState([]);
    const [page, setPage] = useState(1);

    const userName = localStorage.getItem('userName');
    const accessToken = localStorage.getItem('accessToken');
    const authorization = { headers: { Authorization: `Bearer ${accessToken}` } };
    const history = useHistory();

    useEffect(() => {

        api.get(`api/book/v1/desc/4/${page}`, authorization).then(response => {

            setBooks(response.data.list);

        });

    }, [accessToken]);

    async function fetchMoreBooks() {

        const response = await api.get(`api/book/v1/desc/4/${page}`, authorization);

        setBooks([...books, ...response.data.list]);
        setPage(page + 1);

    }

    async function logout() {

        try {

            await api.get(`api/auth/v1/revoke`, authorization);

            localStorage.clear();
            history.push('/');

        } catch (error) {


            alert('Delete failed! Try again!')
        }
    }


    async function deleteBook(id) {

        try {

            await api.delete(`api/book/v1/${id}`, authorization);

            setBooks(books.filter(b => b.id !== id));

        } catch (error) {


            alert('Delete failed! Try again!')
        }
    }

    async function editBook(id) {

        try {

            history.push(`book/new/${id} `);

        } catch (error) {
            alert('Edit book failed');
        }
    }


    return (
        <div className="book-container">
            <header>
                <img src={logo} alt="Logo" />
                <span>Welcome <strong>{userName.toUpperCase()}</strong>!</span>
                <Link className="button" to="book/new/0">Add New Booak</Link>
                <button>
                    <FiPower onClick={logout} size={18} color="#251fc5" />
                </button>
            </header>

            <h1>Registered Books</h1>
            <ul>

                {books.map((book) => {

                    const price = Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(book.price);
                    const launchDate = Intl.DateTimeFormat('pt-BR').format(new Date(book.launchDate));
                    const { id, title, author } = book;

                    return (
                        <li key={id}>
                            <strong>Title:</strong>
                            <p>{title}</p>
                            <strong>Author:</strong>
                            <p>{author}</p>
                            <strong>Price:</strong>
                            <p>{price} </p>
                            <strong>Release Date:</strong>
                            <p>{launchDate}</p>
                            <button onClick={() => editBook(id)}>
                                <FiEdit size={20} color="#251fc5" />
                            </button>
                            <button onClick={() => deleteBook(id)}   >
                                <FiTrash2 size={20} color="#251fc5" />
                            </button>
                        </li>
                    )
                }
                )}




            </ul>
            <button onClick={fetchMoreBooks} className="button" >Load More</button>
        </div>
    );
}