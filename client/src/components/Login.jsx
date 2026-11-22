
import { useState } from 'react';
import { loginUser } from '../services/api';

function Login({ onLoginSuccess, onSwitchToRegister }) {
    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    async function handleSubmit(event) {
        event.preventDefault();

        setError('');
        setLoading(true);

        try {
            const data = await loginUser({ login, password });

            onLoginSuccess(data.accessToken);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    }

    return (
        <div className="auth-container">
            <h2>Login</h2>

            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    placeholder="Login"
                    value={login}
                    onChange={(event) => setLogin(event.target.value)}
                    required
                />

                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(event) => setPassword(event.target.value)}
                    required
                />

                {error && <p className="error-message">{error}</p>}

                <button type="submit" disabled={loading}>
                    {loading ? 'Logging in...' : 'Login'}
                </button>
            </form>

            <p>
                Don't have an account?{' '}
                <button type="button" onClick={onSwitchToRegister}>
                    Register
                </button>
            </p>
        </div>
    );
}

export default Login;
