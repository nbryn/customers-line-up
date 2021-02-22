import Chip from '@material-ui/core/Chip';
import {Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';

import {ApiCaller, useApi} from '../../hooks/useApi';
import {BookingDTO} from '../../models/Booking';
import {Header} from '../../components/Texts';
import {MapModal, MapModalProps, defaultMapProps} from '../../components/modal/MapModal';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';
import URL, {USER_BOOKINGS_URL} from '../../api/URL';

const useStyles = makeStyles((theme) => ({
    row: {
        justifyContent: 'center',
    },
}));

export const UserBookingView: React.FC = () => {
    const styles = useStyles();

    const [removeBooking, setRemoveBooking] = useState<number | null>(null);
    const [mapModalInfo, setMapModalInfo] = useState<MapModalProps>(defaultMapProps);

    const apiCaller: ApiCaller<BookingDTO[]> = useApi();

    const columns: TableColumn[] = [
        {title: 'id', field: 'id', hidden: true},
        {title: 'timeSlotid', field: 'timeSlotId', hidden: true},
        {title: 'Date', field: 'date'},
        {title: 'Business & Zip', field: 'business'},
        {title: 'Address', field: 'address'},
        {title: 'Interval', field: 'interval'},
    ];

    const actions = [
        {
            icon: () => <Chip size="small" label="Delete" clickable color="primary" />,
            tooltip: 'Delete Booking',
            onClick: async (event: any, booking: BookingDTO) => {
                await apiCaller.mutation(
                    URL.getDeleteBookingForUserURL(booking.timeSlotId),
                    'DELETE'
                );

                setRemoveBooking(booking.id);
            },
        },
        {
            icon: () => <Chip size="small" label="See on map" clickable color="secondary" />,
            tooltip: 'Show location on map',
            onClick: (event: any, booking: BookingDTO) => {
                setMapModalInfo({
                    visible: true,
                    zoom: 14,
                    center: [booking.longitude as number, booking.latitude as number],
                    markers: [[booking.longitude as number, booking.latitude as number], 13],
                });
            },
        },
    ];

    return (
        <Container>
            <Row className={styles.row}>
                <Header text="Your Bookings" />
            </Row>
            <MapModal
                visible={mapModalInfo.visible}
                setVisible={() => setMapModalInfo(defaultMapProps)}
                zoom={mapModalInfo.zoom}
                center={mapModalInfo.center}
                markers={mapModalInfo.markers}
            />
            <Row className={styles.row}>
                <Col sm={6} md={8} lg={6} xl={10}>
                    <TableContainer
                        actions={actions}
                        columns={columns}
                        fetchTableData={async () => await apiCaller.query(USER_BOOKINGS_URL)}
                        removeEntryId={removeBooking}
                        tableTitle="Bookings"
                        emptyMessage="No Bookings Yet"
                    />
                </Col>
            </Row>
        </Container>
    );
};
