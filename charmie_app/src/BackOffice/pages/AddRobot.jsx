import React, { useState, useEffect, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import Select from 'react-select';
import { getData, postData } from '../../db/db';
import ImportCss from '../../ImportCss';

const AddRobot = () => {
  ImportCss('Back');
  const navigate = useNavigate();

  const token = localStorage.getItem('user-token');
  const userData = localStorage.getItem('user-info');
  const user = JSON.parse(userData);

  const [options, setOptions]  = useState([]);
  const [selected, setSelected] = useState([]);
  const [name, setName] = useState('');

  const get = useCallback(async () => {  
    const data = await getData(`Environments/${user['identity']['clients'][0]['id']}`, token);

    let query = [];

    data.map((item) => {
      query.push({ value: item.name, label: item.name, id: item.id });
    });

    setOptions(query);
  }, []);

  
  const add = async (e) => {
    e.preventDefault();

    if (!name)
    {
        alert("Campos Vazios!!!");
        return;
    }

    let robotData = {
        'name': name,
        'environmentId': selected.id
    };

    const resp = await postData('Robots', robotData, token);

    if (resp != null)
    {
      alert("Inserido com sucesso");
      navigate('/backOffice/robots');
    }
    else alert("Nao foi possivel inserir");
  };

  useEffect(() => {
    get();
  }, []);
  
  const handleChange = (selectedOption) => {
    setSelected(selectedOption);
  };

  return (
     <>
        <div className="h-screen m-2 md:m-10 p-10 bg-white rounded-3xl">
        <p className="font-sans text-lg font-bold">Nome</p>
        <form>
            <p className="w-40">
                <label htmlFor="full-name" className="input-label"></label>
                <input type="text" className="input-control" value={name} autoComplete="off" onChange={(e) => setName(e.target.value)}></input>
            </p> 
            <p className="font-sans text-lg font-bold">Ambiente</p>
            <div className="text-black font-bold py-2 w-48">
              <Select options={options} onChange={handleChange} autoFocus={true} />
            </div> 
        </form>
        <div className="flex h-3/4 justify-center items-end"> 
          <button className="flex-none mx-2 bg-black hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full" onClick={add}>Add Robot</button>
        </div>
      </div>  
      </>
    );
}
  
  export default AddRobot;

  