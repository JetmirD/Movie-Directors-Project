import React from 'react';
import { Link } from 'react-router-dom';

function NavBar() {
    return (
        <nav className="navbar">
            <Link to="/">Home</Link>
            <Link to="/add-directors">Add Directors</Link>
            <Link to="add-movies">Add Movies</Link>
            {/* Add other links as needed */}
        </nav>
    );
}

export default NavBar;
