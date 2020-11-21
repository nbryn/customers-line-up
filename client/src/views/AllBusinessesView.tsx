import {Badge, Col, Container} from 'react-bootstrap';
import React, {useEffect, useState} from 'react';

import {BusinessDTO} from '../services/dto/Business';
import BusinessService from '../services/BusinessService';
import {BusinessTable} from '../components/BusinessTable';
import CircularProgress from '@material-ui/core/CircularProgress';

export const AllBusinessesView: React.FC = () => {
   const [businesses, setBusinesses] = useState<BusinessDTO[]>([]);

   useEffect(() => {
      (async () => {
         const businesses: BusinessDTO[] = await BusinessService.fetchAllBusinesses();

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
                     Available Businesses
                  </Badge>
               </h1>
               {businesses.length === 0 ? <CircularProgress /> : <BusinessTable data={businesses} />}
            </Col>
         </div>
      </Container>
   );
};
