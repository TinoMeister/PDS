import React, { useState } from 'react';
import { AiOutlineRobot } from 'react-icons/ai';
import { Header } from '../components';
import { Link } from "react-router-dom";

const EnvironmentInfo = () => {
 return (
     <>
        <div className="m-2 md:m-10 p-10 bg-white rounded-3xl">
        <p class="font-sans text-lg font-bold">Nome</p>
        <form>
            <p class="w-max mb-10">
                <label htmlFor="full-name" className="input-label"></label>
                <input type="text" className="input-control" id="username" autoComplete="off" required></input>
            </p>
            <p class="font-sans text-lg font-bold">Dimensões</p>
            <p class=" flex gap-1 w-max mb-10">
                <label htmlFor="full-name" className="input-label"></label>
                <input type="text" className="input-control" id="Largura" autoComplete="off" required></input>
                <label htmlFor="full-name" className="input-label"></label>
                <input type="text" className="input-control" id="Largura" autoComplete="off" required></input>
            </p> 
            <p class="font-sans text-lg font-bold">Materials</p>
            <p class=" flex gap-4 mt-4 w-max">
                <label htmlFor="full-name" className="input-label"></label>
                <input type="text" className="input-control" id="Largura" autoComplete="off" required></input>
                <label htmlFor="full-name" className="input-label"></label>
                <input type="text" className="input-control" id="Largura" autoComplete="off" required></input>
            </p>
            <p class=" flex gap-4 mt-4 w-max">
                <label htmlFor="full-name" className="input-label"></label>
                <input type="text" className="input-control" id="Largura" autoComplete="off" required></input>
                <label htmlFor="full-name" className="input-label"></label>
                <input type="text" className="input-control" id="Largura" autoComplete="off" required></input>
            </p>
            <p class=" flex gap-4 mt-4 w-max">
                <label htmlFor="full-name" className="input-label"></label>
                <input type="text" className="input-control" id="Largura" autoComplete="off" required></input>
                <label htmlFor="full-name" className="input-label"></label>
                <input type="text" className="input-control" id="Largura" autoComplete="off" required></input>
            </p>
            <div class="flex justify-center m-7">
              <button class="flex-none mx-2 bg-black hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full">Add Material</button>
            </div>
        </form>
      
      
      
      
      
      
      
      
      
      
      </div>  
      </>
    );
}
  
  export default EnvironmentInfo;

  