import React, { useEffect, useState } from 'react';
import '../style.css';


function AddMovieForm(){
    const [title, setTitle] =useState('')
    const[releaseYear, setReleaseYear] = useState('')
    const [directorID, setDirectorID] =useState('')
    const [director, setDirector]=useState([])

useEffect(()=>{
    fetch("https://localhost:7125/api/Directors")
    .then(response => {
        if (response.ok) {
          return response.json();
        } else {
          throw new Error('Failed to fetch Directors');
        }
      })   
    .then(data=>{
        setDirector(data)
    })
    .catch(error=>{
        console.log("Failed to fetch: ", error)
    })
}, [])




    const handleSubmit=(e)=>{
        e.preventDefault();

        const newMovie={
            title:title,
            releaseYear:releaseYear,
            directorId:directorID,
        }
        fetch("https://localhost:7125/api/Movie",{
            
            method:'POST',
            headers:{
                'Content-Type': 'application/json'
            },
            body:JSON.stringify(newMovie)
            
        })
        .then(response=>{
            if(response.ok){
                alert("Succesfully saved the movie in the database")

                return response.json()

            }else{
                
                console.log('Failed to fetch the author data')
            }
        })
        .then(data=>{
            console.log("Author saved succesfully", data);
        })
        .catch(error =>{
            console.log("Author can not be saved in database", error)
        })

    }

    return(
        <div style={{backgroundColor:'#282C34', height:'100vh', width:'100%', display:'flex', alignItems:'center', justifyContent:'center', flexDirection:'column'}}>

        <div>
            <form onSubmit={handleSubmit}>
            <h1 style={{color:'white'}}>Add Movies in database</h1>


            <div style={{marginTop:'10px', marginBottom:'2px'}}>
                    <label style={{color:"white"}}>Movie title</label>
                </div>

                <div>
                    <input type='text' value={title} onChange={(e)=>setTitle(e.target.value)} required placeholder="Title e.g: 'Nentoka' "/>
                </div>

                <div style={{marginTop:'10px', marginBottom:'2px'}}>
                    <label style={{color:"white"}}>Movie release Year</label>
                </div>

                <div>
                    <input type='number' value={releaseYear} onChange={(e)=>setReleaseYear(e.target.value)} required placeholder="Release Year"/>
                </div>

                <div style={{marginTop:'10px', marginBottom:'2px'}}>
                    <label style={{color:"white"}}>Select an Director</label>
                </div>

                <div>
                <select value={directorID} onChange={(e) => {
                    console.log("Selected Director ID:", e.target.value); 
                    setDirectorID(e.target.value);
                        }} required>
                    <option value="">Select an Director</option>
                    {director.map(directors => (
                        <option key={directors.directorId} value={directors.directorId}>
                            {directors.name}
                        </option>
                    ))}
                </select>

                </div>
                
                <button type='submit' style={{cursor:'pointer'}}>Shto</button>
          
            </form>
        </div>
        
        </div>
    )
    
}

export default AddMovieForm