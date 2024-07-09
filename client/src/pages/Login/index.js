import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../services/api';
import logoImage from '../../assets/logo.svg';
import padlock from '../../assets/padlock.png';
import '../../global.css';
import './styles.css';

export default function Login() {
    const [userName, setUsername] = useState(''); // State to store username
    const [password, setPassword] = useState(''); // State to store password
    const navigate = useNavigate(); // Redirect user to another page

    async function login(e) {
        e.preventDefault(); // Prevents the default form behavior (refresh page)
        const data = { userName, password }; // Data to be sent to the server (username and password)
        try {
            const response = await api.post('/api/auth/v1/signin', data);
            localStorage.setItem('userName', userName); // Save username in the browser's local storage
            localStorage.setItem('accessToken', response.data.accessToken); // Save access token in the browser's local storage
            localStorage.setItem('refreshToken', response.data.refreshToken); // Save refresh token in the browser's local storage
            //debugger;
            console.log(response);
            navigate('/books'); // Redirect user to the profile page
        } catch (err) {
            alert('Login failed. Try again.'); // Alert the user if login fails
        }
    }

    return (
        <div className="login-container">
            <section className="form">
                <img src={logoImage} alt="Be The Hero" />
                <form onSubmit={login}>
                    <h1>Access your account</h1>
                    <input
                        placeholder="Username"
                        value={userName}
                        onChange={e => setUsername(e.target.value)}
                    />
                    <input
                        type="password" placeholder="Password"
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                    />
                    <button className="button" type="submit">Login</button>
                </form>
            </section>
            <img src={padlock} alt="Padlock" />
        </div>
    );
}
