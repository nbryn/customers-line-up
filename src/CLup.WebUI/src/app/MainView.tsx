import React, {useState} from 'react';
import {Container} from 'react-bootstrap';
import CssBaseline from '@mui/material/CssBaseline';
import makeStyles from '@mui/styles/makeStyles';
import type {Theme} from '@mui/material/styles';
import {useHistory} from 'react-router-dom';

import {clearApiState, selectApiState} from '../shared/api/ApiState';
import {ExtendedToastMessage, ToastMessage} from '../shared/components/Toast';
import {Header} from '../shared/components/navigation/Header';
import {LoginView} from '../features/user/LoginView';
import {MainMenu} from '../shared/components/navigation/MainMenu';
import {Routes} from './Routes';
import {selectCurrentUser} from '../features/user/UserState';
import {useAppSelector, useAppDispatch} from '../app/Store';

declare module '@mui/styles/defaultTheme' {
    interface DefaultTheme extends Theme {}
}

const useStyles = makeStyles((theme) => ({
    root: {},
    content: {
        marginTop: theme.spacing(10),
        marginBottom: theme.spacing(4),
    },
}));

export const MainView: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    const [mobileOpen, setMobileOpen] = useState(false);

    const apiState = useAppSelector(selectApiState);
    const user = useAppSelector(selectCurrentUser);
    const dispatch = useAppDispatch();

    const handleMenuToggle = () => {
        setMobileOpen(!mobileOpen);
    };

    const handleMenuClose = () => {
        setMobileOpen(false);
    };

    return (
        <div className={styles.root}>
            <CssBaseline />
            {!user ? (
                <LoginView />
            ) : (
                <>
                    <Header onMenuToggle={handleMenuToggle} />
                    <MainMenu mobileOpen={mobileOpen} onClose={handleMenuClose} />

                    <Container className={styles.root}>
                        {apiState.message && !apiState.toastInfo && (
                            <ToastMessage
                                onClose={() => dispatch(clearApiState())}
                                message={apiState.message}
                                severity={apiState.error ? 'error' : 'success'}
                            />
                        )}
                        {apiState.toastInfo && (
                            <ExtendedToastMessage
                                onClose={() => dispatch(clearApiState())}
                                message={apiState.message}
                                primaryButtonText={apiState.toastInfo.buttonText}
                                primaryAction={() =>
                                    history.push(apiState.toastInfo?.navigateTo ?? '/home')
                                }
                            />
                        )}
                        <Routes />
                    </Container>
                </>
            )}
        </div>
    );
};
