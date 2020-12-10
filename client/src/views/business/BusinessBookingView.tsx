import {Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';
import {useHistory} from 'react-router-dom';

import {BusinessDTO} from '../../models/dto/Business';
import {Header} from '../../components/Texts';
import {List, ListItem} from '../../components/List';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {TableColumn} from '../../components/Table';
import {TableContainer} from '../../containers/TableContainer';
import URL, {USER_BOOKINGS_URL, BUSINESSES_OWNER_URL} from '../../api/URL';

const useStyles = makeStyles((theme) => ({
   row: {
      justifyContent: 'center',
   },
}));

export const BusinessBookingView: React.FC = () => {
   const styles = useStyles();
   const history = useHistory();
   const [businessNames, setBusinessNames] = useState<ListItem[]>([]);

   const requestHandler: RequestHandler<BusinessDTO[]> = useRequest();

   // TODO: Show list first - When chosen Show Bookings

   useEffect(() => {
      (async () => {
         const businesses = await requestHandler.query(BUSINESSES_OWNER_URL);

         setBusinessNames(
            businesses.map((x) => ({
               id: x.id,
               name: x.name,
            }))
         );
      })();
   }, []);

   // const columns: TableColumn[] = [
   //    {title: 'businessId', field: 'businessId', hidden: true},
   //    {title: 'TimeSlotId', field: 'timeSlotId', hidden: true},
   //    {title: 'UserMail', field: 'UserMail'},
   //    {title: 'Capacity', field: 'capacity'},
   //    {title: 'Start', field: 'start'},
   //    {title: 'End', field: 'end'},
   // ];

   // const actions = [
   //    {
   //       icon: 'delete',
   //       tooltip: 'Delete Booking',
   //       onClick: async (event: any, rowData: BookingDTO) => {
   //          // setIdToBeRemoved(rowData.id);

   //          // await requestHandler.mutation(URL.getDeleteBookingURL(rowData.id), 'DELETE');
   //       },
   //    },
   // ];

   return (
      <Container>
         <Row className={styles.row}>
            <Header text="Choose Business" />
         </Row>
         <Row>
            <List
               listItems={businessNames}
               onClick={(businessId) => history.push('/businessbooking', {data: businessId})}
            />
         </Row>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={10}>
               {/* <TableContainer
                  actions={actions}
                  columns={columns}
                  fetchTableData={async () => await requestHandler.query(USER_BOOKINGS_URL)}
                  removeEntryId={idToBeRemoved}
                  tableTitle="Bookings"
                  emptyMessage="No Bookings Yet"
               /> */}
            </Col>
         </Row>
      </Container>
   );
};
