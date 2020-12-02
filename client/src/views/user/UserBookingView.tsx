import {Badge, Col, Container, Row} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';

import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {TimeSlotDTO} from '../../models/dto/Business';
import {Table, TableColumn} from '../../components/Table';
import URL, {USER_BOOKINGS_URL} from '../../api/URL';

const useStyles = makeStyles((theme) => ({
   badge: {
      marginTop: 15,
      marginBottom: 25,
      textAlign: 'center',
   },
   row: {
      justifyContent: 'center',
   },
}));

export const UserBookingView: React.FC = () => {
   const styles = useStyles();

   const [loading, setLoading] = useState<boolean>(true);
   const [bookings, setbookings] = useState<TimeSlotDTO[]>([]);

   const requestHandler: RequestHandler<TimeSlotDTO[]> = useRequest();

   useEffect(() => {
      (async () => {
         const bookings: TimeSlotDTO[] = await requestHandler.query(USER_BOOKINGS_URL);

         setbookings(bookings);
         setLoading(false);
      })();
   }, []);

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
            const _bookings = bookings.filter((b) => b.id !== rowData.id);
            setbookings(_bookings);

            await requestHandler.mutation(URL.getDeleteBookingURL(rowData.id), 'DELETE');
         },
      },
   ];
   return (
      <Container>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={9}>
               <h1 className={styles.badge}>
                  <Badge variant="primary">Your Bookings</Badge>
               </h1>
            </Col>
         </Row>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={10}>
               {loading ? (
                  <CircularProgress />
               ) : (
                  <Table
                     actions={actions}
                     columns={columns}
                     data={bookings}
                     title="Bookings"
                     emptyMessage="No Bookings Yet"
                  />
               )}
            </Col>
         </Row>
      </Container>
   );
};
