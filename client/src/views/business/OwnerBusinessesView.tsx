import {Badge, Col, Container} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import React, {useEffect, useState} from 'react';

import {BUSINESSES_OWNER_URL} from '../../services/URL';
import {BusinessDTO} from '../../models/dto/Business';
import {RequestHandler, useRequest} from '../../services/ApiService';
import {Table, TableColumn} from '../../components/Table';

export const OwnerBusinessesView: React.FC = () => {
   const [businesses, setBusinesses] = useState<BusinessDTO[]>([]);

   const requestHandler: RequestHandler<BusinessDTO[], void> = useRequest();

   useEffect(() => {
      (async () => {
         const businesses: BusinessDTO[] = await requestHandler.query(
            BUSINESSES_OWNER_URL
         );

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
                     Your Businesses
                  </Badge>
               </h1>
               {businesses.length === 0 ? (
                  <CircularProgress />
               ) : (
                  <Table actions={actions} columns={columns} data={businesses} title="Businesses" />
               )}
            </Col>
         </div>
      </Container>
   );
};
