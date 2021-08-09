import React, {useState} from 'react';
import {Container} from 'react-bootstrap';
import CssBaseline from '@material-ui/core/CssBaseline';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory} from 'react-router-dom';

import {clearBookingApiInfo} from '../features/booking/bookingSlice';
import {clearBusinessApiInfo} from '../features/business/businessSlice';
import {clearEmployeeApiInfo} from '../features/employee/employeeSlice';
import {clearInsightsApiInfo} from '../features/insights/insightsSlice';
import {clearTimeSlotsApiInfo} from '../features/timeslot/timeSlotSlice';
import {clearUserApiInfo} from '../features/user/userSlice';
import {ExtendedToastMessage, ToastMessage} from '../common/components/Toast';
import {Header} from '../common/components/navigation/Header';
import {LoginView} from '../features/user/LoginView';
import {MainMenu} from '../common/components/navigation/MainMenu';
import {Routes} from './Routes';
import {selectCurrentUser} from '../features/user/userSlice';
import {selectGeneralApiInfo, useAppSelector, useAppDispatch} from '../app/Store';
import {State} from '../app/AppTypes';

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

    const apiInfo = useAppSelector(selectGeneralApiInfo);
    const user = useAppSelector(selectCurrentUser);
    const dispatch = useAppDispatch();

    const handleMenuToggle = () => {
        setMobileOpen(!mobileOpen);
    };

    const handleMenuClose = () => {
        setMobileOpen(false);
    };

    const dispatchHelper = (state: State) => {
        switch (state) {
            case State.Bookings:
                return dispatch(clearBookingApiInfo());

            case State.Businesses:
                return dispatch(clearBusinessApiInfo());

            case State.Employees:
                return dispatch(clearEmployeeApiInfo());

            case State.Insights:
                return dispatch(clearInsightsApiInfo());

            case State.TimeSlots:
                return dispatch(clearTimeSlotsApiInfo());

            case State.Users:
                return dispatch(clearUserApiInfo());
        }
    };

    const navigate = (state: State) => {
        dispatchHelper(state);
        switch (state) {
            case State.Bookings:
                return history.push('/user/bookings');

            case State.Businesses:
                return dispatch(clearBusinessApiInfo());

            case State.Employees:
                return dispatch(clearEmployeeApiInfo());

            case State.Insights:
                return dispatch(clearInsightsApiInfo());

            case State.TimeSlots:
                return dispatch(clearTimeSlotsApiInfo());

            case State.Users:
                return dispatch(clearUserApiInfo());
        }
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
                        {apiInfo && !apiInfo?.buttonText && (
                            <ToastMessage
                                onClose={() => dispatchHelper(apiInfo.state)}
                                message={apiInfo.message}
                                severity={apiInfo.isError ? 'error' : 'success'}
                            />
                        )}
                        {apiInfo?.buttonText && (
                            <ExtendedToastMessage
                                onClose={() => dispatchHelper(apiInfo.state)}
                                message={apiInfo.message}
                                primaryButtonText={apiInfo.buttonText}
                                primaryAction={() => navigate(apiInfo.state)}
                            />
                        )}
                        <Routes />
                    </Container>
                </>
            )}
        </div>
    );
};
