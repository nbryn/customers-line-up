import React from 'react';
import {useNavigate, useParams} from 'react-router-dom';
import {Col, Row} from 'react-bootstrap';
import makeStyles from '@mui/styles/makeStyles';

import {Header} from '../../shared/components/Texts';
import {InfoCard} from '../../shared/components/card/InfoCard';
import {
    BUSINESS_BOOKINGS_ROUTE,
    BUSINESS_MESSAGES_ROUTE,
    BUSINESS_PROFILE_ROUTE,
    BUSINESS_TIMESLOTS_ROUTE,
    CREATE_EMPLOYEE_ROUTE,
    GENERATE_TIMESLOTS_ROUTE,
    MANAGE_EMPLOYEES_ROUTE,
} from '../../app/RouteConstants';

const useStyles = makeStyles(() => ({
    row: {
        justifyContent: 'center',
    },
    spinner: {
        justifyContent: 'center',
        marginTop: 100,
    },
}));

const businessAreas = [
    {area: 'Business Info', buttonText: 'Edit info', path: BUSINESS_PROFILE_ROUTE},
    {area: 'Bookings', buttonText: 'Manage Bookings', path: BUSINESS_BOOKINGS_ROUTE},
    {area: 'Messages', buttonText: 'Manage Messages', path: BUSINESS_MESSAGES_ROUTE},
    {
        area: 'Time Slots',
        buttonText: 'Manage Time Slots',
        secondaryButtonText: 'Add Time Slots',
        path: BUSINESS_TIMESLOTS_ROUTE,
        secondaryPath: GENERATE_TIMESLOTS_ROUTE,
    },
    {
        area: 'Employees',
        buttonText: 'Manage Employees',
        secondaryButtonText: 'New Employee',
        path: MANAGE_EMPLOYEES_ROUTE,
        secondaryPath: CREATE_EMPLOYEE_ROUTE,
    },
];

export const BusinessOverview: React.FC = () => {
    const {businessId} = useParams();
    const navigate = useNavigate();
    const styles = useStyles();
    return (
        <>
            <Row className={styles.row}>
                <Header text="Manage Business" />
            </Row>

            <Row className={styles.row}>
                <>
                    {businessAreas.map((entry) => {
                        return (
                            <Col key={entry.area} sm={6} md={8} lg={4}>
                                <InfoCard
                                    title={entry.area}
                                    buttonText={entry.buttonText}
                                    buttonAction={() => navigate(`${entry.path}/${businessId}`)}
                                    secondaryButtonText={entry.secondaryButtonText}
                                    secondaryAction={() =>
                                        navigate(`${entry.secondaryPath}/${businessId}`)
                                    }
                                ></InfoCard>
                            </Col>
                        );
                    })}
                </>
            </Row>
        </>
    );
};
