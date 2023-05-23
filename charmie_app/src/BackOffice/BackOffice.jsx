import { Routes, Route } from 'react-router-dom';
import { Navbar, Footer, Sidebar} from './components';
import { Ecommerce, Environments, Robots, Tasks } from './pages';

import ImportCss from '../ImportCss';

import { useStateContext } from './contexts/ContextProvider';

const BackOffice = () => {
  const { currentMode, activeMenu } = useStateContext();

  ImportCss('Back');

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
            </Routes>
          </div>
          <Footer />
        </div>
      </div>
    </div>
  );
};

export default BackOffice;