import React from 'react';
import Chip from '@mui/material/Chip';
import {Col, Row} from 'react-bootstrap';
import makeStyles from '@mui/styles/makeStyles';

import type {BookingDTO} from './Booking';
import {deleteBookingForBusiness, selectBookingsByBusiness} from './BookingApi';
import {ErrorView} from '../../shared/views/ErrorView';
import {Header} from '../../shared/components/Texts';
import {selectCurrentBusiness} from '../business/BusinessApi';
import type {TableColumn} from '../../shared/components/Table';
import {TableContainer} from '../../shared/containers/TableContainer';
import {useAppDispatch, useAppSelector} from '../../app/Store';

const useStyles = makeStyles(() => ({
    row: {
        justifyContent: 'center',
    },
}));

export const BusinessBookingView: React.FC = () => {
    const styles = useStyles();
    const dispatch = useAppDispatch();
    const business = useAppSelector(selectCurrentBusiness);

    if (!business) {
        return <ErrorView />;
    }

    const bookings = useAppSelector(selectBookingsByBusiness);

    const columns: TableColumn[] = [
        {title: 'id', field: 'id', hidden: true},
        {title: 'timeSlotId', field: 'timeSlotId', hidden: true},
        {title: 'User', field: 'userEmail'},
        {title: 'Interval', field: 'interval'},
        {title: 'Date', field: 'date'},
        {title: 'Capacity', field: 'capacity'},
    ];

    const actions = [
        {
            icon: () => <Chip size="small" label="Contact User" clickable color="primary" />,
            onClick: (event: React.ChangeEvent, rowData: BookingDTO) => {
                console.log(rowData);
            },
        },
        {
            icon: () => <Chip size="small" label="Delete Booking" clickable color="secondary" />,
            onClick: async (event: React.ChangeEvent, rowData: BookingDTO) => {
                dispatch(deleteBookingForBusiness({id: business.id, data: rowData.id}));
            },
        },
    ];

    return (
        <>
            <Row className={styles.row}>
                <Header text={`Bookings For ${business.name}`} />
            </Row>
            <Row className={styles.row}>
                <Col sm={6} md={8} lg={6} xl={10}>
                    <TableContainer
                        actions={actions}
                        columns={columns}
                        tableTitle="Bookings"
                        emptyMessage="No Bookings Yet"
                        tableData={bookings}
                    />
                </Col>
            </Row>
        </>
    );
};
