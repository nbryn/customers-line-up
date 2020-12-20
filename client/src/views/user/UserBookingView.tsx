import Chip from '@material-ui/core/Chip';
import {Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';

import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {BookingDTO} from '../../dto/Booking';
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
   const [removeBooking, setRemoveBooking] = useState<number | null>(null);

   const requestHandler: RequestHandler<BookingDTO[]> = useRequest();

   const columns: TableColumn[] = [
      {title: 'id', field: 'id', hidden: true},
      {title: 'timeSlotid', field: 'timeSlotId', hidden: true},
      {title: 'Business', field: 'business'},
      {title: 'Date', field: 'date'},
      {title: 'Interval', field: 'interval'},
   ];

   const actions = [
      {
         icon: () => <Chip size="small" label="Delete" clickable color="primary" />,
         tooltip: 'Delete Booking',
         onClick: async (event: any, rowData: BookingDTO) => {
            await requestHandler.mutation(URL.getDeleteBookingForUserURL(rowData.timeSlotId), 'DELETE');
            
            setRemoveBooking(rowData.id);
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
                  removeEntryId={removeBooking}
                  tableTitle="Bookings"
                  emptyMessage="No Bookings Yet"
               />
            </Col>
         </Row>
      </Container>
   );
};
