import React from 'react';
import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useLocation} from 'react-router-dom';

import {BusinessDTO} from '../business/Business';
import {BookingDTO} from './Booking';
import {
    deleteBookingForBusiness,
    fetchBookingsByBusiness,
    selectBookingsByBusiness,
} from './bookingSlice';
import {State, isLoading, useAppDispatch, useAppSelector} from '../../app/Store';
import {ErrorView} from '../../common/views/ErrorView';
import {Header} from '../../common/components/Texts';
import {TableColumn} from '../../common/components/Table';
import {TableContainer} from '../../common/containers/TableContainer';




const useStyles = makeStyles((theme) => ({
    row: {
        justifyContent: 'center',
    },
}));

interface LocationState {
    business: BusinessDTO;
}

export const BusinessBookingView: React.FC = () => {
    const styles = useStyles();
    const location = useLocation<LocationState>();
    const dispatch = useAppDispatch();

    if (!location.state) {
        <ErrorView />;
    }

    const {business} = location.state;

    const loading = useAppSelector(isLoading(State.Bookings));
    const bookings = useAppSelector(selectBookingsByBusiness(business.id));

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
                dispatch(
                    deleteBookingForBusiness({id: business.id, data: rowData.id})
                );
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
                        loading={loading}
                        fetchData={() => dispatch(fetchBookingsByBusiness(business.id))}
                        tableTitle="Bookings"
                        emptyMessage="No Bookings Yet"
                        tableData={bookings}
                    />
                </Col>
            </Row>
        </>
    );
};
