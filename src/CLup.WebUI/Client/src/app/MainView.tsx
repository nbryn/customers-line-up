import React, {useState} from 'react';
import {Container} from 'react-bootstrap';
import CssBaseline from '@material-ui/core/CssBaseline';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory} from 'react-router-dom';

import {clearApiState} from '../shared/api/apiSlice';
import {ExtendedToastMessage, ToastMessage} from '../shared/components/Toast';
import {Header} from '../shared/components/navigation/Header';
import {LoginView} from '../features/user/LoginView';
import {MainMenu} from '../shared/components/navigation/MainMenu';
import {Routes} from './Routes';
import {selectApiState} from '../shared/api/apiSlice';
import {selectCurrentUser} from '../features/user/userSlice';
import {useAppSelector, useAppDispatch} from '../app/Store';

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
