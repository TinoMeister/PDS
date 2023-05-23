import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import { registerLicense } from '@syncfusion/ej2-base';

import './index.css';
import App from './App.jsx';


registerLicense('Mgo+DSMBaFt+QHJqVk1mQ1BMaV1CX2BZdllzRGlYf04BCV5EYF5SRHNeS11rTHtWf0JhWHg=;Mgo+DSMBPh8sVXJ1S0R+X1pCaV5KQmFJfFBmTWlcflRxdEU3HVdTRHRcQlhiQH5bc0BmUHhdeHM=;ORg4AjUWIQA/Gnt2VFhiQlJPcEBDVXxLflF1VWtTel96dVVWESFaRnZdQV1mSHdTfkBmXHZZdXZS;MjExNjM0MUAzMjMxMmUzMjJlMzNCVmtUTTNveXFpRkF0azQ0R1IycW1qSm9GSmJyUHZpaHdnckdVWXQvV1VNPQ==;MjExNjM0MkAzMjMxMmUzMjJlMzNtUmNHbVpzdmYwU1ZNOFh6cU9IdWxMeldkMEwyM0V6ZURTeVZjTWI5TXg4PQ==;NRAiBiAaIQQuGjN/V0d+Xk9HfVldXGtWfFN0RnNRdVtxflRGcC0sT3RfQF5jTH9adk1mXHpXeHVTQQ==;MjExNjM0NEAzMjMxMmUzMjJlMzNhL3hSdHBsZUJMMktIbFdqV2hpK3RUNktXUzFPakt3eG90a21rbGgzWWdVPQ==;MjExNjM0NUAzMjMxMmUzMjJlMzNVemVrTG9FWXFVaVNhZ2VlQ0N1WDVFblkwalhlRXUzUEdYazJtdy9uL3EwPQ==;Mgo+DSMBMAY9C3t2VFhiQlJPcEBDVXxLflF1VWtTel96dVVWESFaRnZdQV1mSHdTfkBmXHZWd3JS;MjExNjM0N0AzMjMxMmUzMjJlMzNlcDhuK0M0dndSTkRrTHhMb3hQK2ZUaFBUelRpZ09rdWJqdDlPaWRZME1jPQ==;MjExNjM0OEAzMjMxMmUzMjJlMzNlTkZCTVcrblpvVXFsS21ienk3Q2p3K2Z1SWpFUWJGL01Ma1F1SUd6YUdBPQ==;MjExNjM0OUAzMjMxMmUzMjJlMzNhL3hSdHBsZUJMMktIbFdqV2hpK3RUNktXUzFPakt3eG90a21rbGgzWWdVPQ==');

const root = createRoot(document.getElementById('root'));

root.render(
  <StrictMode>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </StrictMode>
);
