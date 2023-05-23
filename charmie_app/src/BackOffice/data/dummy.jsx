import React from 'react';
import { AiOutlineRobot, AiOutlineHome, AiOutlineMenuUnfold, AiOutlineSetting} from 'react-icons/ai';
import { FiShoppingBag } from 'react-icons/fi';
import { BsCurrencyDollar } from 'react-icons/bs';

import avatar2 from './avatar2.jpg';

export const chatData = [
  {
    image: avatar2,
    message: 'Roman Joined the Team!',
    desc: 'Congratulate him',
    time: '9:08 AM',
  },
];

export const userProfileData = [
  {
    icon: <BsCurrencyDollar />,
    title: 'My Profile',
    desc: 'Account Settings',
    iconColor: '#03C9D7',
    iconBg: '#E5FAFB',
  }
];

export const links = [
  {
    title: 'Dashboard',
    links: [
      {
        name: 'backOffice',
        icon: <FiShoppingBag />,
      },
    ],
  },

  {
    title: 'Pages',
    links: [
      {
        name: 'robots',
        icon: <AiOutlineRobot />,
      },
      {
        name: 'environments',
        icon: <AiOutlineHome />,
      },
      {
        name: 'tasks',
        icon: <AiOutlineMenuUnfold />,
      },
    ],
  },
  {
    title: 'Settings',
    links: [
      {
        name: 'settings',
        icon: <AiOutlineSetting />,
      },
    ],
  },
];