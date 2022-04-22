import React from 'react';
import {Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory} from 'react-router-dom';

import {CardInfo} from '../../shared/components/card/CardInfo';
import {Header} from '../../shared/components/Texts';
import {InfoCard} from '../../shared/components/card/InfoCard';
import {selectBookingsByOwner, selectBookingsByUser, selectNextBookingByUser} from '../booking/BookingState';
import {selectCurrentUser} from '../user/UserState';
import {selectEmployeesByOwner} from '../business/employee/EmployeeState';
import {useAppDispatch, useAppSelector} from '../../app/Store';

const useStyles = makeStyles((theme) => ({
    card: {
        marginTop: 25,
    },
    center: {
        justifyContent: 'center',
    },
}));

export const HomeView: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    const dispatch = useAppDispatch();

    const user = useAppSelector(selectCurrentUser);
    const userBookings = useAppSelector(selectBookingsByUser)
    const userEmployees = useAppSelector(selectEmployeesByOwner);
    const userBusinessBookings = useAppSelector(selectBookingsByOwner);
    const userNextBooking = useAppSelector(selectNextBookingByUser)

    return (
        <Container>
            <Row className={styles.center}>
                <Header text={`Welcome ${user?.name}`} />
            </Row>
            <Row className={styles.center}>
                <Col className={styles.card} sm={6} md={8} lg={5}>
                    <InfoCard
                        buttonAction={() => history.push('/user/bookings')}
                        buttonText="My Bookings"
                        secondaryAction={() => history.push('/user/business')}
                        secondaryButtonText="New Booking"
                        title="Booking Info"
                        subtitle="Summary"
                    >
                        <CardInfo
                            infoTexts={[
                                {text: `Bookings: ${userBookings.length}`, icon: 'Home'},
                                {text: `Next: ${userNextBooking.dateString}`, icon: 'Hot'},
                                {
                                    text: `Where: ${userNextBooking.booking.business}`,
                                    icon: 'Grain',
                                },
                            ]}
                        />
                    </InfoCard>
                </Col>
                <Col className={styles.card} sm={6} md={8} lg={5}>
                    <InfoCard
                        buttonAction={() => history.push('/business')}
                        buttonText="My Businesses"
                        primaryButtonDisabled={!user.businesses}
                        secondaryAction={() => history.push('/business/new')}
                        secondaryButtonText="Create Business"
                        title="Business Info"
                        subtitle="Summary"
                    >
                        <CardInfo
                            infoTexts={[
                                {text: `Businesses: ${user.businesses?.length}`, icon: 'Home'},
                                {text: `Employees: ${userEmployees.length}`, icon: 'Hot'},
                                {
                                    text: `Bookings: ${userBusinessBookings.length}`,
                                    icon: 'Grain',
                                },
                            ]}
                        />
                    </InfoCard>
                </Col>
            </Row>
        </Container>
    );
};
