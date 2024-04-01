import React, {useState} from 'react';
import {useHistory} from 'react-router-dom';
import {Container} from 'react-bootstrap';
import CssBaseline from '@mui/material/CssBaseline';
import makeStyles from '@mui/styles/makeStyles';
import type {Theme} from '@mui/material/styles';

import {ExtendedToastMessage, ToastMessage} from '../shared/components/Toast';
import {Header} from '../shared/components/navigation/Header';
import {MainMenu} from '../shared/components/navigation/MainMenu';
import {PublicRoutes, Routes} from './Routes';
import {useGetUserQuery} from '../features/user/UserApi';
import {useAppDispatch, useAppSelector} from './Store';
import {clearApiState, selectApiState} from '../shared/api/ApiState';

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
    const [mobileOpen, setMobileOpen] = useState(false);

    const dispatch = useAppDispatch();
    const history = useHistory();
    const styles = useStyles();

    const apiState = useAppSelector(selectApiState);
    const {data: user} = useGetUserQuery();
    return (
        <div className={styles.root}>
            <CssBaseline />
            {!user ? (
                <PublicRoutes />
            ) : (
                <>
                    <Header onMenuToggle={() => setMobileOpen(!mobileOpen)} />
                    <MainMenu mobileOpen={mobileOpen} onClose={() => setMobileOpen(false)} />

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
