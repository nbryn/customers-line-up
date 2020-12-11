import {Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React from 'react';
import {useLocation} from 'react-router-dom';

import URL from '../../api/URL';
import {BookingDTO} from '../../models/dto/Booking';
import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';

const useStyles = makeStyles((theme) => ({
   row: {
      justifyContent: 'center',
   },
}));

type BusinessInfo = {
   id: number;
   name: string;
};

interface LocationState {
   data: BusinessInfo;
}

export const BusinessBookingView: React.FC = () => {
   const styles = useStyles();
   const location = useLocation<LocationState>();

   const requestHandler: RequestHandler<BookingDTO[]> = useRequest();

   const {id, name} = location.state.data;

   const columns: TableColumn[] = [
      {title: 'businessId', field: 'businessId', hidden: true},
      {title: 'timeSlotId', field: 'timeSlotId', hidden: true},
      {title: 'UserMail', field: 'userMail'},
      {title: 'Capacity', field: 'capacity'},
      {title: 'Interval', field: 'interval'},
   ];

   const actions = [
      {
         icon: 'Delete',
         tooltip: 'Remove Booking',
         onClick: (event: any, rowData: any) => {
            console.log(rowData);
         },
      },
      {
         icon: 'Contact',
         tooltip: 'Contact User',
         onClick: (event: any, rowData: any) => {
            console.log(rowData);
         },
      },
   ];

   return (
      <Container>
         <Row className={styles.row}>
            <Header text={`Bookings For ${name}`} />
         </Row>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={10}>
               <TableContainer
                  actions={actions}
                  columns={columns}
                  fetchTableData={async () =>
                     await requestHandler.query(URL.getBusinessBookingsURL(id))
                  }
                  tableTitle="Bookings"
                  emptyMessage="No Bookings Yet"
               />
            </Col>
         </Row>
      </Container>
   );
};
