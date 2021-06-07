import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';
import {useLocation} from 'react-router-dom';

import {BusinessDTO} from '../../models/Business';
import {BookingDTO} from '../../models/Booking';
import {ErrorView} from '../ErrorView';
import {Header} from '../../components/Texts';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';
import {useBookingService} from '../../services/BookingService';

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

    const [removeBooking, setRemoveBooking] = useState<string | null>(null);

    const bookingService = useBookingService();

    if (!location.state) {
        <ErrorView />;
    }

    const {business} = location.state;

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
                await bookingService.deleteBookingForBusiness(rowData.timeSlotId, rowData.userMail);

                setRemoveBooking(rowData.id);
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
                        loading={bookingService.working}
                        fetchTableData={async () =>
                            await bookingService.fetchBookingsByBusiness(business.id)
                        }
                        removeEntryId={removeBooking}
                        tableTitle="Bookings"
                        emptyMessage="No Bookings Yet"
                    />
                </Col>
            </Row>
        </>
    );
};
