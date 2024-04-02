import React from 'react';
import {useHistory} from 'react-router-dom';
import type {Theme} from '@mui/material/styles';

import {ExtendedToastMessage, ToastMessage} from '../shared/components/Toast';
import {AppFrame} from '../shared/components/navigation/AppFrame';
import {PublicRoutes} from './Routes';
import {useGetUserQuery} from '../features/user/UserApi';
import {useAppDispatch, useAppSelector} from './Store';
import {clearApiState, selectApiState} from '../shared/api/ApiState';

declare module '@mui/styles/defaultTheme' {
    interface DefaultTheme extends Theme {}
}

export const MainView: React.FC = () => {
    const dispatch = useAppDispatch();
    const history = useHistory();

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
                    <AppFrame />

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
                </>
            )}
        </>
    );
};
