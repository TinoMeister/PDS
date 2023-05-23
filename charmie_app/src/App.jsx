import { Routes, Route } from 'react-router-dom';

import Home from './Home';
import Signin from "./Auth/Signin";
import Signup from "./Auth/Signup";
import BackOffice from './BackOffice/BackOffice';

import { ContextProvider } from './BackOffice/contexts/ContextProvider';

export default function App() {
    return (
        <div className="App">
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/auth/signin" element={<Signin />} />
                <Route path="/auth/signup" element={<Signup />} />
                <Route path="/backOffice/*" element={<ContextProvider> <BackOffice /> </ContextProvider>} />
            </Routes>
        </div>
    );
}