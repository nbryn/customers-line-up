import React, {useEffect} from 'react';
import {Badge, Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory} from 'react-router-dom';

import {CardInfo} from '../../common/components/card/CardInfo';
import {fetchUserInsights, selectUserInsights} from '../insights/insightsSlice';
import {HomeCard} from '../../common/components/card/HomeCard';
import {selectCurrentUser} from './userSlice';
import {useAppDispatch, useAppSelector} from '../../app/Store';


const useStyles = makeStyles((theme) => ({
    card: {
        textAlign: 'center',
        float: 'none',
        marginTop: 35,
    },
    headline: {
        marginTop: 10,
        textAlign: 'center',
    },
    icon1: {
        marginBottom: 10,
        marginLeft: -175,
    },
    icon2: {
        marginBottom: 10,
        marginLeft: -85,
    },
    icon3: {
        marginLeft: -175,
    },
    icon4: {
        marginBottom: 10,
        marginLeft: -170,
    },
    icon5: {
        marginBottom: 10,
        marginLeft: -185,
    },
    icon6: {
        marginLeft: -195,
    },
}));

// Refactor HomeCard BusinessCard into one.
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
            <Row>
                <Col className={styles.card} sm={6} md={8} lg={6}>
                    <HomeCard
                        buttonAction={() => history.push('/user/bookings')}
                        buttonText="My Bookings"
                        secondaryAction={() => history.push('/user/business')}
                        secondaryButtonText="New Booking"
                        title="Booking Info"
                        subtitle="Summary"
                    >
                        <CardInfo
                            icon1={{
                                text: `Bookings: ${userInsights?.bookings}`,
                                icon: 'Home',
                                styles: styles.icon1,
                            }}
                            icon2={{
                                text: `Next: ${userInsights?.nextBookingTime}`,
                                icon: 'Hot',
                                styles: styles.icon2,
                            }}
                            icon3={{
                                text: `Where: ${userInsights?.nextBookingBusiness}`,
                                icon: 'Grain',
                                styles: styles.icon3,
                            }}
                        />
                    </HomeCard>
                </Col>
                <Col className={styles.card} sm={6} md={8} lg={6}>
                    <HomeCard
                        buttonAction={() => history.push('/business')}
                        buttonText="My Businesses"
                        primaryButtonDisabled={!userInsights?.businesses}
                        secondaryAction={() => history.push('/business/new')}
                        secondaryButtonText="Create Business"
                        title="Business Info"
                        subtitle="Summary"
                    >
                        <CardInfo
                            icon1={{
                                text: `Businesses: ${userInsights?.businesses}`,
                                icon: 'Home',
                                styles: styles.icon4,
                            }}
                            icon2={{
                                text: `Employees: `,
                                icon: 'Hot',
                                styles: styles.icon5,
                            }}
                            icon3={{
                                text: `Bookings: `,
                                icon: 'Grain',
                                styles: styles.icon6,
                            }}
                        />
                    </HomeCard>
                </Col>
            </Row>
        </Container>
    );
};
