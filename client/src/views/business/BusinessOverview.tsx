import {Col, Container, Row} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';
import {useHistory} from 'react-router-dom';

import {BUSINESSES_OWNER_URL} from '../../api/URL';
import {BusinessDTO} from '../../models/dto/Business';
import {SimpleBusinessCard} from '../../components/SimpleBusinessCard';
import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../api/RequestHandler';

const useStyles = makeStyles((theme) => ({
   row: {
      justifyContent: 'center',
   },
}));

export const BusinessOverview: React.FC = () => {
   const styles = useStyles();
   const history = useHistory();

   const [businessData, setBusinessData] = useState<BusinessDTO[]>([]);

   const requestHandler: RequestHandler<BusinessDTO[]> = useRequest();

   useEffect(() => {
      (async () => {
         const data: BusinessDTO[] = await requestHandler.query(BUSINESSES_OWNER_URL);

         setBusinessData(data);
      })();
   }, []);

   return (
      <Container>
         <Row className={styles.row}>
            <Header text="Your Businesses" />
         </Row>
         <Row className={styles.row}>
            {requestHandler.working && <CircularProgress />}
            <>
               {businessData.map((x) => {
                  return (
                     <Col key={x.id} sm={6} md={8} lg={4}>
                        <SimpleBusinessCard
                           data={{
                              id: x.id,
                              name: x.name,
                              type: x.type,
                              zip: x.zip as string,
                              capacity: x.capacity as number,
                              businessHours: x.businessHours as string,
                           }}
                           buttonText="Manage Business"
                           buttonAction={(businessId) =>
                              history.push('/business/manage', {
                                 business: x,
                              })
                           }
                        />
                     </Col>
                  );
               })}
            </>
         </Row>
      </Container>
   );
};
