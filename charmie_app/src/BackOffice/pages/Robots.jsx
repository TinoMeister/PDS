import React from 'react';

import { AiOutlineRobot } from 'react-icons/ai';

import { Header } from '../components';


const earningData = [
    {
      icon: <AiOutlineRobot />,
      amount: '39,354',
      percentage: '-4%',
      title: 'Customers',
      iconColor: '#03C9D7',
      iconBg: '#E5FAFB',
      pcColor: 'red-600',
    },
    {
      icon: <AiOutlineRobot />,
      amount: '4,396',
      percentage: '+23%',
      title: 'Products',
      iconColor: 'rgb(255, 244, 229)',
      iconBg: 'rgb(254, 201, 15)',
      pcColor: 'green-600',
    },
    {
      icon: <AiOutlineRobot />,
      amount: '423,39',
      percentage: '+38%',
      title: 'Sales',
      iconColor: 'rgb(228, 106, 118)',
      iconBg: 'rgb(255, 244, 229)',
  
      pcColor: 'green-600',
    },
    {
      icon: <AiOutlineRobot />,
      amount: '39,354',
      percentage: '-12%',
      title: 'Refunds',
      iconColor: 'rgb(0, 194, 146)',
      iconBg: 'rgb(235, 250, 242)',
      pcColor: 'red-600',
    },
  ];

const DataInfo = () => {
    return (
      <>
        {earningData.map((item) => (
          <div key={item.title} className="bg-white h-44 dark:text-gray-200 dark:bg-secondary-dark-bg md:w-56  p-4 pt-9 rounded-2xl ">
            <button
              type="button"
              style={{ color: item.iconColor, backgroundColor: item.iconBg }}
              className="text-2xl opacity-0.9 rounded-full  p-4 hover:drop-shadow-xl"
            >
              { item.icon }
            </button>
            <p className="mt-3">
              <span className="text-lg font-semibold">{item.amount}</span>
            </p>
            <p className="text-sm text-gray-400  mt-1">{item.title}</p>
          </div>
        ))}
      </>
    );
  };

const Robots = () => {

    return (
        <>
            <div className="m-2 md:m-10 mt-24 p-2 md:p-10 bg-white rounded-3xl">
                <Header category="Page" title="Robots" />

                <div className="flex m-3 flex-wrap justify-center gap-5 items-center">
                <DataInfo />
                </div>

                <div className="flex m-3 flex-wrap justify-center gap-5 items-center">
                <DataInfo />
                </div>

                <div className="flex m-3 flex-wrap justify-center gap-5 items-center">
                <DataInfo />
                </div>

                <div className="flex m-3 flex-wrap justify-center gap-5 items-center">
                <DataInfo />
                </div>
            </div>
        </>
    );
}

export default Robots;