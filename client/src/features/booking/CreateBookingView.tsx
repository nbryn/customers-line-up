import React, {useState} from 'react';
import Chip from '@material-ui/core/Chip';
import {Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useLocation, useHistory} from 'react-router-dom';

import {BusinessDTO} from '../business/Business';
import {ErrorView} from '../../common/views/ErrorView';
import {Header} from '../../common/components/Texts';
import {MapModal} from '../../common/components/modal/MapModal';
import {Modal} from '../../common/components/modal/Modal';
import {TableColumn} from '../../common/components/Table';
import {TableContainer} from '../../common/containers/TableContainer';
import {TimeSlotDTO} from '../timeslot/TimeSlot';
import {useBookingService} from './BookingService';
import {useTimeSlotService} from '../timeslot/TimeSlotService';

const useStyles = makeStyles((theme) => ({
    address: {
        marginTop: 10,
    },
    badge: {
        top: -5,
        marginLeft: 35,
    },
    row: {
        justifyContent: 'center',
    },
}));

interface LocationState {
    business: BusinessDTO;
}

const SUCCESS_MESSAGE = 'Booking Made - Go to my bookings to see your bookings';

export const CreateBookingView: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    const location = useLocation<LocationState>();

    const [showMapModal, setShowMapModal] = useState<boolean>(false);

    const bookingService = useBookingService(SUCCESS_MESSAGE);
    const timeSlotService = useTimeSlotService();

    if (!location.state) {
        return <ErrorView />;
    }

    const {business} = location.state;

    const columns: TableColumn[] = [
        {title: 'id', field: 'id', hidden: true},
        {title: 'Date', field: 'date'},
        {title: 'Interval', field: 'interval'},
    ];

    const actions = [
        {
            icon: () => <Chip size="small" label="Book Time" clickable color="primary" />,
            onClick: async (event: any, rowData: TimeSlotDTO) => {
                await bookingService.createBooking(rowData.id);
            },
        },
    ];
    return (
        <Container>
            <Row className={styles.row}>
                <Header text={`Available Time Slots For ${business.name}`} />
            </Row>

            <MapModal
                visible={showMapModal}
                setVisible={() => setShowMapModal(false)}
                zoom={14}
                center={[business.longitude as number, business.latitude as number]}
                markers={[[business.longitude as number, business.latitude as number], 13]}
            />
            <Row className={styles.row}>
                <Col sm={6} md={8} lg={6} xl={10}>
                    <TableContainer
                        actions={actions}
                        columns={columns}
                        loading={timeSlotService.working}
                        fetchTableData={async () => {
                            const timeSlots = await timeSlotService.fetchTimeSlotsByBusiness(
                                business.id!
                            );

                            return timeSlots.map((x) => ({
                                ...x,
                                interval: x.start + ' - ' + x.end,
                            }));
                        }}
                        tableTitle={
                            <>
                                <h5 className={styles.address}>{business.address}</h5>
                                <Chip
                                    className={styles.badge}
                                    size="small"
                                    label="Open map"
                                    clickable
                                    color="secondary"
                                    onClick={() => setShowMapModal(true)}
                                />
                            </>
                        }
                        emptyMessage="No Time Slots Available"
                    />

                    <Modal
                        show={bookingService.requestInfo ? true : false}
                        title="Booking Info"
                        text={bookingService.requestInfo}
                        secondaryAction={() => bookingService.setRequestInfo('')}
                        primaryAction={() => history.push('/user/bookings')}
                        primaryActionText="My Bookings"
                    />
                </Col>
            </Row>
        </Container>
    );
};
