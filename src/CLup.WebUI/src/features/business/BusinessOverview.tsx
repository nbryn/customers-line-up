import React from 'react';
import {Col, Row} from 'react-bootstrap';
import makeStyles from '@mui/styles/makeStyles';
import {useHistory} from 'react-router-dom';

import {Header} from '../../shared/components/Texts';
import {InfoCard} from '../../shared/components/card/InfoCard';

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
    {area: 'Business Info', buttonText: 'Edit info', path: 'manage'},
    {area: 'Bookings', buttonText: 'Manage Bookings', path: 'bookings/manage'},
    {area: 'Messages', buttonText: 'Manage Messages', path: 'messages/manage'},
    {
        area: 'Time Slots',
        buttonText: 'Manage Time Slots',
        secondaryButtonText: 'Add Time Slots',
        path: 'timeslots/manage',
        secondaryPath: 'timeslots/new',
    },
    {
        area: 'Employees',
        buttonText: 'Manage Employees',
        secondaryButtonText: 'New Employee',
        path: 'employees/manage',
        secondaryPath: 'employees/new',
    },
];

export const BusinessOverview: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    return (
        <>
            <Row className={styles.row}>
                <Header text="Manage Business Business" />
            </Row>

            <Row className={styles.row}>
                <>
                    {businessAreas.map((entry) => {
                        return (
                            <Col key={entry.area} sm={6} md={8} lg={4}>
                                <InfoCard
                                    title={entry.area}
                                    buttonText={entry.buttonText}
                                    buttonAction={() => history.push(`/business/${entry.path}`)}
                                    secondaryButtonText={entry.secondaryButtonText}
                                    secondaryAction={() =>
                                        history.push(`/business/${entry.secondaryPath}`)
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
