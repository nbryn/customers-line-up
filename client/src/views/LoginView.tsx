import {Alert, Button, Badge, Col, Container, Form, Row} from 'react-bootstrap';
import Card from '@material-ui/core/Card';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';

import ApiService from '../services/ApiService';
import {SignupView} from './SignupView';
import {TextField} from '../components/TextField';
import {UserDTO} from '../models/dto/User';
import UserService from '../services/UserService';
import {useUserContext} from '../context/UserContext';

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
      height: 450,
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

export const LoginView: React.FC = () => {
   const styles = useStyles();
   const {setUser} = useUserContext();

   const [renderSignUp, setRenderSignUp] = useState(false);

   const [email, setEmail] = useState('');
   const [password, setPassword] = useState('');
   const [errorMessage, setErrorMessage] = useState('');

   const handleSubmit = async (event: React.FormEvent) => {
      event.preventDefault();

      const user: UserDTO = await ApiService.request(
         () => UserService.login({email, password}),
         setErrorMessage
      );

      setUser(user);
   };

   return (
      <Container>
         {renderSignUp ? (
            <SignupView />
         ) : (
            <Row className={styles.wrapper}>
               <Col sm={10} lg={6}>
                  <Card className={styles.card}>
                     <h1>
                        <Badge className={styles.badge} variant="primary">
                           Login
                        </Badge>
                     </h1>

                     <Form onSubmit={handleSubmit}>
                        {errorMessage && (
                           <Alert className={styles.alert} variant="danger">
                              {errorMessage}
                           </Alert>
                        )}
                        <Form.Group>
                           <TextField
                              className={styles.textField}
                              id="email"
                              label="Email"
                              onChange={(e) => setEmail(e.target.value)}
                              value={email}
                           />
                        </Form.Group>

                        <Form.Group>
                           <TextField
                              className={styles.textField}
                              id="password"
                              label="Password"
                              type="password"
                              onChange={(e) => setPassword(e.target.value)}
                              value={password}
                           />
                        </Form.Group>
                        <div className={styles.buttonGroup}>
                           <Button
                              className={styles.button}
                              variant="primary"
                              type="submit"
                              disabled={!(email && password)}
                           >
                              Login
                           </Button>
                           <br />
                           <Button
                              onClick={() => setRenderSignUp(true)}
                              className={styles.button}
                              variant="secondary"
                           >
                              Signup
                           </Button>
                        </div>
                     </Form>
                  </Card>
               </Col>
            </Row>
         )}
      </Container>
   );
};
