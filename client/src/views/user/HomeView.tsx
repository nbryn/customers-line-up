import React, {useEffect, useState} from 'react';
import {Badge, Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import {useHistory} from 'react-router-dom';

import {HomeCard} from '../../components/card/HomeCard';
import {useUserContext} from '../../context/UserContext';
import {UserInsights} from '../../models/User';
import {useInsightsService} from '../../services/InsightsService';

const useStyles = makeStyles((theme) => ({
    headline: {
        marginTop: 10,
        textAlign: 'center',
    },
    card: {
        textAlign: 'center',
        float: 'none',
        marginTop: 35,
    },
}));

// Refactor HomeCard BusinessCard into one. 
// Also look taking TextField out of TextField modal and use children instead
export const HomeView: React.FC = () => {
    const [userInsights, setUserInsights] = useState({} as UserInsights);

    const styles = useStyles();
    const {user} = useUserContext();

    const history = useHistory();
    const insightsService = useInsightsService();

    useEffect(() => {
        (async () => {
            const insights = await insightsService.fetchUserInsights();

            setUserInsights(insights);
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
                    >
                        <Typography>Booking Total: {userInsights.bookings}</Typography>
                        <Typography>Next Booking: {userInsights.nextBookingTime}</Typography>
                        <Typography>Where: {userInsights.nextBookingBusiness}</Typography>
                    </HomeCard>
                </Col>
                <Col className={styles.card} sm={6} md={8} lg={6}>
                    <HomeCard
                        buttonAction={() => history.push('/business')}
                        buttonText="My Businesses"
                        secondaryAction={() => history.push('/business/new')}
                        secondaryButtonText="Create Business"
                        title="Business Info"
                    >
                        <Typography>Number of Businesses: {userInsights.businesses}</Typography>
                        <Typography>"Number of Employees"</Typography>
                        <Typography>"Number of Bookings"</Typography>
                    </HomeCard>
                </Col>
            </Row>
        </Container>
    );
};
