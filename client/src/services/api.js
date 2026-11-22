
const API_URL = 'https://localhost:7178/api';

async function handleResponse(response) {
    const text = await response.text();

    let data;

    try {
        data = text ? JSON.parse(text) : null;
    } catch {
        data = text;
    }

    if (!response.ok) {
        if (Array.isArray(data)) {
            throw new Error(
                data.map(error => error.errorMessage || error.ErrorMessage || JSON.stringify(error)).join('\n')
            );
        }

        if (data && typeof data === 'object') {
            throw new Error(
                data.message || data.title || data.errors
                    ? JSON.stringify(data.errors || data.message || data.title)
                    : 'Request failed'
            );
        }

        throw new Error(data || 'Request failed');
    }

    return data;
}

export async function loginUser(userData) {
    const response = await fetch(`${API_URL}/Auth/login`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    });

    return await handleResponse(response);
}

export async function registerUser(userData) {
    const response = await fetch(`${API_URL}/Auth/register`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    });

    return await handleResponse(response);
}


export async function getMovies(accessToken) {
    const response = await fetch(`${API_URL}/Movie`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${accessToken}`
        }
    });

    return await handleResponse(response);
}

export async function addMovie(movieData, accessToken) {
    const response = await fetch(`${API_URL}/Movie`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${accessToken}`
        },
        body: JSON.stringify(movieData)
    });

    return await handleResponse(response);
}

export async function deleteMovie(id, accessToken) {
    const response = await fetch(`${API_URL}/Movie/${id}`, {
        method: 'DELETE',
        headers: {
            'Authorization': `Bearer ${accessToken}`
        }
    });

    return await handleResponse(response);
}
