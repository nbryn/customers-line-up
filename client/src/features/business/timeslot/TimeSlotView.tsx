import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React from 'react';

import {
    deleteTimeSlot,
    fetchTimeSlotsByBusiness,
    selectTimeSlotsByBusiness,
} from './timeSlotSlice';
import {ErrorView} from '../../../shared/views/ErrorView';
import {Header} from '../../../shared/components/Texts';
import {selectCurrentBusiness} from '../businessSlice';
import {TimeSlotDTO} from './TimeSlot';
import {TableColumn} from '../../../shared/components/Table';
import {TableContainer} from '../../../shared/containers/TableContainer';
import {useAppDispatch, useAppSelector} from '../../../app/Store';

const useStyles = makeStyles((theme) => ({
    row: {
        justifyContent: 'center',
    },
}));

export const TimeSlotView: React.FC = () => {
    const styles = useStyles();
    const business = useAppSelector(selectCurrentBusiness);
    const dispatch = useAppDispatch();

    if (!business) {
        return <ErrorView />;
    }

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
            <Row className={styles.row}>
                <Header text={business.name} />
            </Row>
            <Row className={styles.row}>
                <Col sm={6} md={8} lg={6} xl={10}>
                    <TableContainer
                        actions={actions}
                        columns={columns}
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
