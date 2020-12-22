import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';
import {useLocation} from 'react-router-dom';

import URL from '../../api/URL';
import {TimeSlotDTO} from '../../models/TimeSlot';
import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../hooks/useRequest';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';
import {BusinessDTO} from '../../models/Business';

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
   const [removeTimeSlot, setRemoveTimeSlot] = useState<number | null>(null);

   const requestHandler: RequestHandler<TimeSlotDTO[]> = useRequest();

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
            await requestHandler.mutation(URL.getDeleteTimeSlotURL(rowData.id), 'DELETE');

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
                  fetchTableData={async () => {
                     const timeSlots = await requestHandler.query(
                        URL.getTimeSlotsURL(business.id)
                     );

                     return timeSlots.map((x) => ({
                        ...x,
                        interval: x.start + ' - ' + x.end,
                     }));
                  }}
                  tableTitle="Time Slots"
                  emptyMessage="No Time Slots Yet"
                  removeEntryId={removeTimeSlot}
               />
            </Col>
         </Row>
      </>
   );
};
