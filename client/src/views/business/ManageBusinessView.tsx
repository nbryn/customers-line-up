import {Col, Container, Row} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';
import {useLocation} from 'react-router-dom';

import {BUSINESSES_OWNER_URL} from '../../api/URL';
import {BusinessDTO} from '../../models/dto/Business';
import {ExtendedBusinessCard} from '../../components/ExtendedBusinessCard';
import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../api/RequestHandler';

const useStyles = makeStyles((theme) => ({
   row: {
      justifyContent: 'center',
   },
}));

interface LocationState {
   business: BusinessDTO;
}

export const ManageBusinessView: React.FC = () => {
   const styles = useStyles();
   const location = useLocation<LocationState>();

   const requestHandler: RequestHandler<BusinessDTO[]> = useRequest();

   const business = location.state.business;

   return (
      <Container>
         <Row className={styles.row}>
            <Header text={`Manage ${business.name}`} />
         </Row>
         <Row className={styles.row}>
            <Col sm={12} md={8} lg={6}>
               <ExtendedBusinessCard business={business} />
            </Col>
         </Row>
      </Container>
   );
};
