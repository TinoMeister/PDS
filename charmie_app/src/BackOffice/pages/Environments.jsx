import React, { useEffect } from 'react';
import { GridComponent, Inject, ColumnsDirective, ColumnDirective, Search, Page } from '@syncfusion/ej2-react-grids';
import { Header } from '../components';

import { getData, postData, putData, deleteData } from '../../db/db';

import { GrLocation } from 'react-icons/gr';
import avatar from '../data/avatar.jpg';
import avatar2 from '../data/avatar2.jpg';
import avatar3 from '../data/avatar3.png';
import avatar4 from '../data/avatar4.jpg';

const employeesData = [
    {
      EmployeeID: 1,
      Name: 'Nancy Davolio',
      Title: 'Sales Representative',
      HireDate: '01/02/2021',
      Country: 'USA',
      ReportsTo: 'Carson',
      EmployeeImage:
      avatar3,
    },
    {
      EmployeeID: 2,
      Name: 'Nasimiyu Danai',
      Title: 'Marketing Head',
      HireDate: '01/02/2021',
      Country: 'USA',
      ReportsTo: 'Carson',
      EmployeeImage:
        avatar3,
    },
    {
      EmployeeID: 3,
      Name: 'Iulia Albu',
      Title: 'HR',
      HireDate: '01/02/2021',
      Country: 'USA',
      ReportsTo: 'Carson',
      EmployeeImage:
        avatar4,
    },
    {
      EmployeeID: 4,
      Name: 'Siegbert Gottfried',
      Title: 'Marketing Head',
      HireDate: '01/02/2021',
      Country: 'USA',
      ReportsTo: 'Carson',
      EmployeeImage:
        avatar2,
    },
    {
      EmployeeID: 5,
      Name: 'Omar Darobe',
      Title: 'HR',
      HireDate: '01/02/2021',
      Country: 'USA',
      ReportsTo: 'Carson',
      EmployeeImage:
        avatar,
    },
    {
      EmployeeID: 4,
      Name: 'Penjani Inyene',
      Title: 'Marketing Head',
      HireDate: '01/02/2021',
      Country: 'USA',
      ReportsTo: 'Carson',
      EmployeeImage:
        avatar,
    },
    {
      EmployeeID: 5,
      Name: 'Miron Vitold',
      Title: 'HR',
      HireDate: '01/02/2021',
      Country: 'USA',
      ReportsTo: 'Carson',
      EmployeeImage:
        avatar2,
    },
    {
      EmployeeID: 1,
      Name: 'Nancy Davolio',
      Title: 'Sales Representative',
      HireDate: '01/02/2021',
      Country: 'USA',
      ReportsTo: 'Carson',
      EmployeeImage:
      avatar2,
  
    },
];

const gridEmployeeProfile = (props) => (
    <div className="flex items-center gap-2">
      <img
        className="rounded-full w-10 h-10"
        src={props.EmployeeImage}
        alt="employee"
      />
      <p>{props.Name}</p>
    </div>
);
  
const gridEmployeeCountry = (props) => (
    <div className="flex items-center justify-center gap-2">
        <GrLocation />
        <span>{props.Country}</span>
    </div>
);

const employeesGrid = [
    { headerText: 'Employee',
      width: '150',
      template: gridEmployeeProfile,
      textAlign: 'Center' },
    { field: 'Name',
      headerText: '',
      width: '0',
      textAlign: 'Center',
    },
    { field: 'Title',
      headerText: 'Designation',
      width: '170',
      textAlign: 'Center',
    },
    { headerText: 'Country',
      width: '120',
      textAlign: 'Center',
      template: gridEmployeeCountry },
  
    { field: 'HireDate',
      headerText: 'Hire Date',
      width: '135',
      format: 'yMd',
      textAlign: 'Center' },
  
    { field: 'ReportsTo',
      headerText: 'Reports To',
      width: '120',
      textAlign: 'Center' },
    { field: 'EmployeeID',
      headerText: 'Employee ID',
      width: '125',
      textAlign: 'Center' },
  ];


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


    return (
        <>
            <div className="m-2 md:m-10 mt-24 p-2 md:p-10 bg-white rounded-3xl">
                <Header category="Page" title="Environments" />
                <GridComponent
                    dataSource={employeesData}
                    width="auto"
                    allowPaging
                    allowSorting
                    pageSettings={{ pageCount: 5 }}
                    editSettings={editing}
                    toolbar={toolbarOptions}
                >
                    <ColumnsDirective>
                    {/* eslint-disable-next-line react/jsx-props-no-spreading */}
                    {employeesGrid.map((item, index) => <ColumnDirective key={index} {...item} />)}
                    </ColumnsDirective>
                    <Inject services={[Search, Page]} />

                </GridComponent>
            </div>
        </>
    );
}

export default Environments;