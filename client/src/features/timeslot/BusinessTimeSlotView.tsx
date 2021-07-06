import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';
import {useLocation} from 'react-router-dom';

import {BusinessDTO} from '../business/Business';
import {ErrorView} from '../../app/ErrorView';
import {Header} from '../../common/components/Texts';
import {TimeSlotDTO} from './TimeSlot';
import {TableColumn} from '../../common/components/Table';
import {TableContainer} from '../../common/containers/TableContainer';
import {useTimeSlotService} from './TimeSlotService';

const useStyles = makeStyles((theme) => ({
    row: {
        justifyContent: 'center',
    },
}));

interface LocationState {
    business: BusinessDTO;
}

export const BusinessTimeSlotView: React.FC = () => {
    const styles = useStyles();
    const location = useLocation<LocationState>();
    const [removeTimeSlot, setRemoveTimeSlot] = useState<string | null>(null);

    const timeSlotService = useTimeSlotService();

    if (!location.state) {
        return <ErrorView />;
    }

    const {business} = location.state;

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
                await timeSlotService.deleteTimeSlot(rowData.id);

                setRemoveTimeSlot(rowData.id);
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
                        loading={timeSlotService.working}
                        fetchTableData={async () => {
                            const timeSlots = await timeSlotService.fetchAvailableTimeSlotsByBusiness(
                                business.id
                            );

                            return timeSlots.map((x) => ({
                                ...x,
                                interval: x.start + ' - ' + x.end,
                            }));
                        }}
                        tableTitle="Time Slots"
                        emptyMessage="No Time Slots Yet"
                        removeEntryWithId={removeTimeSlot}
                    />
                </Col>
            </Row>
        </>
    );
};
