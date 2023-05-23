import React from 'react';

import { AiOutlineRobot } from 'react-icons/ai';

import { Header } from '../components';


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
        <div className="m-2 md:m-10 mt-2 p-2 md:p-10 bg-white rounded-3xl">
        <button class="bg-black hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full">
        Add Robot
        </button>
        </div>
            <div className="m-2 md:m-10 mt-24 p-2 md:p-10 bg-white rounded-3xl">

                <div className="flex m-3 flex-wrap justify-center gap-5 items-center">
                <DataInfo />
                </div>
            </div>
        </>
    );
}

export default Robots;