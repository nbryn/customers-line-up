import React from 'react';
import {useNavigate} from 'react-router-dom';
import {styled} from '@mui/material/styles';
import MuiDrawer from '@mui/material/Drawer';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import {ListItemButton, ListItemIcon, ListItemText} from '@mui/material';
import DashboardIcon from '@mui/icons-material/Dashboard';

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

export const DRAWER_WIDTH: number = 240;

type MenuItem = {
    label: string;
    icon: JSX.Element;
    path: string;
};

const Drawer = styled(MuiDrawer, {shouldForwardProp: (prop) => prop !== 'open'})(
    ({theme, open}) => ({
        '& .MuiDrawer-paper': {
            position: 'relative',
            whiteSpace: 'nowrap',
            width: DRAWER_WIDTH,
            transition: theme.transitions.create('width', {
                easing: theme.transitions.easing.sharp,
                duration: theme.transitions.duration.enteringScreen,
            }),
            boxSizing: 'border-box',
            ...(!open && {
                overflowX: 'hidden',
                transition: theme.transitions.create('width', {
                    easing: theme.transitions.easing.sharp,
                    duration: theme.transitions.duration.leavingScreen,
                }),
                width: theme.spacing(7),
                [theme.breakpoints.up('sm')]: {
                    width: theme.spacing(9),
                },
            }),
        },
    })
);

const menuItems: MenuItem[] = [
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

const businessItems: MenuItem[] = [
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

type Props = {
    open: boolean;
    setOpen: (open: boolean) => void;
};

export const MainMenu: React.FC<Props> = ({open, setOpen}: Props) => {
    const navigate = useNavigate();
    return (
        <Drawer variant="permanent" open={open}>
            <Toolbar
                sx={{
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'flex-end',
                    px: [1],
                }}
            >
                <IconButton onClick={() => setOpen(!open)}>
                    <ChevronLeftIcon />
                </IconButton>
            </Toolbar>
            <Divider />
            <List component="nav">
                {menuItems.map((menuItem) => (
                    <ListItemButton key={menuItem.label} onClick={() => navigate(menuItem.path)}>
                        <ListItemIcon>{menuItem.icon}</ListItemIcon>
                        <ListItemText primary={menuItem.label} />
                    </ListItemButton>
                ))}
                <Divider sx={{my: 1}} />
                {businessItems.map((menuItem) => (
                    <ListItemButton key={menuItem.label} onClick={() => navigate(menuItem.path)}>
                        <ListItemIcon>{menuItem.icon}</ListItemIcon>
                        <ListItemText primary={menuItem.label} />
                    </ListItemButton>
                ))}
            </List>
        </Drawer>
    );
};
