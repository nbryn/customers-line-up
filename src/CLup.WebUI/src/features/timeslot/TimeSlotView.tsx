import React from 'react';
import {useParams} from 'react-router-dom';
import Chip from '@mui/material/Chip';
import {Col, Row} from 'react-bootstrap';
import makeStyles from '@mui/styles/makeStyles';

import {useDeleteTimeSlotMutation} from './TimeSlotApi';
import {Header} from '../../shared/components/Texts';
import {Table, type TableData, type TableColumn} from '../../shared/components/Table';
import {type TimeSlotDto} from '../../autogenerated';
import {formatInterval} from '../business/AllBusinessesView';
import {useGetBusinessAggregateByIdQuery} from '../business/BusinessApi';

const useStyles = makeStyles(() => ({
    row: {
        justifyContent: 'center',
    },
}));

export const mapTimeSlotsToTableData = (timeSlots: TimeSlotDto[] | undefined | null): TableData =>
    timeSlots?.map((timeSlot) => ({
        id: timeSlot.id ?? '',
        date: timeSlot.date ?? '',
        interval: formatInterval(timeSlot.timeInterval),
        capacity: timeSlot.capacity ?? '',
    })) ?? [];

export const TimeSlotView: React.FC = () => {
    const {businessId} = useParams();
    const styles = useStyles();

    const {data: business} = useGetBusinessAggregateByIdQuery(businessId!);
    const [deleteTimeSlot] = useDeleteTimeSlotMutation();

    const columns: TableColumn[] = [
        {title: 'timeSlotId', field: 'timeSlotId', hidden: true},
        {title: 'Date', field: 'date'},
        {title: 'Interval', field: 'interval'},
        {title: 'Capacity', field: 'capacity'},
    ];

    const actions = [
        {
            icon: () => <Chip size="small" label="Delete Time Slot" clickable color="primary" />,
            onClick: async (_: any, timeSlot: TimeSlotDto) => {
                await deleteTimeSlot({
                    businessId: business?.id ?? '',
                    timeSlotId: timeSlot.id ?? '',
                });
            },
        },
    ];

    return (
        <>
            <Row className={styles.row}>
                <Header text={business?.name ?? ''} />
            </Row>
            <Row className={styles.row}>
                <Col sm={6} md={8} lg={6} xl={10}>
                    <Table
                        actions={actions}
                        columns={columns}
                        data={mapTimeSlotsToTableData(business?.timeSlots)}
                        title="Time Slots"
                        emptyMessage="No Time Slots Yet"
                    />
                </Col>
            </Row>
        </>
    );
};
