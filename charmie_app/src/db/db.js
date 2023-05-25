//const url = "http://172.16.10.13:7064/api/"; // school
const url = "http://192.168.1.64:7064/api/"; // resi
//const url = "http://192.168.1.70:7064/api/"; // home

export async function getData(camp = '', token = '') {
    try {
        const res = await fetch(`${url + camp}`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token, 
            }
        });

        if (res.status >= 400) return null;

        return await res.json();

    } catch (error) {
        console.error(`Error: ${error.message}`);
    }
}

export async function postData(camp = '', data = '', token = '') {
    try {
        const res = await fetch(`${url + camp}`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token, 
            },
            body: JSON.stringify(data),
        });

        if (res.status >= 400) return null;

        return await res.json();
        
    } catch (error) {
        console.error(`Error: ${error.message}`);
    }
}

export async function putData(camp = '', data = '', token = '') {
    try {
        const res = await fetch(`${url + camp}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token, 
            },
            body: JSON.stringify(data),
        });

        return res.status;
    } catch (error) {
        console.error(`Error: ${error.message}`);
    }
}

export async function deleteData(camp = '', data = '', token = '') {
    try {
        const res = await fetch(`${url + camp}`, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token, 
            },
            body: JSON.stringify(data),
        });

        return res.status;
    } catch (error) {
        console.error(`Error: ${error.message}`);
    }
}