import {Badge, Col, Container} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import React, {useEffect, useState} from 'react';
import {useLocation, useHistory} from 'react-router-dom';

import BookingService from '../../services/BookingService';
import {BusinessDTO, TimeSlotDTO} from '../../models/dto/Business';
import {Modal} from '../../components/Modal';
import URLService from '../../services/TimeSlotService';
import {Table, TableColumn} from '../../components/Table';

import {RequestHandler, useRequest} from '../../services/ApiService';

import ApiService from '../../services/ApiService';

interface LocationState {
   data: BusinessDTO;
}

export const CreateBookingView: React.FC = () => {
   const location = useLocation<LocationState>();
   const history = useHistory();

   const [modalText, setModalText] = useState<string>('');
   const [loading, setLoading] = useState<boolean>(true);
   const [timeSlots, setTimeSlots] = useState<TimeSlotDTO[]>([]);

   const requestHandler: RequestHandler<TimeSlotDTO[], void> = useRequest();

   const business: BusinessDTO = location.state.data;

   useEffect(() => {
      (async () => {
         // const timeSlots: TimeSlotDTO[] = await ApiService.request(
         //    () => TimeSlotService.fetchAvailableTimeSlotsForBusiness(business.id!),
         //    setModalText
         // );

         const timeSlots: TimeSlotDTO[] = await requestHandler.query(
            URLService.getTimeSlotURL(business.id!)
         );

         setTimeSlots(timeSlots);
         setLoading(false);
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
            console.log(rowData.id);
            //ApiService.request(() => BookingService.createBooking(rowData.id), setModalText);

            requestHandler.mutation(URLService.getCreateBookingURL(rowData.id), 'POST');

            requestHandler.setRequestInfo('Booking Made - Go to my bookings to see your bookings');
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

               {loading ? (
                  <CircularProgress />
               ) : (
                  <>
                     <Modal
                        show={requestHandler.requestInfo ? true : false}
                        title="Booking Info"
                        text={requestHandler.requestInfo}
                        secondaryAction={() => requestHandler.setRequestInfo('')}
                        primaryAction={() => history.push('/mybookings')}
                        primaryActionText="My Bookings"
                     />
                     <Table
                        actions={actions}
                        columns={columns}
                        data={timeSlots}
                        title="Time Slots"
                        emptyMessage="No Time Slots Available"
                     />
                  </>
               )}
            </Col>
         </div>
      </Container>
   );
};
