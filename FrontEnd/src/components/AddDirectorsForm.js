import React, { useState } from 'react';
import '../style.css';

function AddDirectorsForm(){
    const[name, setDirectorsName]=useState([])
    const[birthYear, setBirthYear] = useState([])

    const handleSubmit=(e)=>{
        e.preventDefault();

        const newAuthor={
            name:name,
            birthYear:birthYear
        }

        fetch("https://localhost:7125/api/Directors", {
            method:'POST',
            headers:{
                'Content-Type': 'application/json'
            },
            body:JSON.stringify(newAuthor)
        })
        .then(response=>{
            if(response.ok){
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
            <div className="form-center">
                <form onSubmit={handleSubmit}>
                <h1 style={{color:'white'}}>Add Directors in database</h1>

                    <div>
                        <input type='text' value={name} onChange={(e) =>setDirectorsName(e.target.value)} required placeholder='Directors Name'/>
                    </div>
                    <div>
                        <input type='number' value={birthYear} onChange={(e)=>setBirthYear(e.target.value)} required placeholder='Directors BirthYear'/>
                    </div>
                    <div>
                        <button type='Submit' className='butoni'><b>Shto</b></button>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default AddDirectorsForm