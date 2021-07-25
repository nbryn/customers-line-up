import React, {useEffect} from 'react';
import {Badge, Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory} from 'react-router-dom';

import {CardInfo} from '../../common/components/card/CardInfo';
import {fetchUserInsights, selectUserInsights} from '../insights/insightsSlice';
import {InfoCard} from '../../common/components/card/InfoCard';
import {selectCurrentUser} from './userSlice';
import {useAppDispatch, useAppSelector} from '../../app/Store';

const useStyles = makeStyles((theme) => ({
    card: {       
        marginTop: 35,
    },
    cards: {
        justifyContent: 'center',
    },
    headline: {
        marginTop: 10,
        textAlign: 'center',
    },
}));

// Also look taking TextField out of TextField modal and use children instead
export const HomeView: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    const dispatch = useAppDispatch();

    const user = useAppSelector(selectCurrentUser);
    const userInsights = useAppSelector(selectUserInsights);

    useEffect(() => {
        (async () => {
            dispatch(fetchUserInsights());
        })();
    }, []);

    return (
        <Container>
            <Row>
                <Col className={styles.headline}>
                    <h1>
                        <Badge variant="primary">Welcome {user?.name}</Badge>
                    </h1>
                </Col>
            </Row>
            <Row className={styles.cards}>
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
                                {text: `Bookings: ${userInsights?.ownBookings}`, icon: 'Home'},
                                {text: `Next: ${userInsights?.nextBookingTime}`, icon: 'Hot'},
                                {
                                    text: `Where: ${userInsights?.nextBookingBusiness}`,
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
                        primaryButtonDisabled={!userInsights?.businesses}
                        secondaryAction={() => history.push('/business/new')}
                        secondaryButtonText="Create Business"
                        title="Business Info"
                        subtitle="Summary"
                    >
                        <CardInfo
                            infoTexts={[
                                {text: `Businesses: ${userInsights?.businesses}`, icon: 'Home'},
                                {text: `Employees: ${userInsights?.employees}`, icon: 'Hot'},
                                {text: `Bookings: ${userInsights?.businessBookings}`, icon: 'Grain'},
                            ]}
                        />
                    </InfoCard>
                </Col>
            </Row>
        </Container>
    );
};
