import { useState } from 'react'
import { useNavigate } from 'react-router-dom';

import { close, logo, menu } from '../assets';
import { navLinks } from '../constants';

const Navbar = () => {
  const navigate = useNavigate();
  const [toggle, setToggle] = useState(false);

  const login = async (e) => {
    e.preventDefault();
    navigate('/auth/signin');
  }

  return (
    <nav className="w-full flex py-6 justify-between itens-center navbar">
      <img src={logo} alt="Robot Inc." className="w-[124px] h-[32px]"/>
      <ul className="list-none sm:flex hidden justify-end itens-center flex-1">
      {navLinks.map((nav, index) => (
        <li 
        key={nav.id}
        className={`font-poppings font-normal cursor-pointer text[16px] mr-10 text-white`} 
        >
          <a href={` #${nav.id}`}>
            {nav.title}
          </a>
      </li>
      ))}
      <li 
        key='login'
        className={`font-poppings font-normal cursor-pointer text[16px] mr-0 text-white`} 
        >
          <a onClick={login}>
            Login
          </a>
      </li>
      </ul>

      <div className="sm:hidden flex flex-1 justify-end itens-center">
        <img src={toggle ? close : menu} 
        alt = "menu"
        className="w-[28px] h-[28px] object-contain"
        onClick={() => setToggle((prev) => !prev)}
        />
        <div className={`${toggle ? 'flex' : 'hidden'} p-6 bg-black-gradient absolute top-20 right-0 mx-4 my-2 min-w-[140px] rounded-x1 sidebar`}>
          <ul className="list-none flex flex-col justify-end itens-center flex-1">
            {navLinks.map((nav, index) => (
            <li 
            key={nav.id}
            className={`font-poppings font-normal cursor-pointer text[16px] ${index === navLinks.length - 1 ? 'mr-0' : 'mb-4'} text-white`}>
              <a href={` #${nav.id}`}>
                {nav.title}
              </a>
            </li>
          ))}
          </ul>
        </div>
      </div>


    </nav>
  )
}

export default Navbar