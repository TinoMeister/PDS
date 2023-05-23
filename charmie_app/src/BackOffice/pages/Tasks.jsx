import React, { useEffect } from 'react';

import { getData, postData, putData, deleteData } from '../..//db/db';

const Tasks = () => {
    const token = localStorage.getItem('user-token');
    const userData = localStorage.getItem('user-info');
    const user = JSON.parse(userData);

    useEffect(() => {
        get();
    });

    const get = async () => {
        let taskData = await getData(`Tasks/Client/${user['identity']['clients'][0]['id']}`, token);

        if (!taskData) return;

        console.log(taskData);
    };

    const post = async () => {
        let robots = [
            {
                "robotId": 0,
                "robot": {
                  "id": 0,
                  "name": "",
                },
            },
        ];

        let taskData = {
            'name': "",
            "initHour": "",
            "endHour": "",
            "weekDays": "",
            "repeat": false,
            "execution": false,
            "stop": false,
            "tasksRobots": JSON.stringify(robots)
        };

        let quantityMaterialsData = [
            {
                'quantity': 0,
                'taskId': 0,
                'material': {
                'name': ""
                }
            },
            {
                'quantity': 0,
                'taskId': 0,
                'material': {
                'name': ""
                }
            },
        ];

        await postData('Tasks', taskData, token);
        await postData('QuantityMaterials', quantityMaterialsData);
    }

    const put = async () => {
        let robots = [
            {
                "robotId": 0,
                "robot": {
                  "id": 0,
                  "name": "",
                },
            },
        ];

        let taskData = {
            'id': 0,
            'name': "",
            "initHour": "",
            "endHour": "",
            "weekDays": "",
            "repeat": false,
            "execution": false,
            "stop": false,
            "tasksRobots": JSON.stringify(robots)
        };

        let quantityMaterialsData = [
            {
                'quantity': 0,
                'taskId': 0,
                'material': {
                'name': ""
                }
            },
            {
                'quantity': 0,
                'taskId': 0,
                'material': {
                'name': ""
                }
            },
        ];

        await putData(`Tasks/${id}`, taskData, token);
        await putData('QuantityMaterials', quantityMaterialsData);
    }

    const del = async () => {
        await deleteData(`Tasks/${id}`, token);
    }

    return (
        <>
            TASK
        </>
    );
}

export default Tasks;