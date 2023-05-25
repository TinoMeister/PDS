import React, { useState } from 'react';
import { AiOutlineRobot } from 'react-icons/ai';
import { Header } from '../components';
import Select from 'react-select'

const RobotInfo = () => {
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
        <div className="m-2 md:m-10 p-10 bg-white rounded-3xl">
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
            <div class="flex p-5 h-48 bg-gray-200 rounded-3xl border-2 border-black">
              <div class="flex-none p-2 ml-2">
                <p class="font-sans text-lg font-bold mb-3 ">Status:</p>
                <p class="font-sans text-xs font-bold">Cleaning Kitchen</p>
                <p class="font-sans text-xs font-bold">No problem</p>
              </div>
              <div class="flex items-center justify-center w-96">
                <button class="bg-black hover:bg-red-500 text-white font-bold py-2 px-4 rounded-full">Report</button>
              </div>
              <div class="flex-auto">
                <p class="font-sans text-lg font-bold">Data:</p>
                <div class="md:p-10 h-24 bg-white border-2 border-black">
                  <ol>
                    <li class="font-sans text-xs">Batery: 10</li>
                    <li class="font-sans text-xs">Data of robot: value</li>
                  </ol>
                </div>
              </div>
            </div>
            <div class="flex justify-center m-7">
            <button class="flex-none mx-2 bg-red-700 hover:bg-red-500 text-white font-bold py-2 px-4 rounded-full">Delete</button>
            <button class="flex-none mx-2 bg-black hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full">Update</button>
            </div>
        </form>
      
      
      
      
      
      
      
      
      
      
      </div>  
      </>
    );
}
  
  export default RobotInfo;

  