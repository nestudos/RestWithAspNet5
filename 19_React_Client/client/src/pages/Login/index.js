import React, { useState } from "react";
import { useHistory } from 'react-router-dom';

import './styles.css';
import logo from '../../assets/logo.svg';
import padlock from '../../assets/padlock.svg';

import api from "../../services/api";

export default function Header(props) {
    
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    
    const history = useHistory();
    

    async function login(e){
        e.preventDefault();

        const data = { userName, password };

        try {
            
            const response = await api.post('api/auth/v1/signin', data);

            localStorage.setItem('userName', userName);
            localStorage.setItem('accessToken', response.data.accessToken);
            localStorage.setItem('refreshToken', response.data.refreshToken);

            history.push('/book');

        } catch (error) {
            alert('Login Failed! Try again.')
        }
    }

    return (
        <div className="login-container">
            <section className="form">
                <img src={logo} alt="login" />
                <form onSubmit={login}>
                    <h1>Acesse a sua conta</h1>
                    <input type="text" placeholder="Username" value={userName} onChange={e => setUserName(e.target.value) } />
                    <input type="password" placeholder="Password" value={password} onChange={e => setPassword(e.target.value)} />
                    <button className="button" type="submit">Login</button>
                </form>
            </section>
            <img src={padlock} alt="login" />

        </div>
    );
}


