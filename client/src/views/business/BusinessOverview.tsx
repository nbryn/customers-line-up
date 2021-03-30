import {Col, Row} from 'react-bootstrap';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';
import {useHistory} from 'react-router-dom';

import {BusinessCard} from '../../components/card/BusinessCard';
import {BusinessDTO} from '../../models/Business';
import PathUtil, {PathInfo} from '../../util/PathUtil';
import {Header} from '../../components/Texts';
import {useBusinessService} from '../../services/BusinessService';

const useStyles = makeStyles((theme) => ({
   row: {
      justifyContent: 'center',
   },
}));

export const BusinessOverview: React.FC = () => {
   const styles = useStyles();
   const history = useHistory();

   const [businessData, setBusinessData] = useState<BusinessDTO[]>([]);

   const pathInfo: PathInfo = PathUtil.getPathAndTextFromURL(window.location.pathname);

   const businessService = useBusinessService();

   useEffect(() => {
      (async () => {
         const businesses: BusinessDTO[] = await businessService.fetchBusinesssesByOwner();

         setBusinessData(businesses);
      })();
   }, []);

   return (
      <>
         <Row className={styles.row}>
            <Header text="Choose Business" />
         </Row>
         <Row className={styles.row}>
            {businessService.working && <CircularProgress />}
            <>
               {businessData.map((x) => {
                  localStorage.setItem('business', JSON.stringify(x));
                  return (
                     <Col key={x.id} sm={6} md={8} lg={4}>
                        <BusinessCard
                           business={x}
                           buttonText={pathInfo.primaryButtonText}
                           buttonAction={() =>
                              history.push(`/business/${pathInfo.primaryPath}`, {
                                 business: x,
                              })
                           }
                           secondaryButtonText={pathInfo.secondaryButtonText}
                           secondaryAction={() =>
                              history.push(`/business/${pathInfo.secondaryPath}`, {
                                 business: x,
                              })
                           }
                        />
                     </Col>
                  );
               })}
            </>
         </Row>
      </>
   );
};
