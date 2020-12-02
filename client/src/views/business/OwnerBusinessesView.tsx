import {Badge, Col, Container, Row} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';

import {BUSINESSES_OWNER_URL} from '../../api/URL';
import {BusinessDTO} from '../../models/dto/Business';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {Table, TableColumn} from '../../components/Table';

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

export const OwnerBusinessesView: React.FC = () => {
   const styles = useStyles();
   const [businesses, setBusinesses] = useState<BusinessDTO[]>([]);

   const requestHandler: RequestHandler<BusinessDTO[]> = useRequest();

   useEffect(() => {
      (async () => {
         const businesses: BusinessDTO[] = await requestHandler.query(BUSINESSES_OWNER_URL);

         setBusinesses(businesses);
      })();
   }, []);

   const columns: TableColumn[] = [
      {title: 'Name', field: 'name'},
      {title: 'Zip', field: 'zip'},
      {title: 'Opens', field: 'opens'},
      {title: 'Closes', field: 'closes'},
      {title: 'Type', field: 'type'},
   ];

   const actions = [
      {
         icon: 'edit',
         tooltip: 'Edit Business',
         onClick: (event: any, rowData: any) => {
            console.log(rowData);
         },
      },
   ];

   return (
      <Container>
         <Row className={styles.row}>
            <Col sm={6} md={8} lg={6} xl={9}>
               <h1 className={styles.badge}>
                  <Badge variant="primary">Your Businesses</Badge>
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
