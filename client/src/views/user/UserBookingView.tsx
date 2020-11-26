import {Badge, Col, Container} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import React, {useEffect, useState} from 'react';

import {TimeSlotDTO} from '../../models/dto/Business';
import BookingService from '../../services/BookingService';
import {Table, TableColumn} from '../../components/Table';

export const UserBookingView: React.FC = () => {
   const [loading, setLoading] = useState<boolean>(true);
   const [queues, setQueues] = useState<TimeSlotDTO[]>([]);

   useEffect(() => {
      (async () => {
         const queues: TimeSlotDTO[] = await BookingService.fetchUserBookings();

         setQueues(queues);
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
            await BookingService.deleteBooking(rowData.id);
         },
      },
   ];
   return (
      <Container>
         <div
            style={{
               position: 'absolute',
               left: '35%',
               width: 2000,
               textAlign: 'center',
            }}
         >
            <Col sm={8} md={8} lg={4}>
               <h1>
                  <Badge style={{marginBottom: 50}} variant="primary">
                     Your Bookings
                  </Badge>
               </h1>

               {loading ? (
                  <CircularProgress />
               ) : (
                  <Table
                     actions={actions}
                     columns={columns}
                     data={queues}
                     title="Bookings"
                     emptyMessage="No Bookings Yet"
                  />
               )}
            </Col>
         </div>
      </Container>
   );
};
