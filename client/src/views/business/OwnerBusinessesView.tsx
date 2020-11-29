import {Badge, Col, Container} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import React, {useEffect, useState} from 'react';

import ApiService from '../../services/ApiService';
import {BusinessDTO} from '../../models/dto/Business';
import BusinessService from '../../services/BusinessService';
import {Table, TableColumn} from '../../components/Table';

import {useUserContext} from '../../context/UserContext';

export const OwnerBusinessesView: React.FC = () => {
   const {user} = useUserContext();

   const [businesses, setBusinesses] = useState<BusinessDTO[]>([]);
   const [errorMessage, setErrorMessage] = useState<string>('');

   useEffect(() => {
      (async () => {
         const businesses: BusinessDTO[] = await ApiService.request(
            () => BusinessService.fetchBusinessesForOwner(user.email),
            setErrorMessage
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
