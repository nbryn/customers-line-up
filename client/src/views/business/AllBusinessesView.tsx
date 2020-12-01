import {Badge, Col, Container} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import React, {useEffect, useState} from 'react';
import {useHistory} from 'react-router';

import {BusinessDTO} from '../../models/dto/Business';
import {RequestHandler, useRequest} from '../../services/ApiService';
import {Table, TableColumn} from '../../components/Table';
import {ALL_BUSINESSES_URL} from '../../services/URL';


export const AllBusinessesView: React.FC = () => {
   const history = useHistory();
   const [businesses, setBusinesses] = useState<BusinessDTO[]>([]);

   const requestHandler: RequestHandler<BusinessDTO[], void> = useRequest();

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
                     Available Businesses
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
