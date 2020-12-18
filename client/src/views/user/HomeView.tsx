import {Badge, Col, Container, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React from 'react';

import {useUserContext} from '../../context/UserContext';

const useStyles = makeStyles((theme) => ({
   headline: {
      marginTop: 10,
      textAlign: 'center',
   },
   badge: {},
}));

export const HomeView: React.FC = () => {
   const styles = useStyles();
   const {user} = useUserContext();

   return (
      <Container>
         <Row>
            <Col className={styles.headline}>
               <h1>
                  <Badge variant="primary">Welcome {user?.name}</Badge>
               </h1>
            </Col>
         </Row>
      </Container>
   );
};
