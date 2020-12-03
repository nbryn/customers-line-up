import {Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';

import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {TimeSlotDTO} from '../../models/dto/Business';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';
import URL, {USER_BOOKINGS_URL} from '../../api/URL';

const useStyles = makeStyles((theme) => ({
   row: {
      justifyContent: 'center',
   },
}));

export const UserBookingView: React.FC = () => {
   const styles = useStyles();
   const [idToBeRemoved, setIdToBeRemoved] = useState<number | null>(null);

   const requestHandler: RequestHandler<TimeSlotDTO[]> = useRequest();

   const columns: TableColumn[] = [
      {title: 'id', field: 'id', hidden: true},
      {title: 'Business', field: 'business'},
      {title: 'Date', field: 'date'},
      {title: 'Start', field: 'start'},
      {title: 'End', field: 'end'},
   ];

   const actions = [
      {
         icon: 'delete',
         tooltip: 'Delete Booking',
         onClick: async (event: any, rowData: TimeSlotDTO) => {
            setIdToBeRemoved(rowData.id);

            await requestHandler.mutation(URL.getDeleteBookingURL(rowData.id), 'DELETE');
         },
      },
   ];
   
   return (
      <Container>
         <Row className={styles.row}>
            <Header text="Your Bookings" />
         </Row>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={10}>
               <TableContainer
                  actions={actions}
                  columns={columns}
                  fetchTableData={async () => await requestHandler.query(USER_BOOKINGS_URL)}
                  removeEntryId={idToBeRemoved}
                  tableTitle="Bookings"
                  emptyMessage="No Bookings Yet"
               />
            </Col>
         </Row>
      </Container>
   );
};
