import React from 'react';
import Chip from '@mui/material/Chip';
import {Col, Row} from 'react-bootstrap';
import makeStyles from '@mui/styles/makeStyles';

import {ErrorView} from '../../shared/views/ErrorView';
import {Header} from '../../shared/components/Texts';
import {selectCurrentBusiness} from '../business/BusinessState';
import {Table, type TableColumn, type TableData} from '../../shared/components/Table';
import {useAppSelector} from '../../app/Store';
import {useDeleteBusinessBookingMutation} from './BookingApi';
import {type BookingDto} from '../../autogenerated';
import {formatInterval} from '../business/AllBusinessesView';

const useStyles = makeStyles(() => ({
    row: {
        justifyContent: 'center',
    },
}));

const mapBookingToTableData = (bookings: BookingDto[] | undefined | null): TableData =>
    bookings?.map((booking) => ({
        id: booking?.id ?? '',
        timeSlotId: booking?.timeSlotId ?? '',
        userEmail: booking?.userEmail ?? '',
        interval: formatInterval(booking?.interval),
        date: booking?.date ?? '',
        capacity: booking?.capacity ?? '',
    })) ?? [];

export const BusinessBookingView: React.FC = () => {
    const styles = useStyles();
    const [deleteBookingForBusiness] = useDeleteBusinessBookingMutation();
    const business = useAppSelector(selectCurrentBusiness);

    if (!business) {
        return <ErrorView />;
    }

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
            onClick: (_: React.ChangeEvent, rowData: BookingDto) => {
                console.log(rowData);
            },
        },
        {
            icon: () => <Chip size="small" label="Delete Booking" clickable color="secondary" />,
            onClick: async (_: React.ChangeEvent, rowData: BookingDto) => {
                await deleteBookingForBusiness({businessId: business.id!, bookingId: rowData.id!});
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
                    <Table
                        actions={actions}
                        columns={columns}
                        title="Bookings"
                        emptyMessage="No Bookings Yet"
                        data={mapBookingToTableData(business.bookings)}
                    />
                </Col>
            </Row>
        </>
    );
};
