import React, {useState} from 'react';
import {useNavigate} from 'react-router-dom';
import type {Theme} from '@mui/material/styles';
import Box from '@mui/material/Box';

import {AppHeader} from '../shared/components/navigation/Header';
import {MainMenu} from '../shared/components/navigation/MainMenu';
import {AuthRoutes, PublicRoutes} from './Routes';
import {ExtendedToastMessage, ToastMessage} from '../shared/components/Toast';
import {useGetUserQuery} from '../features/user/UserApi';
import {useAppDispatch, useAppSelector} from './Store';
import {clearApiState, selectApiState} from '../shared/api/ApiState';
import {Typography} from '@mui/material';

declare module '@mui/styles/defaultTheme' {
    interface DefaultTheme extends Theme {}
}

export const AppShell: React.FC = () => {
    const [open, setOpen] = useState(true);
    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const apiState = useAppSelector(selectApiState);
    const {data: user} = useGetUserQuery();
    return (
        <>
            {!user ? (
                <>
                    <PublicRoutes />
                    {apiState.message && (
                        <ToastMessage
                            onClose={() => dispatch(clearApiState())}
                            message={apiState.message}
                            severity="error"
                        />
                    )}
                </>
            ) : (
                <>
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
                                navigate(apiState.toastInfo?.navigateTo ?? '/home')
                            }
                        />
                    )}

                    <Box sx={{display: 'flex'}}>
                        <AppHeader open={open} setOpen={setOpen} />
                        <MainMenu open={open} setOpen={setOpen} />
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
                            <AuthRoutes />
                            <Typography
                                marginTop={30}
                                align="center"
                                color="text.secondary"
                                variant="body2"
                            >
                                {'Copyright © Customers Lineup '}
                                {new Date().getFullYear()}
                                {'.'}
                            </Typography>
                        </Box>
                    </Box>
                </>
            )}
        </>
    );
};
