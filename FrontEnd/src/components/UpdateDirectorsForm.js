import { useState } from 'react';

function UpdateDirector() {
    const [directorID, setDirectorId] = useState('');
    const [directorName, setDirectorName] = useState('');
    const [birthYear, setBirthYear] = useState('');
    const [message, setMessage] = useState('');

    const updateDirectorData = async () => {
        const response = await fetch(`https://localhost:7125/api/Directors/${directorID}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                directorID: directorID,
                name: directorName,
                BirthYear: birthYear
            })
        });

        if (response.ok) {
            alert("Successfully updated the directors data")
        } else {
            setMessage('Failed to update Director. Please try again.');
        }
    };

    return (
        <div>
            <h2>Update Director</h2>

            <label>Director ID:</label>
            <input type="number" value={directorID} onChange={e => setDirectorId(e.target.value)} />
            <br/>

            <label>Director Name:</label>
            <input type="text" value={directorName} onChange={e => setDirectorName(e.target.value)} />
            <br/>

            <label>Birth Year:</label>
            <input type="number" value={birthYear} onChange={e => setBirthYear(e.target.value)} />
            <br/>

            <button onClick={updateDirectorData}>Update Director</button>
            <br/>
            <br/>

            {message && <p>{message}</p>}
        </div>
    );
}

export default UpdateDirector;
