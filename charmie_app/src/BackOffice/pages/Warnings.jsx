import React, { useEffect } from 'react';

const Warnings = () => {
    const token = localStorage.getItem('user-token');
    const userData = localStorage.getItem('user-info');
    const user = JSON.parse(userData);

    useEffect(() => {
        get();
    });

    const get = async () => {
        let warningData = await getData(`Robots/Client/${user['identity']['clients'][0]['id']}`, token);
    
        if (!warningData) return;
    
        console.log(warningData);
    };

    const put = async () => {
        let warningData = {
          'id': 0,
          'message': "",
          'hourDay': "",
          'robotId': 0,
          'identityId': 0
        };
  
        await putData(`Warnings/${id}`, warningData, token);
    };
  
    const del = async () => {
        await deleteData(`Warnings/${id}`, token);
    };


    return (
        <>
        </>
    );
}

export default Warnings;