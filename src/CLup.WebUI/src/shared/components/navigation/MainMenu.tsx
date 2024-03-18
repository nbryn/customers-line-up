import DashboardIcon from '@mui/icons-material/Dashboard';
import Divider from '@mui/material/Divider';
import Drawer from '@mui/material/Drawer';
import Hidden from '@mui/material/Hidden';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import makeStyles from '@mui/styles/makeStyles';
import PeopleIcon from '@mui/icons-material/People';
import React from 'react';
import {useHistory} from 'react-router-dom';

import {selectCurrentUser} from '../../../features/user/UserState';
import {useAppSelector} from '../../../app/Store';
import {useUserContext} from '../../../features/user/UserContext';

const drawerWidth = 210;

const useStyles = makeStyles((theme) => ({
    drawer: {
        [theme.breakpoints.up('md')]: {
            width: drawerWidth,
            flexShrink: 0,
        },
    },
    drawerPaper: {
        width: drawerWidth,
    },
    logo: {
        [theme.breakpoints.up('md')]: {
            backgroundImage: '',
            marginTop: 10,
            marginBottom: 50,
            marginLeft: 40,
            width: '80px',
            height: '75px',
        },
    },
    listItem: {
        marginTop: 15,
        marginBottom: 15,
    },
    listItemIcon: {
        marginRight: -10,
        marginLeft: 5,
    },
}));

type MenuItem = {
    label: string;
    icon: JSX.Element;
    path: string;
};

type Props = {
    mobileOpen: boolean;
    onClose: () => void;
};

export const MainMenu: React.FC<Props> = (props: Props) => {
    const history = useHistory();
    const styles = useStyles();
    const user = useAppSelector(selectCurrentUser);
    const {logout} = useUserContext();

    const menuItems: MenuItem[] = [
        {
            label: 'Home',
            icon: <DashboardIcon />,
            path: '/',
        },
        {
            label: 'Find Business',
            icon: <DashboardIcon />,
            path: '/user/business',
        },
        {
            label: 'My Bookings',
            icon: <DashboardIcon />,
            path: '/user/bookings',
        },
        {
            label: 'Messages',
            icon: <DashboardIcon />,
            path: '/user/messages',
        },
        {
            label: 'Profile',
            icon: <DashboardIcon />,
            path: '/user/profile',
        },
    ];

    const businessItems: MenuItem[] = [
        {
            label: 'Businesses',
            icon: <DashboardIcon />,
            path: '/business',
        },
        {
            label: 'Create Business',
            icon: <DashboardIcon />,
            path: '/business/new',
        },
    ];

    const employeeItems: MenuItem[] = [
        {
            label: 'Bookings',
            icon: <DashboardIcon />,
            path: '/business/bookings',
        },
        {
            label: 'Time Slots',
            icon: <DashboardIcon />,
            path: '/business/timeslots',
        },
    ];

    const {mobileOpen} = props;

    const handleMenuItemClick = (menuItem: MenuItem) => {
        history.push(menuItem.path);
        if (mobileOpen) {
            props.onClose();
        }
    };

    const drawer = (
        <>
            <div className={styles.logo}>
                <img src="" />
            </div>
            <Divider />
            <List>
                {menuItems.map((menuItem) => (
                    <ListItem
                        className={styles.listItem}
                        button
                        key={menuItem.label}
                        onClick={() => handleMenuItemClick(menuItem)}
                    >
                        <ListItemIcon className={styles.listItemIcon}>{menuItem.icon}</ListItemIcon>
                        <ListItemText primary={menuItem.label} />
                    </ListItem>
                ))}
            </List>
            <Divider />

            <List>
                {businessItems.map((menuItem) => (
                    <ListItem
                        className={styles.listItem}
                        button
                        key={menuItem.label}
                        onClick={() => handleMenuItemClick(menuItem)}
                    >
                        <ListItemIcon className={styles.listItemIcon}>{menuItem.icon}</ListItemIcon>
                        <ListItemText primary={menuItem.label} />
                    </ListItem>
                ))}
                {user?.role === 'Employee' &&
                    employeeItems.map((menuItem) => (
                        <ListItem
                            className={styles.listItem}
                            button
                            key={menuItem.label}
                            onClick={() => handleMenuItemClick(menuItem)}
                        >
                            <ListItemIcon className={styles.listItemIcon}>
                                {menuItem.icon}
                            </ListItemIcon>
                            <ListItemText primary={menuItem.label} />
                        </ListItem>
                    ))}
            </List>
            <Divider />
        </>
    );

    return (
        <nav className={styles.drawer} aria-label="navigation">
            <Hidden mdDown implementation="css">
                <Drawer
                    classes={{
                        paper: styles.drawerPaper,
                    }}
                    variant="permanent"
                    open
                >
                    {drawer}
                    <List>
                        <ListItem
                            className={styles.listItem}
                            button
                            onClick={() => {
                                window.location.href = window.location.href.substring(
                                    0,
                                    window.location.href.indexOf('/') + 1
                                );
                                logout();
                            }}
                        >
                            <ListItemIcon className={styles.listItemIcon}>
                                <PeopleIcon />
                            </ListItemIcon>
                            <ListItemText primary="Logout" />
                        </ListItem>
                    </List>
                </Drawer>
            </Hidden>
        </nav>
    );
};
