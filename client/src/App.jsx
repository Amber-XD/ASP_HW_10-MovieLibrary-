
import { useState } from 'react';
import Login from './components/Login';
import Register from './components/Register';
import MovieList from './components/MovieList';
import './App.css';

function App() {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [accessToken, setAccessToken] = useState('');
    const [showRegister, setShowRegister] = useState(false);

    function handleLoginSuccess(token) {
        setAccessToken(token);
        setIsLoggedIn(true);
    }

    function handleLogout() {
        setIsLoggedIn(false);
        setAccessToken('');
    }

    if (!isLoggedIn) {
        return (
            <div className="app">
                {showRegister ? (
                    <Register
                        onSwitchToLogin={() => setShowRegister(false)}
                    />
                ) : (
                    <Login
                        onLoginSuccess={handleLoginSuccess}
                        onSwitchToRegister={() => setShowRegister(true)}
                    />
                )}
            </div>
        );
    }

    return (
        <div className="app movie-app">
            <div className="movie-page">
                <button onClick={handleLogout}>
                    Logout
                </button>

                <MovieList accessToken={accessToken} />
            </div>
        </div>
    );
}

export default App;
