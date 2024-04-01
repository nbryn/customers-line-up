import React from 'react';
import {Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@mui/styles';
import {useHistory} from 'react-router-dom';

import {CardInfo} from '../../shared/components/card/CardInfo';
import {Header} from '../../shared/components/Texts';
import {InfoCard} from '../../shared/components/card/InfoCard';
import {useGetUserQuery} from './UserApi';
import {useGetBusinessesByOwnerQuery} from '../business/BusinessApi';
import {
    BUSINESS_ROUTE,
    CREATE_BUSINESS_ROUTE,
    USER_BOOKINGS_ROUTE,
    USER_BUSINESS_ROUTE,
} from '../../app/RouteConstants';

const useStyles = makeStyles(() => ({
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

    const {data: getUserResponse} = useGetUserQuery();
    const {data: getBusinessesByOwnerResponse} = useGetBusinessesByOwnerQuery();

    return (
        <Container>
            <Row className={styles.center}>
                <Header text={`Welcome ${getUserResponse?.user?.name}`} />
            </Row>
            <Row className={styles.center}>
                <Col className={styles.card} sm={6} md={8} lg={5}>
                    <InfoCard
                        buttonAction={() => history.push(USER_BOOKINGS_ROUTE)}
                        buttonText="My Bookings"
                        secondaryAction={() => history.push(USER_BUSINESS_ROUTE)}
                        secondaryButtonText="New Booking"
                        title="Booking Info"
                        subtitle="Summary"
                    >
                        <CardInfo
                            infoTexts={[
                                {
                                    text: `Bookings: ${
                                        getUserResponse?.user?.bookings?.length ?? 0
                                    }`,
                                    icon: 'Home',
                                },
                                {
                                    text: `Next: ${
                                        getUserResponse?.user?.bookings?.[0]?.date ?? ''
                                    }`,
                                    icon: 'Hot',
                                },
                                {
                                    text: `Where: ${
                                        getUserResponse?.user?.bookings?.[0]?.business ?? ''
                                    }`,
                                    icon: 'Grain',
                                },
                            ]}
                        />
                    </InfoCard>
                </Col>
                <Col className={styles.card} sm={6} md={8} lg={5}>
                    <InfoCard
                        buttonAction={() => history.push(BUSINESS_ROUTE)}
                        buttonText="My Businesses"
                        primaryButtonDisabled={!getBusinessesByOwnerResponse?.businesses?.length}
                        secondaryAction={() => history.push(CREATE_BUSINESS_ROUTE)}
                        secondaryButtonText="Create Business"
                        title="Business Info"
                        subtitle="Summary"
                    >
                        <CardInfo
                            infoTexts={[
                                {
                                    text: `Businesses: ${
                                        getBusinessesByOwnerResponse?.businesses?.length ?? 0
                                    }`,
                                    icon: 'Home',
                                },
                                {
                                    text: `Employees: ${
                                        getBusinessesByOwnerResponse?.businesses?.flatMap(
                                            (business) => business.employees
                                        ).length ?? 0
                                    }`,
                                    icon: 'Hot',
                                },
                                {
                                    text: `Bookings: ${
                                        getBusinessesByOwnerResponse?.businesses?.flatMap(
                                            (business) => business.bookings
                                        ).length ?? 0
                                    }`,
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
