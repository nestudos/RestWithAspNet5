import React, { useEffect, useState } from 'react';
import { Link, useHistory, useParams } from 'react-router-dom';
import './styles.css';
import logo from '../../assets/logo.svg';
import { FiArrowLeft } from 'react-icons/fi'
import api from '../../services/api'


export default function NewBook() {

    const [id, setId] = useState(null);
    const [author, setAuthor] = useState('');
    const [title, setTitle] = useState('');
    const [launchDate, setLaunchDate] = useState('');
    const [price, setPrice] = useState('');
    const { bookId } = useParams();

    const history = useHistory();

    const accessToken = localStorage.getItem('accessToken');
    const authorization = { headers: { Authorization: `Bearer ${accessToken}` } };

    useEffect(() => {

        if (bookId === 0) {
            return;
        }

        loadBook(bookId);

    }, [bookId]);

    async function loadBook() {

        try {

            const response = await api.get(`api/book/v1/${bookId}`, authorization);

            const { id, title, author, price, launchDate } = response.data;
            const adjustedDate = launchDate.split('T', 10)[0];

            setId(id);
            setTitle(title);
            setAuthor(author);
            setLaunchDate(adjustedDate);
            setPrice(price);


        } catch (error) {
            alert('Error recovering book.');
            history.push('/book');
        }

    }

    async function save(e) {

        e.preventDefault();

        const data = { title, author, launchDate, price };

        try {

            if (bookId === 0) {
                await api.post('api/book/v1', data, authorization);
            } else {
                data.id = id;
                await api.put('api/book/v1', data, authorization);
            }


        } catch (error) {
            alert('Error while recording book. Try again');
        }
        history.push('/book');

    }

    return (

        <div className="new-book-container">
            <div className="content">
                <section className="form">
                    <img src={logo} alt="Imagem" />
                    <h1> {bookId === 0 ? 'Add New' : 'Update'} Book</h1>
                    <p>Enter the new information and click on {bookId === 0 ? 'Add' : 'Update'}</p>
                    <Link className="black-link" to="/book">
                        <FiArrowLeft size={16} color="#251fc5" />Back to Books
                    </Link>
                </section>
                <form onSubmit={save}>
                    <input value={title} onChange={e => setTitle(e.target.value)} placeholder="Title"></input>
                    <input placeholder="Author" value={author} onChange={e => setAuthor(e.target.value)}  ></input>
                    <input type="date" value={launchDate} onChange={e => setLaunchDate(e.target.value)} ></input>
                    <input placeholder="Price" value={price} onChange={e => setPrice(e.target.value)}></input>
                    <button className="button" type="submit"> {bookId === 0 ? 'Add' : 'Update'}</button>
                </form>
            </div>
        </div>
    );
}