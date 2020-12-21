import Chip from '@material-ui/core/Chip';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';
import {useLocation} from 'react-router-dom';

import URL from '../../api/URL';
import {BusinessDTO} from '../../models/Business';
import {BookingDTO} from '../../models/Booking';
import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../hooks/useRequest';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';

const useStyles = makeStyles((theme) => ({
   row: {
      justifyContent: 'center',
   },
}));

interface LocationState {
   business: BusinessDTO;
}

export const BusinessBookingView: React.FC = () => {
   const styles = useStyles();
   const location = useLocation<LocationState>();

   const [removeBooking, setRemoveBooking] = useState<number | null>(null);

   const requestHandler: RequestHandler<BookingDTO[]> = useRequest();

   const {business} = location.state;

   const columns: TableColumn[] = [
      {title: 'id', field: 'id', hidden: true},
      {title: 'timeSlotId', field: 'timeSlotId', hidden: true}, 
      {title: 'UserMail', field: 'userMail'},
      {title: 'Interval', field: 'interval'}, 
      {title: 'Date', field: 'date'},   
      {title: 'Capacity', field: 'capacity'},
      
   ];

   const actions = [
      {
         icon: () => <Chip size="small" label="Contact User" clickable color="primary" />,
         onClick: (event: React.ChangeEvent, rowData: BookingDTO) => {
            console.log(rowData);
         },
      },
      {
         icon: () => <Chip size="small" label="Delete Booking" clickable color="secondary" />,
         onClick: async (event: React.ChangeEvent, rowData: BookingDTO) => {
            const url = URL.getDeleteBookingForBusinessURL(rowData.timeSlotId, rowData.userMail);

            await requestHandler.mutation(url, "DELETE");

            setRemoveBooking(rowData.id);
         },
      },
   ];

   return (
      <>
         <Row className={styles.row}>
            <Header text={`Bookings For ${business.name}`} />
         </Row>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={10}>
               <TableContainer
                  actions={actions}
                  columns={columns}
                  fetchTableData={async () =>
                     await requestHandler.query(URL.getBusinessBookingsURL(business.id))
                  }
                  removeEntryId={removeBooking}
                  tableTitle="Bookings"
                  emptyMessage="No Bookings Yet"
               />
            </Col>
         </Row>
      </>
   );
};
