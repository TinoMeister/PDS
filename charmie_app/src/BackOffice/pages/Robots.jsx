import React, { useEffect } from 'react';
import { AiOutlineRobot } from 'react-icons/ai';
import { Header } from '../components';
import { Link } from "react-router-dom";

const earningData = [
  {
    icon: <AiOutlineRobot />,
    task: 'Cleaning Kitchen',
    title: 'Robot 1',
    state: 'No Problem',
  },
  {
    icon: <AiOutlineRobot />,
    task: 'Charging',
    state: 'Low Battery',
    title: 'Robot 2',
  },
  {
    icon: <AiOutlineRobot />,
    title: 'Robot 4',
    task: 'Nothing',
    state: 'No Problem',
  },
];

const DataInfo = () => {
  return (
    <>
      {earningData.map((item) => (
        <div key={item.title} className="bg-white h-40 black bg-zinc-300 md:w-56 p-2 pt-5 rounded-2xl ">
        <p className="text-lg black mt-0 p-0">{item.title}</p>

        <p className="mt-3"> <span className="text-sm font-semibold">{item.task}</span> </p>
        <p> <span className="text-sm font-semibold">{item.state}</span> </p>
      </div>
    ))}
  </>
);
};

const Robots = () => {
 return (
     <>
      <div className="flex justify-between items-center m-10 p-5 bg-white rounded-3xl">
      <p>
        <Link to="/backOffice/robot/addrobot" >
          <button class="bg-black hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full ">Add Robot</button>
        </Link>
      </p>
      </div>
      <div className="flex flex-row justify-center items-center md:m-10 mt-20 md:p-5 bg-white rounded-3xl"> 
        <Link to="/backOffice/robot/robotinfo" >
          <DataInfo />
        </Link>
      </div>
      </>
    );
}

export default Robots;