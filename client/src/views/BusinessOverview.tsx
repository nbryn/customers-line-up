import {Badge, Col, Container} from 'react-bootstrap';
import React, {useEffect, useState} from 'react';

import {BusinessDTO} from '../services/dto/Business';
import BusinessService from '../services/BusinessService';
import CircularProgress from '@material-ui/core/CircularProgress';
import {Table} from '../components/Table';
import {useUserContext} from '../context/UserContext';

export const BusinessOverview: React.FC = () => {
   const {token} = useUserContext();

   const [businesses, setBusinesses] = useState<BusinessDTO[]>([]);

   const columnNames = ['Name', 'Zip', 'Type', 'Opens', 'Closes'];

   useEffect(() => {
      (async () => {
         const businesses = await BusinessService.fetchAllBusinesses(token);

         console.log(businesses);

         setBusinesses(businesses);
      })();
   }, []);

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
                     Businesses
                  </Badge>
               </h1>
               {businesses.length === 0 ? (
                  <CircularProgress />
               ) : (
                  <Table data={businesses} columnNames={columnNames} />
               )}
            </Col>
         </div>
      </Container>
   );
};
