import React, { useState, useEffect, useCallback } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import Select from 'react-select';
import { getData, putData, deleteData } from '../../db/db';
import ImportCss from '../../ImportCss';

function RobotInfo()
{
  ImportCss('Back');
  const navigate = useNavigate();

  const location = useLocation();
  const robot = location.state;

  const token = localStorage.getItem('user-token');
  const userData = localStorage.getItem('user-info');
  const user = JSON.parse(userData);

  const [name, setName] = useState(robot.name);
  const [options, setOptions]  = useState([]);
  const [selected, setSelected] = useState(null);

  const get = useCallback(async () => {  
    const data = await getData(`Environments/${user['identity']['clients'][0]['id']}`, token);

    let query = [];
    let amb = [];

    data.map((item) => {
      query.push({ value: item.name, label: item.name, id: item.id });
      if (item.id === robot.environmentId) amb = { value: item.name, label: item.name, id: item.id };
    });

    setOptions(query);
    setSelected(amb);
  }, []);

  const update = async (e) => {
    e.preventDefault();

    if (!name)
    {
        alert("Campos Vazios!!!");
        return;
    }

    let robotData = {
        'id': robot.id,
        'name': name,
        'environmentId': selected.id
    };

    const resp = await putData(`Robots/${robot.id}`, robotData, token);

    if (resp != null) alert("Atualizado com sucesso");
    else alert("Nao foi possivel atualizar"); 
  };

  const remove = async (e) => {
    e.preventDefault();

    const resp = await deleteData(`Robots/${robot.id}`, "", token);

    if (resp === 200) 
    {
      alert("Removido com sucesso");
      navigate('/backOffice/robots');
    }
    else alert("Nao foi possivel remover"); 
  };

  useEffect(() => {
    get();
  }, []);

  const handleChange = (selectedOption) => {
    setSelected(selectedOption);
  };

  return (
     <>
        <div className="m-2 md:m-10 p-10 bg-white rounded-3xl">
        <p className="font-sans text-lg font-bold">Nome</p>
        <form>
            <p className="w-40">
                <label htmlFor="full-name" className="input-label"></label>
                <input type="text" className="input-control" value={name} autoComplete="off" onChange={(e) => setName(e.target.value)}></input>
            </p> 
            <p className="font-sans text-lg font-bold">Ambiente</p>
            <div className="text-black font-bold py-2 w-48">
              <Select options={options} value={selected} onChange={handleChange} autoFocus={true} />
            </div> 
            <div className="flex p-5 h-48 bg-gray-200 rounded-3xl border-2 border-black">
              <div className="flex-none p-2 ml-2">
                <p className="font-sans text-lg font-bold mb-3 ">Status:</p>
                <p className="font-sans text-xs font-bold">Cleaning Kitchen</p>
                <p className="font-sans text-xs font-bold">{ robot.state }</p>
              </div>
              <div className="flex items-center justify-center w-96">
                <button className="bg-black hover:bg-red-500 text-white font-bold py-2 px-4 rounded-full">Report</button>
              </div>
              <div className="flex-auto">
                <p className="font-sans text-lg font-bold">Data:</p>
                <div className="md:p-10 h-24 bg-white border-2 border-black">
                  <ol>
                    <li className="font-sans text-xs">Batery: 10</li>
                    <li className="font-sans text-xs">Data of robot: value</li>
                  </ol>
                </div>
              </div>
            </div>
            <div className="flex justify-center m-7">
            <button className="flex-none mx-2 bg-red-700 hover:bg-red-500 text-white font-bold py-2 px-4 rounded-full" onClick={remove}>Delete</button>
            <button className="flex-none mx-2 bg-black hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full" onClick={update}>Update</button>
            </div>
        </form>
      </div>  
      </>
    );
}
  
export default RobotInfo;

  