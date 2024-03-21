import React, {useState} from 'react';
import Chip from '@mui/material/Chip';
import {Col, Row} from 'react-bootstrap';
import makeStyles from '@mui/styles/makeStyles';

import {createBooking} from './BookingApi';
import {ErrorView} from '../../shared/views/ErrorView';
import {selectAvailableTimeSlotsForCurrentBusiness} from '../business/timeslot/TimeSlotState';
import {Header} from '../../shared/components/Texts';

import {MapModal} from '../../shared/components/modal/MapModal';
import {selectCurrentBusiness} from '../business/BusinessApi';
import type {TableColumn} from '../../shared/components/Table';
import {TableContainer} from '../../shared/containers/TableContainer';
import type {TimeSlotDTO} from '../business/timeslot/TimeSlot';
import {useAppDispatch, useAppSelector} from '../../app/Store';

const useStyles = makeStyles(() => ({
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

    const timeSlots = useAppSelector(selectAvailableTimeSlotsForCurrentBusiness);

    const columns: TableColumn[] = [
        {title: 'id', field: 'id', hidden: true},
        {title: 'Date', field: 'date'},
        {title: 'Interval', field: 'interval'},
    ];

    const actions = [
        {
            icon: () => <Chip size="small" label="Book Time" clickable color="primary" />,
            onClick: async (event: any, rowData: TimeSlotDTO) => {
                dispatch(createBooking({id: rowData.id, data: rowData.businessId}));
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
                        tableTitle={
                            <>
                                <h5 className={styles.address}>{business.street}</h5>
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
