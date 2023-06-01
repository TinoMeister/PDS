import React, { useEffect } from 'react';


const createLink = (loc) => {
    const link = document.createElement('link');
    link.href = loc;
    link.rel = 'stylesheet';
    document.head.appendChild(link);
}

const removeLink = (loc) => {
    const links = document.querySelectorAll('link[href="' + loc + '"]');
    links.forEach((link) => { link.parentNode.removeChild(link); });
}


const ImportCss = (location) => {
  useEffect(() => {
    if (location === 'Home') 
    {
        removeLink('./src/Auth/auth.css');
        removeLink('./src/Auth/css/bootstrap-grid.min.css');
        removeLink('./src/Auth/icons/boxicons-2/css/boxicons.min.css');
        removeLink('./src/BackOffice/App.css');
    } else if (location === 'Auth') 
    {
        removeLink('../src/BackOffice/App.css');

        createLink('../src/Auth/auth.css');
        createLink('../src/Auth/css/bootstrap-grid.min.css');
        createLink('../src/Auth/icons/boxicons-2/css/boxicons.min.css');
    } else if (location === 'Back')
    {
        removeLink('../src/Auth/auth.css');
        removeLink('../src/Auth/css/bootstrap-grid.min.css');
        removeLink('../src/Auth/icons/boxicons-2/css/boxicons.min.css');
      
        createLink('../../src/BackOffice/App.css');
    }
  }, []);
};

export default ImportCss;