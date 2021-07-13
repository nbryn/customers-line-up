import React, {useState} from 'react';
import {Container} from 'react-bootstrap';
import CssBaseline from '@material-ui/core/CssBaseline';
import {makeStyles} from '@material-ui/core/styles';

import {LoginView} from '../features/user/LoginView';
import {MainMenu} from '../common/components/navigation/MainMenu';
import {Routes} from './Routes';
import {selectCurrentUser} from '../features/user/userSlice';
import {useAppSelector} from '../app/Store';

const useStyles = makeStyles((theme) => ({
    root: {},
    content: {
        marginTop: theme.spacing(10),
        marginBottom: theme.spacing(4),
    },
}));

export const MainView: React.FC = () => {
    const styles = useStyles();
    const [mobileOpen, setMobileOpen] = useState(false);
    const user = useAppSelector(selectCurrentUser);

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
                    <MainMenu mobileOpen={mobileOpen} onClose={handleMenuClose} />
                    <Container className={styles.root}>
                        <Routes />
                    </Container>
                </>
            )}
        </div>
    );
};
