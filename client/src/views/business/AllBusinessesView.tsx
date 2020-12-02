import {Badge, Col, Container, Row} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';
import {useHistory} from 'react-router';

import {BusinessDTO} from '../../models/dto/Business';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {Table, TableColumn} from '../../components/Table';
import {ALL_BUSINESSES_URL} from '../../api/URL';

const useStyles = makeStyles((theme) => ({
   badge: {
      marginTop: 15,
      marginBottom: 25,
      textAlign: 'center'
   },
   row: {
      justifyContent: 'center',
   },
}));

export const AllBusinessesView: React.FC = () => {
   const styles = useStyles();
   const history = useHistory();

   const [businesses, setBusinesses] = useState<BusinessDTO[]>([]);
   const requestHandler: RequestHandler<BusinessDTO[]> = useRequest();

   useEffect(() => {
      (async () => {
         const businesses: BusinessDTO[] = await requestHandler.query(ALL_BUSINESSES_URL);

         setBusinesses(businesses);
      })();
   }, []);

   const columns: TableColumn[] = [
      {title: 'id', field: 'id', hidden: true},
      {title: 'Name', field: 'name'},
      {title: 'Zip', field: 'zip'},
      {title: 'Opens', field: 'opens'},
      {title: 'Closes', field: 'closes'},
      {title: 'Type', field: 'type'},
   ];

   const actions = [
      {
         icon: 'info',
         tooltip: 'See available time slots',
         onClick: (event: any, rowData: BusinessDTO) => {
            history.push('/booking', {data: rowData});
         },
      },
   ];

   return (
      <Container>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={8} className={styles.badge}>
               <h1>
                  <Badge variant="primary">Available Businesses</Badge>
               </h1>
            </Col>
         </Row>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={10}>
               {businesses.length === 0 ? (
                  <CircularProgress />
               ) : (
                  <Table actions={actions} columns={columns} data={businesses} title="Businesses" />
               )}
            </Col>
         </Row>
      </Container>
   );
};
