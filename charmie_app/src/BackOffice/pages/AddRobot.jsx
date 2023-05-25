import React, { useState } from 'react';
import { AiOutlineRobot } from 'react-icons/ai';
import { Header } from '../components';
import Select from 'react-select'

const AddRobot = () => {
  const options = [
    { value: "Ambiente 1", label: "Ambiente 1" },
    { value: "Ambiente 2", label: "Ambiente 2" },
    { value: "Ambiente 3", label: "Ambiente 3" },
    { value: "Ambiente 4", label: "Ambiente 4" },
    { value: "Ambiente 5", label: "Ambiente 5" },
  ];
  const [selected, setSelected] = useState(null);

  const handleChange = (selectedOption) => {
    setSelected(selectedOption);
  };
 return (
     <>
        <div className="h-screen m-2 md:m-10 p-10 bg-white rounded-3xl">
        <p class="font-sans text-lg font-bold">Nome</p>
        <form>
            <p class="w-40">
                <label htmlFor="full-name" className="input-label"></label>
                <input type="text" className="input-control" id="username" autoComplete="off"></input>
            </p> 
            <p class="font-sans text-lg font-bold">Ambiente</p>
            <div class="text-black font-bold py-2 w-48">
              <Select options={options} onChange={handleChange} autoFocus={true} />
            </div> 
        </form>
        <div class="flex h-3/4 justify-center items-end"> 
          <button class="flex-none mx-2 bg-black hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full">Add Material</button>
        </div>
      
        
      
      
      
      
      
      
      
      </div>  
      </>
    );
}
  
  export default AddRobot;

  