import {Badge, Col, Container, Row} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';
import {useLocation, useHistory} from 'react-router-dom';

import {BusinessDTO, TimeSlotDTO} from '../../models/dto/Business';
import {Modal} from '../../components/Modal';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {Table, TableColumn} from '../../components/Table';
import URL from '../../api/URL';

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

interface LocationState {
   data: BusinessDTO;
}

const SUCCESS_MESSAGE = 'Booking Made - Go to my bookings to see your bookings';

export const CreateBookingView: React.FC = () => {
   const styles = useStyles();
   const location = useLocation<LocationState>();
   const history = useHistory();

   const [loading, setLoading] = useState<boolean>(true);
   const [timeSlots, setTimeSlots] = useState<TimeSlotDTO[]>([]);

   const requestHandler: RequestHandler<TimeSlotDTO[]> = useRequest(SUCCESS_MESSAGE);

   const business: BusinessDTO = location.state.data;

   useEffect(() => {
      (async () => {
         const timeSlots: TimeSlotDTO[] = await requestHandler.query(
            URL.getTimeSlotURL(business.id!)
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

            requestHandler.mutation(URL.getCreateBookingURL(rowData.id), 'POST');
         },
      },
   ];
   return (
      <Container>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={8} className={styles.badge}>
               <h1>
                  <Badge style={{marginBottom: 50}} variant="primary">
                     Available Time Slots for {business.name}
                  </Badge>
               </h1>
            </Col>
         </Row>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={10}>
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
         </Row>
      </Container>
   );
};
