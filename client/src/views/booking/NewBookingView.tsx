import Chip from '@material-ui/core/Chip';
import {Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React from 'react';
import {useLocation, useHistory} from 'react-router-dom';

import {BusinessDTO, TimeSlotDTO} from '../../models/Business';
import {Header} from '../../components/Texts';
import {Modal} from '../../components/modal/Modal';
import {RequestHandler, useRequest} from '../../hooks/useRequest';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';
import URL from '../../api/URL';

const useStyles = makeStyles((theme) => ({
   row: {
      justifyContent: 'center',
   },
}));

interface LocationState {
   data: BusinessDTO;
}

const SUCCESS_MESSAGE = 'Booking Made - Go to my bookings to see your bookings';

export const NewBookingView: React.FC = () => {
   const styles = useStyles();
   const history = useHistory();
   const location = useLocation<LocationState>();

   const requestHandler: RequestHandler<TimeSlotDTO[]> = useRequest(SUCCESS_MESSAGE);

   const business: BusinessDTO = location.state.data;

   const columns: TableColumn[] = [
      {title: 'id', field: 'id', hidden: true},
      {title: 'Date', field: 'date'},
      {title: 'Start', field: 'start'},
      {title: 'End', field: 'end'},
   ];

   const actions = [
      {
         icon: () => <Chip size="small" label="Book Time" clickable color="primary" />,
         onClick: async (event: any, rowData: TimeSlotDTO) => {
            requestHandler.mutation(URL.getNewBookingURL(rowData.id), 'POST');
         },
      },
   ];
   return (
      <Container>
         <Row className={styles.row}>
            <Header text={`Available Time Slots For ${business.name}`} />
         </Row>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={10}>
               <TableContainer
                  actions={actions}
                  columns={columns}
                  fetchTableData={async () =>
                     await requestHandler.query(URL.getTimeSlotURL(business.id!))
                  }
                  tableTitle="Time Slots"
                  emptyMessage="No Time Slots Available"
               />

               <Modal
                  show={requestHandler.requestInfo ? true : false}
                  title="Booking Info"
                  text={requestHandler.requestInfo}
                  secondaryAction={() => requestHandler.setRequestInfo('')}
                  primaryAction={() => history.push('/user/bookings')}
                  primaryActionText="My Bookings"
               />
            </Col>
         </Row>
      </Container>
   );
};
