import React, {useState} from 'react';
import AccountBoxIcon from '@material-ui/icons/AccountBox';
import AppBar from '@material-ui/core/AppBar';
import classNames from 'classnames';
import {Grid, Paper, Popover} from '@material-ui/core';
import IconButton from '@material-ui/core/IconButton';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import {makeStyles} from '@material-ui/core/styles';
import MenuIcon from '@material-ui/icons/Menu';
import PeopleIcon from '@material-ui/icons/People';
import Toolbar from '@material-ui/core/Toolbar';
import useWindowScroll from '@react-hook/window-scroll';

import {useUserContext} from '../../../features/user/UserContext';

type Props = {
    onMenuToggle: () => void;
};

const useStyles = makeStyles((theme) => ({
    menuButton: {
        marginRight: theme.spacing(2),
        display: 'inline-block',
        [theme.breakpoints.up('md')]: {
            display: 'none',
        },
    },
    menuButtonUltraDense: {
        paddingTop: '6px',
        paddingBottom: '6px',
    },
    logo: {
        [theme.breakpoints.down('sm')]: {
            backgroundImage: 'url(assets/logo_white.svg)',
            backgroundSize: 'contain',
            backgroundRepeat: 'no-repeat',
            width: '200px',
            height: '50px',
            position: 'absolute',
            display: 'inline-block',
        },
    },
    listItem: {
        marginLeft: -25,
    },
    listItemIcon: {
        marginRight: -10,
        marginLeft: 5,
    },
    logoUltraDense: {
        [theme.breakpoints.down('sm')]: {
            height: '39px',
        },
    },
    menuSpace: {
        [theme.breakpoints.up('md')]: {
            marginLeft: '240px',
        },
        width: '100%',
    },
    toolBarUltraDense: {
        minHeight: '30px',
    },
    smooth: {
        transitionProperty: 'min-height height padding',
        transitionDuration: '0.2s',
    },
    end: {
        marginLeft: 'auto',
    },
    paper: {
        width: '150px',
        height: '100px',
        padding: theme.spacing(2),
    },
    language: {
        width: '100%',
    },
}));

export const Header: React.FC<Props> = (props: Props) => {
    const styles = useStyles();
    const scrollY = useWindowScroll(60 /*fps*/);
    const isUltraDense = scrollY > 10;
    const {logout} = useUserContext();
    const [userAccountEl, setUserAccountEl] = useState<HTMLElement | null>(null);

    const handleUserAccountToggle = (event: React.MouseEvent<HTMLElement, MouseEvent>) => {
        setUserAccountEl(userAccountEl === null ? event.currentTarget : null);
    };

    const showUserAccount = Boolean(userAccountEl);

    const menuButtonClasses = classNames({
        [styles.smooth]: true,
        [styles.menuButton]: true,
        [styles.menuButtonUltraDense]: isUltraDense,
    });
    const logoClasses = classNames({
        [styles.smooth]: true,
        [styles.logo]: true,
        [styles.logoUltraDense]: isUltraDense,
    });
    const toolBarClasses = classNames({
        [styles.smooth]: true,
        [styles.toolBarUltraDense]: isUltraDense,
    });

    return (
        <AppBar>
            <Toolbar className={toolBarClasses}>
                <div className={styles.menuSpace}>
                    <Grid container alignContent="flex-start">
                        <Grid item>
                            <IconButton
                                color="inherit"
                                aria-label="open main menu"
                                edge="start"
                                onClick={props.onMenuToggle}
                                className={menuButtonClasses}
                            >
                                <MenuIcon />
                            </IconButton>
                        </Grid>
                        <Grid item>
                            <div className={logoClasses} />
                        </Grid>
                        <Grid item className={styles.end}>
                            <IconButton
                                color="inherit"
                                aria-label="account"
                                onClick={handleUserAccountToggle}
                            >
                                <AccountBoxIcon />
                            </IconButton>
                            <Popover
                                open={showUserAccount}
                                anchorEl={userAccountEl}
                                anchorOrigin={{
                                    vertical: 'bottom',
                                    horizontal: 'left',
                                }}
                                transformOrigin={{
                                    vertical: 'top',
                                    horizontal: 'left',
                                }}
                                onClose={handleUserAccountToggle}
                                disableRestoreFocus
                            >
                                <Paper className={styles.paper}>
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
                                </Paper>
                            </Popover>
                        </Grid>
                    </Grid>
                </div>
            </Toolbar>
        </AppBar>
    );
};
