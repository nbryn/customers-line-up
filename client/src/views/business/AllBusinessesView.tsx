import {Badge, Col, Container} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import React, {useEffect, useState} from 'react';
import {useHistory} from 'react-router';

import {BusinessDTO} from '../../models/dto/Business';
import BusinessService from '../../services/BusinessService';
import {Table, TableColumn} from '../../components/Table';

export const AllBusinessesView: React.FC = () => {
   const history = useHistory();
   const [businesses, setBusinesses] = useState<BusinessDTO[]>([]);

   useEffect(() => {
      (async () => {
         const businesses: BusinessDTO[] = await BusinessService.fetchAllBusinesses();

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
         tooltip: 'Book Visit',
         onClick: (event: any, rowData: any) => {
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
