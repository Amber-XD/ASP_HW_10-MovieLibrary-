
import { useState } from 'react';
import { registerUser } from '../services/api';

function Register({ onSwitchToLogin }) {
    const [formData, setFormData] = useState({
        name: '',
        email: '',
        password: '',
        confirmPassword: '',
        phone: '',
        photoPath: '',
        age: '',
        login: ''
    });

    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');
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

        setError('');
        setSuccess('');
        setLoading(true);

        try {
            const result = await registerUser({
                ...formData,
                age: Number(formData.age)
            });

            if (result.success) {
                setSuccess('Registration successful! You can now log in.');
            } else {
                setError(result.errors?.join('\n') || 'Registration failed.');
            }
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    }

    return (
        <div className="auth-container">
            <h2>Register</h2>

            <form onSubmit={handleSubmit}>
                <input
                    name="name"
                    type="text"
                    placeholder="Name"
                    value={formData.name}
                    onChange={handleChange}
                    required
                />

                <input
                    name="email"
                    type="email"
                    placeholder="Email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                />

                <input
                    name="login"
                    type="text"
                    placeholder="Login"
                    value={formData.login}
                    onChange={handleChange}
                    required
                />

                <input
                    name="password"
                    type="password"
                    placeholder="Password"
                    value={formData.password}
                    onChange={handleChange}
                    required
                />

                <input
                    name="confirmPassword"
                    type="password"
                    placeholder="Confirm password"
                    value={formData.confirmPassword}
                    onChange={handleChange}
                    required
                />

                <input
                    name="phone"
                    type="tel"
                    placeholder="Phone"
                    value={formData.phone}
                    onChange={handleChange}
                    required
                />

                <input
                    name="photoPath"
                    type="text"
                    placeholder="Photo URL"
                    value={formData.photoPath}
                    onChange={handleChange}
                    required
                />

                <input
                    name="age"
                    type="number"
                    placeholder="Age"
                    value={formData.age}
                    onChange={handleChange}
                    required
                />

                {error && <p className="error-message">{error}</p>}
                {success && <p className="success-message">{success}</p>}

                <button type="submit" disabled={loading}>
                    {loading ? 'Registering...' : 'Register'}
                </button>
            </form>

            <p>
                Already have an account?{' '}
                <button type="button" onClick={onSwitchToLogin}>
                    Login
                </button>
            </p>
        </div>
    );
}

export default Register;
