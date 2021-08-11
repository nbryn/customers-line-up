import React, {useState} from 'react';
import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useLocation, useHistory} from 'react-router-dom';

import {BusinessDTO} from '../business/Business';
import {createBooking} from './bookingSlice';
import {ErrorView} from '../../common/views/ErrorView';
import {
    fetchAvailableTimeSlotsByBusiness,
    selectAvailableTimeSlotsByBusiness,
} from '../timeslot/timeSlotSlice';
import {Header} from '../../common/components/Texts';
import {useAppDispatch, useAppSelector} from '../../app/Store';
import {MapModal} from '../../common/components/modal/MapModal';
import {selectApiState} from '../../common/api/apiSlice';
import {TableColumn} from '../../common/components/Table';
import {TableContainer} from '../../common/containers/TableContainer';
import {TimeSlotDTO} from '../timeslot/TimeSlot';

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

export const CreateBookingView: React.FC = () => {
    const styles = useStyles();
    const location = useLocation<LocationState>();
    const dispatch = useAppDispatch();

    const apiState = useAppSelector(selectApiState);
    const [showMapModal, setShowMapModal] = useState<boolean>(false);

    if (!location.state) {
        return <ErrorView />;
    }

    const {business} = location.state;
    const timeSlots = useAppSelector(selectAvailableTimeSlotsByBusiness(business.id));

    const columns: TableColumn[] = [
        {title: 'id', field: 'id', hidden: true},
        {title: 'Date', field: 'date'},
        {title: 'Interval', field: 'interval'},
    ];

    const actions = [
        {
            icon: () => <Chip size="small" label="Book Time" clickable color="primary" />,
            onClick: async (event: any, rowData: TimeSlotDTO) => {
                dispatch(createBooking(rowData.id));
            },
        },
    ];
    return (
        <>
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
                        loading={apiState.loading}
                        tableData={timeSlots}
                        fetchData={() => dispatch(fetchAvailableTimeSlotsByBusiness(business.id))}
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
                </Col>
            </Row>
        </>
    );
};
