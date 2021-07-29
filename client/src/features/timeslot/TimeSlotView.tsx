import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React from 'react';
import {useLocation} from 'react-router-dom';

import {BusinessDTO} from '../business/Business';
import {
    deleteTimeSlot,
    fetchTimeSlotsByBusiness,
    selectTimeSlotsByBusiness,
} from '../timeslot/timeSlotSlice';
import {State, isLoading, useAppDispatch, useAppSelector} from '../../app/Store';
import {ErrorView} from '../../common/views/ErrorView';
import {Header} from '../../common/components/Texts';
import {TimeSlotDTO} from './TimeSlot';
import {TableColumn} from '../../common/components/Table';
import {TableContainer} from '../../common/containers/TableContainer';

const useStyles = makeStyles((theme) => ({
    headline: {
        marginTop: 75,
        justifyContent: 'center',
    },
    row: {
        justifyContent: 'center',
    },
}));

interface LocationState {
    business: BusinessDTO;
}

export const TimeSlotView: React.FC = () => {
    const styles = useStyles();
    const location = useLocation<LocationState>();

    const dispatch = useAppDispatch();
    const loading = useAppSelector(isLoading(State.TimeSlots));

    if (!location.state) {
        return <ErrorView />;
    }

    const {business} = location.state;
    const timeSlots = useAppSelector(selectTimeSlotsByBusiness(business.id));

    const columns: TableColumn[] = [
        {title: 'timeSlotId', field: 'timeSlotId', hidden: true},
        {title: 'Date', field: 'date'},
        {title: 'Interval', field: 'interval'},
        {title: 'Capacity', field: 'capacity'},
    ];

    const actions = [
        {
            icon: () => <Chip size="small" label="Delete Time Slot" clickable color="primary" />,
            onClick: async (event: any, rowData: TimeSlotDTO) => {
                dispatch(deleteTimeSlot({businessId: business.id, timeSlotId: rowData.id}));
            },
        },
    ];

    return (
        <>
            <Row className={styles.headline}>
                <Header text={business.name} />
            </Row>
            <Row className={styles.row}>
                <Col sm={6} md={8} lg={6} xl={10}>
                    <TableContainer
                        actions={actions}
                        columns={columns}
                        loading={loading}
                        tableData={timeSlots}
                        fetchData={() => dispatch(fetchTimeSlotsByBusiness(business.id))}
                        tableTitle="Time Slots"
                        emptyMessage="No Time Slots Yet"
                    />
                </Col>
            </Row>
        </>
    );
};
