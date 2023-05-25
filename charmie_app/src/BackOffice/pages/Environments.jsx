import React, { useState, useEffect } from 'react';
import { Header } from '../components';
import { AiOutlineRobot } from 'react-icons/ai';
import { Link } from "react-router-dom";
import Select from 'react-select';
import { getData, postData, putData, deleteData } from '../../db/db';


const earningData = [
  {
    icon: <AiOutlineRobot />,
    title: 'Ambiente 1',
  },
  {
    icon: <AiOutlineRobot />,
    title: 'Ambiente 1',
  },
  {
    icon: <AiOutlineRobot />,
    title: 'Ambiente 1',
  },
  
];

const DataInfo = () => {
  return (
    <>
      {earningData.map((item) => (
        <div key={item.title} className="bg-white h-40 black bg-zinc-300 md:w-56 p-2 pt-5 rounded-2xl ">
        <p className="text-lg black mt-0 p-0">{item.title}</p>
      </div>
    ))}
  </>
);
};

const Environments = () => {
    const toolbarOptions = ['Search'];
    const editing = { allowDeleting: true, allowEditing: true };

    const token = localStorage.getItem('user-token');
    const userData = localStorage.getItem('user-info');
    const user = JSON.parse(userData);

    useEffect(() => {
        get();
    });

    const get = async () => {
      console.log(user['identity']['clients'][0]['id']);

      let environmentData = await getData(`Environments/${user['identity']['clients'][0]['id']}`, token);

      if (!environmentData) return;

      console.log(environmentData);
    };

    const post = async () => {
        let envData = {
          'name': "",
          'length': 0,
          'width': 0,
          'clientId': 0
        };

        let quantityMaterialsData = [
          {
            'quantity': 0,
            'environmentId': 0,
            'material': {
              'name': ""
            }
          },
          {
            'quantity': 0,
            'environmentId': 0,
            'material': {
              'name': ""
            }
          },
        ];

        await postData('Environments', envData, token);
        await postData('QuantityMaterials', quantityMaterialsData);
    }

    const put = async () => {
      let envData = {
        'id': 0,
        'name': "",
        'length': 0,
        'width': 0,
        'clientId': 0
      };

      let quantityMaterialsData = [
        {
          'quantity': 0,
          'environmentId': 0,
          'material': {
            'name': ""
          }
        },
        {
          'quantity': 0,
          'environmentId': 0,
          'material': {
            'name': ""
          }
        },
      ];

      await putData(`Environments/${id}`, envData, token);
      await putData('QuantityMaterials', quantityMaterialsData);
    }

    const del = async () => {
      await deleteData(`Environments/${id}`, token);
    }

    const options = [
      { value: "Ambiente 1", label: "1" },
      { value: "Ambiente 2", label: "2" },
      { value: "Ambiente 3", label: "3" },
      { value: "Ambiente 4", label: "4" },
      { value: "Ambiente 5", label: "5" },
    ];
    const [selected, setSelected] = useState(null);
  
    const handleChange = (selectedOption) => {
      setSelected(selectedOption);
    };

    return (
      <>
       <div className="flex justify-between items-center m-10 p-5 bg-white rounded-3xl">
       <p>
         <Link to="/backOffice/robot/environmentinfo" >
           <button class="bg-black hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full">Add Environment</button>
         </Link>
       </p>
       <div class="text-black font-bold py-2 w-48">
              <Select options={options} onChange={handleChange} autoFocus={true} />
            </div> 
       </div>
       <div className="flex flex-row justify-center md:m-10 mt-20 md:p-5 bg-white rounded-3xl"> 
         <Link to="/backOffice/robot/environmentinfo" >
           <DataInfo />
         </Link>
       </div>
       </>
     );
}

export default Environments;