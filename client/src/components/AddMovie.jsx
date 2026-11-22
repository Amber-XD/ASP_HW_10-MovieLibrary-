import { useState } from 'react';

function AddMovie({ onAddMovie, onCancel }) {
    const [formData, setFormData] = useState({
        name: '',
        description: '',
        releaseDate: '',
        genre: '',
        director: '',
        posterUrl: '',
        movieUrl: '',
        keyword: '',
        country: ''
    });

    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    function handleChange(event) {
        const { name, value } = event.target;

        setFormData({
            ...formData,
            [name]: value
        });
    }

    async function handleSubmit(event) {
        event.preventDefault();

        setLoading(true);
        setError('');

        try {
            await onAddMovie({
                ...formData,
                releaseDate: `${formData.releaseDate}T00:00:00`
            });
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    }

    return (
        <div className="add-movie-form">
            <h2>Add New Movie</h2>

            <form onSubmit={handleSubmit}>
                <input name="name" value={formData.name}
                    onChange={handleChange} placeholder="Movie name" required />

                <textarea name="description" value={formData.description}
                    onChange={handleChange} placeholder="Description" required />

                <input name="releaseDate" type="date" value={formData.releaseDate}
                    onChange={handleChange} required />

                <input name="genre" value={formData.genre}
                    onChange={handleChange} placeholder="Genre" required />

                <input name="director" value={formData.director}
                    onChange={handleChange} placeholder="Director" required />

                <input name="posterUrl" value={formData.posterUrl}
                    onChange={handleChange} placeholder="Poster URL" />

                <input name="movieUrl" value={formData.movieUrl}
                    onChange={handleChange} placeholder="Movie URL" />

                <input name="keyword" value={formData.keyword}
                    onChange={handleChange} placeholder="Keyword" />

                <input name="country" value={formData.country}
                    onChange={handleChange} placeholder="Country" required />

                {error && <p className="error-message">{error}</p>}

                <button type="submit" disabled={loading}>
                    {loading ? 'Adding...' : 'Add Movie'}
                </button>

                <button type="button" onClick={onCancel}>
                    Cancel
                </button>
            </form>
        </div>
    );
}

export default AddMovie;