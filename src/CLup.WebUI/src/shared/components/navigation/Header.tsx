import React from 'react';
import Cookies from 'js-cookie';
import {styled} from '@mui/material/styles';
import MuiAppBar, {type AppBarProps as MuiAppBarProps} from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import Badge from '@mui/material/Badge';
import MenuIcon from '@mui/icons-material/Menu';
import NotificationsIcon from '@mui/icons-material/Notifications';
import PeopleIcon from '@mui/icons-material/PeopleOutline';

import {baseApi, useAppDispatch} from '../../../app/Store';
import {DRAWER_WIDTH} from './AppFrame';

interface Props extends MuiAppBarProps {
    open: boolean;
    setOpen: (open: boolean) => void;
}

const AppBar = styled(MuiAppBar, {
    shouldForwardProp: (prop) => prop !== 'open',
})<Props>(({theme, open}) => ({
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(['width', 'margin'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    ...(open && {
        marginLeft: DRAWER_WIDTH,
        width: `calc(100% - ${DRAWER_WIDTH}px)`,
        transition: theme.transitions.create(['width', 'margin'], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.enteringScreen,
        }),
    }),
}));

export const AppHeader: React.FC<Props> = ({open, setOpen}: Props) => {
    const dispatch = useAppDispatch();

    return (
        <AppBar position="absolute" open={open} setOpen={setOpen}>
            <Toolbar
                sx={{
                    pr: '24px',
                }}
            >
                <IconButton
                    edge="start"
                    color="inherit"
                    aria-label="open drawer"
                    onClick={() => setOpen(!open)}
                    sx={{
                        marginRight: '36px',
                        ...(open && {display: 'none'}),
                    }}
                >
                    <MenuIcon />
                </IconButton>
                <Typography component="h1" variant="h6" color="inherit" noWrap sx={{flexGrow: 1}}>
                    {'Customers Lineup'}
                </Typography>
                <IconButton color="inherit">
                    <Badge badgeContent={4} color="secondary">
                        <NotificationsIcon />
                    </Badge>
                </IconButton>
                <IconButton
                    onClick={() => {
                        window.location.href = window.location.href.substring(
                            0,
                            window.location.href.indexOf('/') + 1
                        );
                        Cookies.remove('access_token');
                        dispatch(baseApi.util.resetApiState());
                    }}
                >
                    <Badge color="secondary">
                        <PeopleIcon />
                    </Badge>
                </IconButton>
            </Toolbar>
        </AppBar>
    );
};
