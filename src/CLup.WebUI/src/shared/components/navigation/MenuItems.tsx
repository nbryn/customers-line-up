import DashboardIcon from '@mui/icons-material/Dashboard';
import React from 'react';
import {
    BUSINESS_BOOKINGS_ROUTE,
    BUSINESS_ROUTE,
    BUSINESS_TIMESLOTS_ROUTE,
    CREATE_BUSINESS_ROUTE,
    HOME_ROUTE,
    USER_BOOKINGS_ROUTE,
    USER_BUSINESS_ROUTE,
    USER_MESSAGES_ROUTE,
    USER_PROFILE_ROUTE,
} from '../../../app/RouteConstants';

export type MenuItem = {
    label: string;
    icon: JSX.Element;
    path: string;
};

export const menuItems: MenuItem[] = [
    {
        label: 'Home',
        icon: <DashboardIcon />,
        path: HOME_ROUTE,
    },
    {
        label: 'Find Business',
        icon: <DashboardIcon />,
        path: USER_BUSINESS_ROUTE,
    },
    {
        label: 'My Bookings',
        icon: <DashboardIcon />,
        path: USER_BOOKINGS_ROUTE,
    },
    {
        label: 'Messages',
        icon: <DashboardIcon />,
        path: USER_MESSAGES_ROUTE,
    },
    {
        label: 'Profile',
        icon: <DashboardIcon />,
        path: USER_PROFILE_ROUTE,
    },
];

export const businessItems: MenuItem[] = [
    {
        label: 'Businesses',
        icon: <DashboardIcon />,
        path: BUSINESS_ROUTE,
    },
    {
        label: 'Create Business',
        icon: <DashboardIcon />,
        path: CREATE_BUSINESS_ROUTE,
    },
];

export const employeeItems: MenuItem[] = [
    {
        label: 'Bookings',
        icon: <DashboardIcon />,
        path: BUSINESS_BOOKINGS_ROUTE,
    },
    {
        label: 'Time Slots',
        icon: <DashboardIcon />,
        path: BUSINESS_TIMESLOTS_ROUTE,
    },
];
