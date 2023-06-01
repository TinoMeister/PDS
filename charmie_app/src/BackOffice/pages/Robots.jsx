import React, { useState, useEffect, useCallback } from 'react';
import { Link } from "react-router-dom";
import { getData } from '../../db/db';
import ImportCss from '../../ImportCss';

const DataInfo = ({ robots }) => {
  return (
    <>
      {robots.map((robot) => (
        <Link key={robot.name} to="/backOffice/robot/robotinfo" state={robot} >
          <div key={robot.name} className="bg-white h-40 black bg-zinc-300 md:w-56 p-2 pt-5 rounded-2xl ">
            <p className="text-lg black mt-0 p-0">{robot.name}</p>

            <p className="mt-3"> <span className="text-sm font-semibold">{"Test"}</span> </p>
            <p> <span className="text-sm font-semibold">{robot.state}</span> </p>
          </div>
        </Link>
      ))}
    </>
  );
};

const Robots = () => {
  ImportCss('Back');
  const token = localStorage.getItem('user-token');
  const userData = localStorage.getItem('user-info');
  const user = JSON.parse(userData);

  const [robots, setRobots] = useState([]);

  const get = useCallback(async () => {  
    const data = await getData(`Robots/Client/${user['identity']['clients'][0]['id']}`, token);
    if (data != null) setRobots(data);
  }, []);

  useEffect(() => {
    get();
  }, []);

  return (
     <>
      <div className="flex justify-between items-center m-10 p-5 bg-white rounded-3xl">
      <p>
        <Link to="/backOffice/robot/addrobot" >
          <button className="bg-black hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full ">Add Robot</button>
        </Link>
      </p>
      </div>
      <div className="flex flex-row justify-center items-center md:m-10 mt-20 md:p-5 bg-white rounded-3xl"> 
        <DataInfo robots={ robots } />
      </div>
      </>
    );
}

export default Robots;