import React, {useState} from 'react';
import Chip from '@material-ui/core/Chip';
import {Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useLocation, useHistory} from 'react-router-dom';

import {BusinessDTO, TimeSlotDTO} from '../../models/Business';
import {ErrorView} from '../ErrorView';
import {Header} from '../../components/Texts';
import {MapModal} from '../../components/modal/MapModal';
import {Modal} from '../../components/modal/Modal';
import {ApiCaller, useApi} from '../../hooks/useApi';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';
import URL from '../../api/URL';

const useStyles = makeStyles((theme) => ({
    address: {marginTop: 10},
    badge: {top: -5, marginLeft: 35},
    row: {
        justifyContent: 'center',
    },
}));

interface LocationState {
    business: BusinessDTO;
}

const SUCCESS_MESSAGE = 'Booking Made - Go to my bookings to see your bookings';

export const NewBookingView: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    const location = useLocation<LocationState>();

    const [showMapModal, setShowMapModal] = useState<boolean>(false);

    const apiCaller: ApiCaller<TimeSlotDTO[]> = useApi(SUCCESS_MESSAGE);

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
                apiCaller.mutation(URL.getNewBookingURL(rowData.id), 'POST');
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
                        fetchTableData={async () => {
                            const timeSlots = await apiCaller.query(
                                URL.getTimeSlotURL(business.id!)
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
                        show={apiCaller.requestInfo ? true : false}
                        title="Booking Info"
                        text={apiCaller.requestInfo}
                        secondaryAction={() => apiCaller.setRequestInfo('')}
                        primaryAction={() => history.push('/user/bookings')}
                        primaryActionText="My Bookings"
                    />
                </Col>
            </Row>
        </Container>
    );
};
