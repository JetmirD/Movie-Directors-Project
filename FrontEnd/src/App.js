import logo from './logo.svg';
import './App.css';
import React, { useState, useEffect } from 'react';
import AddDirectorsForm from './components/AddDirectorsForm';
import './style.css';
import { BrowserRouter as Router, Link, Route, Routes, redirect } from 'react-router-dom'; // Import Routes
import NavBar from './components/NavBar';
import AddMovieForm from './components/AddMovieForm'
import UpdateDirectorForm from './components/UpdateDirectorsForm'

function App() {
  const [directors, setDirectors] = useState([]);
  const [movies, setMovies] = useState([]);
  const[year, setYear] = useState('')
const[directorName, setDirectorName] = useState('')
console.log(setDirectorName)


  const handleFilter=async()=>{
    const response = await fetch(`https://localhost:7125/api/Movie/FilterById?vitiLeshimit=${year}`)
    const data = await response.json();
   
    setMovies(data)
    console.log(data)
  }

  const fetchDirectorsByName = async()=>{
    const response = await fetch(`https://localhost:7125/api/Movie/FilterByAuthor?director=${directorName}`);
    const data = await response.json();
    setMovies(data);
  }

  useEffect(() => {
    //FetchDirectors
    fetch("https://localhost:7125/api/Directors")
      .then(response => response.json())
      .then(data => {
        setDirectors(data);
      })
      .catch(error => {
        console.error("Error fetching Directors:", error);
      });

    //Fetch Movies
    fetch("https://localhost:7125/api/Movie")
      .then(response => response.json())
      .then(data => {
        setMovies(data);
      })
      .catch(error => {
        console.error("Error fetching movies from database: ", error);
      });

  }, []);

  return (
    <Router>
      <div className="App">
      <NavBar/>
      <div style={{flexGrow:1, overflow:'auto'}}>
        <Routes>
          <Route path="/" element={
            <>
              <header className="App-header">

                    <label>Filtro Filma</label>
                    <input placeholder='Filtro filma nga viti e.g:"2012"' 
                    type='number'
                    value={year} 
                    onChange={e=>setYear(e.target.value)}/>

                    <button type='submit' onClick={handleFilter} >Filtro</button>
                      <h1>Movies nga databaza:</h1>
                      <ul>
                        {movies.map(movie => (
                          <li key={movie.movieId}>
                            {movie.title} i lansuar ne vitin {movie.releaseYear} i krijuar nga {movie.director.name}
                          </li>
                        ))}
                      </ul>

                      <label>Filtro Direktor</label>
                    <input placeholder='Filtro filma nga viti e.g:"2012"' 
                    type='text'
                    value={directorName} 
                    onChange={e=>setDirectorName(e.target.value)}/>

                    <button type='submit' onClick={fetchDirectorsByName} >Filtro</button>
                
                <h1>Directors nga databaza</h1>
                <ul>
                  {directors.map(director => (
                    <li key={director.directorId}>
                      <b><span>Id: {director.directorId}</span></b> Emri: "{director.name}" i lindur me date {director.birthYear}
                    </li>
                  ))}
                </ul>
<UpdateDirectorForm/>
              </header>
            </>
          } />
          <Route path="/add-directors" element={<AddDirectorsForm />} />
          <Route path="/add-movies" element={<AddMovieForm/>} />
          <Route path="/directors/update/:id" component={UpdateDirectorForm} />

        </Routes>
        </div>
      </div>
    </Router>
  );
}

export default App;
