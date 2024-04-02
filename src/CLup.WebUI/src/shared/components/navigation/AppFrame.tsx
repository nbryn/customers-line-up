import React, {useState} from 'react';
import {useHistory} from 'react-router-dom';
import {styled} from '@mui/material/styles';
import MuiDrawer from '@mui/material/Drawer';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import {ListItemButton, ListItemIcon, ListItemText} from '@mui/material';
// import makeStyles from '@mui/styles/makeStyles';

import {businessItems, menuItems} from './MenuItems';
import {AppHeader} from './Header';
import {Routes} from '../../../app/Routes';

export const DRAWER_WIDTH: number = 240;

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

export const AppFrame: React.FC = () => {
    const [open, setOpen] = useState(true);
    const history = useHistory();
    
    return (
        <Box sx={{display: 'flex'}}>
            <AppHeader open={open} setOpen={setOpen} />
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
                        <ListItemButton
                            key={menuItem.label}
                            onClick={() => history.push(menuItem.path)}
                        >
                            <ListItemIcon>{menuItem.icon}</ListItemIcon>
                            <ListItemText primary={menuItem.label} />
                        </ListItemButton>
                    ))}
                    <Divider sx={{my: 1}} />
                    {businessItems.map((menuItem) => (
                        <ListItemButton
                            key={menuItem.label}
                            onClick={() => history.push(menuItem.path)}
                        >
                            <ListItemIcon>{menuItem.icon}</ListItemIcon>
                            <ListItemText primary={menuItem.label} />
                        </ListItemButton>
                    ))}
                </List>
            </Drawer>
            <Box
                component="main"
                sx={{
                    backgroundColor: (theme) =>
                        theme.palette.mode === 'light'
                            ? theme.palette.grey[100]
                            : theme.palette.grey[900],
                    flexGrow: 1,
                    height: '100vh',
                    overflow: 'auto',
                }}
            >
                <Routes />
            </Box>
        </Box>
    );
};
