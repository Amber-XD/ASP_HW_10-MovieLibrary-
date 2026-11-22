
import { useEffect, useState } from 'react';
import { getMovies, addMovie, deleteMovie } from '../services/api';
import AddMovie from './AddMovie';

function MovieList({ accessToken }) {
    const [movies, setMovies] = useState([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(true);
    const [showAddForm, setShowAddForm] = useState(false);

    async function loadMovies() {
        try {
            setError('');
            const data = await getMovies(accessToken);
            setMovies(data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    }

    async function handleDelete(id) {
        try {
            await deleteMovie(id, accessToken);

            setMovies(currentMovies =>
                currentMovies.filter(movie => movie.id !== id)
            );
        } catch (err) {
            setError(err.message);
        }
    }

    async function handleAddMovie(movieData) {
    const newMovie = await addMovie(movieData, accessToken);

    setMovies(currentMovies => [...currentMovies, newMovie]);
    setShowAddForm(false);
}

    useEffect(() => {
        loadMovies();
    }, [accessToken]);

    if (loading) {
        return <p>Loading movies...</p>;
    }

    return (
        <div className="movie-list">
            <h1>My Movies</h1>

            <button onClick={() => setShowAddForm(true)}>
                Add Film
            </button>
                
            {showAddForm && (
            <AddMovie
                onAddMovie={handleAddMovie}
                onCancel={() => setShowAddForm(false)}
                    />
                )}
            {error && <p className="error-message">{error}</p>}

            {movies.length === 0 ? (
                <p>You don't have any movies yet.</p>
            ) : (
                <div className="movie-grid">
                    {movies.map(movie => (
                        <div className="movie-card" key={movie.id}>
                            {movie.posterUrl && (
                                <img
                                    src={movie.posterUrl}
                                    alt={movie.name}
                                    className="movie-poster"
                                />
                            )}

                            <div className="movie-card-content">
                                <h2>{movie.name}</h2>

                                <p>{movie.description}</p>

                                <p><strong>Genre:</strong> {movie.genre}</p>

                                <p><strong>Release year:</strong> {movie.releaseYear}</p>

                                <p><strong>Country:</strong> {movie.country}</p>

                                <button
                                    className="remove-movie-button"
                                    onClick={() => handleDelete(movie.id)}
                                    title="Remove movie"
                                >
                                    ×
                                </button>
                            </div>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
}

export default MovieList;
