import {Alert, Button, Badge, Col, Container, Form, Row} from 'react-bootstrap';
import Card from '@material-ui/core/Card';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';

import {BusinessDTO} from '../services/dto/Business';
import BusinessService from '../services/BusinessService';
import {TextField} from '../components/TextField';

const useStyles = makeStyles((theme) => ({
   alert: {
      display: 'inline-block',
      marginTop: -20,
      marginBottom: 40,
      maxWidth: 380,
   },
   badge: {
      marginBottom: 40,
      marginTop: 25,
      width: '50%',
   },
   button: {
      marginTop: 25,
      marginBottom: -15,
      width: '45%',
   },
   buttonGroup: {
      marginTop: 35,
      marginBottom: -15,
      width: '100%',
   },
   card: {
      marginTop: 60,
      borderRadius: 15,
      height: 700,
      boxShadow: '0px 0px 0px 8px rgba(12, 12, 242, 0.1)',
      textAlign: 'center',
   },
   helperText: {
      color: 'red',
   },
   textField: {
      width: '35%',
   },
   wrapper: {
      justifyContent: 'center',
   },
}));

export const CreateBusinessView: React.FC = () => {
   const styles = useStyles();

   const [name, setName] = useState<string>('');
   const [zip, setZip] = useState<string>('');
   const [capacity, setCapacity] = useState<string>('');
   const [type, setType] = useState<string>('');
   const [opens, setOpens] = useState<string>('');
   const [closes, setCloses] = useState<string>('');
   const [errorMsg, setErrorMsg] = useState<string>('');

   const handleSubmit = async (event: React.FormEvent) => {
      try {
         event.preventDefault();

         const business: BusinessDTO = {
            name,
            zip,
            capacity,
            type,
            opens: opens.replace(':', '.'),
            closes: closes.replace(':', '.'),
         };

         await BusinessService.createBusiness(business);
      } catch (err) {
         setErrorMsg('Wrong email/password combination');
      }
   };

   return (
      <Container>
         <Row className={styles.wrapper}>
            <Col sm={10} lg={6}>
               <Card className={styles.card}>
                  <h1>
                     <Badge className={styles.badge} variant="primary">
                        Create Business
                     </Badge>
                  </h1>

                  <Form onSubmit={handleSubmit}>
                     {errorMsg && (
                        <Alert className={styles.alert} variant="danger">
                           {errorMsg}
                        </Alert>
                     )}
                     <Form.Group>
                        <TextField
                           className={styles.textField}
                           id="name"
                           label="Name"
                           type="text"
                           onChange={(e) => setName(e.target.value)}
                           value={name}
                        />
                     </Form.Group>

                     <Form.Group>
                        <TextField
                           className={styles.textField}
                           id="zip"
                           label="Zip"
                           type="text"
                           onChange={(e) => setZip(e.target.value)}
                           value={zip}
                        />
                     </Form.Group>
                     <Form.Group>
                        <TextField
                           className={styles.textField}
                           id="capacity"
                           label="Capacity"
                           type="text"
                           onChange={(e) => setCapacity(e.target.value)}
                           value={capacity}
                        />
                     </Form.Group>
                     <Form.Group>
                        <TextField
                           className={styles.textField}
                           id="type"
                           label="Type"
                           type="text"
                           onChange={(e) => setType(e.target.value)}
                           value={type}
                        />
                     </Form.Group>
                     <Form.Group>
                        <TextField
                           className={styles.textField}
                           id="opens"
                           label="Opens"
                           type="time"
                           defaultValue="08:00"
                           onChange={(e) => setOpens(e.target.value)}
                           value={opens}
                           inputLabelProps={{
                              shrink: true,
                           }}
                           inputProps={{
                              step: 1800,
                           }}
                        />
                     </Form.Group>
                     <Form.Group>
                        <TextField
                           className={styles.textField}
                           id="closes"
                           label="Closes"
                           type="time"
                           defaultValue="04:00"
                           onChange={(e) => setCloses(e.target.value)}
                           value={closes}
                           inputLabelProps={{
                              shrink: true,
                           }}
                           inputProps={{
                              step: 1800,
                           }}
                        />
                     </Form.Group>
                     <div className={styles.buttonGroup}>
                        <Button
                           className={styles.button}
                           variant="primary"
                           type="submit"
                           //disabled={!(email && password)}
                        >
                           Create
                        </Button>
                     </div>
                  </Form>
               </Card>
            </Col>
         </Row>
      </Container>
   );
};
