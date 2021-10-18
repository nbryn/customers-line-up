import React, {useState} from 'react';
import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';

import {createBooking} from './bookingSlice';
import {ErrorView} from '../../common/views/ErrorView';
import {
    fetchAvailableTimeSlotsByBusiness,
    selectAvailableTimeSlotsByBusiness,
} from '../timeslot/timeSlotSlice';
import {Header} from '../../common/components/Texts';

import {MapModal} from '../../common/components/modal/MapModal';
import {selectCurrentBusiness} from '../business/businessSlice';
import {TableColumn} from '../../common/components/Table';
import {TableContainer} from '../../common/containers/TableContainer';
import {TimeSlotDTO} from '../timeslot/TimeSlot';
import {useAppDispatch, useAppSelector} from '../../app/Store';

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

export const CreateBookingView: React.FC = () => {
    const styles = useStyles();
    const dispatch = useAppDispatch();

    const business = useAppSelector(selectCurrentBusiness);
    const [showMapModal, setShowMapModal] = useState<boolean>(false);

    if (!business) {
        return <ErrorView />;
    }

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
