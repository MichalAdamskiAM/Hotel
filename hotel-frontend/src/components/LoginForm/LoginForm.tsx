import { useState } from 'react';
import { login } from '../../services/authService';
import { useAuth } from '../../contexts/AuthContext'
import { useNavigate } from 'react-router-dom';

export function LoginForm(){
    const { saveToken } = useAuth()
    const navigate = useNavigate();
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    async function handleSubmit(e: React.SubmitEvent<HTMLFormElement>){
        e.preventDefault();

        const response = await login({
            email,
            password
        });

        saveToken(response.token);
        navigate('/dashboard');
    }

    return(
        <form onSubmit={handleSubmit}>
            <input type="email" value={email} onChange={e => setEmail(e.target.value)} /><br />
            <input type="password" value={password} onChange={e => setPassword(e.target.value)} /><br />
            <input type="submit" value="Sign in"/>
        </form>
    );
}