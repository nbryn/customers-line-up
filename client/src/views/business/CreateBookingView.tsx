import {Badge, Col, Container} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import React, {useEffect, useState} from 'react';
import {useLocation} from 'react-router-dom';

import BookingService from '../../services/BookingService';
import {BusinessDTO, TimeSlotDTO} from '../../models/dto/Business';
import TimeSlotService from '../../services/TimeSlotService';
import {Table, TableColumn} from '../../components/Table';

interface LocationState {
   data: BusinessDTO;
}

export const CreateBookingView: React.FC = () => {
   const location = useLocation<LocationState>();

   const [queues, setQueues] = useState<TimeSlotDTO[]>([]);

   const business: BusinessDTO = location.state.data;

   useEffect(() => {
      (async () => {
         console.log(business);
         const queues: TimeSlotDTO[] = await TimeSlotService.fetchAvailableTimeSlotsForBusiness(
            business.id!
         );

         setQueues(queues);
      })();
   }, []);

   const columns: TableColumn[] = [
      {title: 'id', field: 'id', hidden: true},
      {title: 'Date', field: 'date'},
      {title: 'Start', field: 'start'},
      {title: 'End', field: 'end'},
   ];

   const actions = [
      {
         icon: 'book',
         tooltip: 'Book Time',
         onClick: async (event: any, rowData: TimeSlotDTO) => {
            console.log(rowData);
            await BookingService.createBooking(rowData.id);
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
                     Available Time Slots for {business.name}
                  </Badge>
               </h1>
               {queues.length === 0 ? (
                  <CircularProgress />
               ) : (
                  <Table actions={actions} columns={columns} data={queues} title="Time Slots" />
               )}
            </Col>
         </div>
      </Container>
   );
};
