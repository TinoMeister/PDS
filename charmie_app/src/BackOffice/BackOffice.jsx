import { Navigate, Routes, Route } from 'react-router-dom';
import { Navbar, Footer, Sidebar} from './components';
import { Ecommerce, Environments, Robots, Tasks, AddRobot, RobotInfo, EnvironmentInfo } from './pages';

import { useStateContext } from './contexts/ContextProvider';

const BackOfficePage = () => {
  const { currentMode, activeMenu } = useStateContext();

  return (
    <div className={ currentMode }>
      <div className="flex relative dark:bg-main-dark-bg">
        {activeMenu ? (
          <div className="w-72 fixed sidebar dark:bg-secondary-dark-bg bg-white ">
            <Sidebar />
          </div>
        ) : (
          <div className="w-0 dark:bg-secondary-dark-bg">
            <Sidebar />
          </div>
        )}
        <div
          className={
            activeMenu
              ? 'dark:bg-main-dark-bg  bg-main-bg min-h-screen md:ml-72 w-full  '
              : 'bg-main-bg dark:bg-main-dark-bg  w-full min-h-screen flex-2 '
          }
        >
          <div className="fixed md:static bg-main-bg dark:bg-main-dark-bg navbar w-full ">
            <Navbar />
          </div>
          <div>
            <Routes>
              {/* dashboard  */}
              <Route path="/" element={(<Ecommerce />)} />

              <Route path="/robots" element={<Robots />} />
              <Route path="/environments" element={<Environments />} />
              <Route path="/tasks" element={<Tasks />} />
              <Route path="/robot/addrobot" element={<AddRobot />} />
              <Route path="/robot/robotinfo" element={<RobotInfo />} />
              <Route path="/robot/environmentinfo" element={<EnvironmentInfo />} />
            </Routes>
          </div>
          <Footer />
        </div>
      </div>
    </div>
  );
};


const BackOffice = () => {
  let loggedIn = true;
  const userData = localStorage.getItem('user-info');
  if (userData === null) loggedIn = false;

  return (loggedIn ? <BackOfficePage /> : <Navigate to={'/'} />);
};

export default BackOffice;