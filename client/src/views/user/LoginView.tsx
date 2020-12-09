import {Col, Container, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';

import {Card} from '../../components/Card';
import {Form} from '../../components/Form';
import {SignupView} from './SignupView';
import {TextField} from '../../components/TextField';
import {UserDTO} from '../../models/dto/User';
import UserService from '../../services/UserService';
import {useUserContext} from '../../context/UserContext';

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
      //boxShadow: '0px 0px 0px 8px rgba(12, 12, 242, 0.1)',
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
   const [working, setWorking] = useState(false);
   const [errorMessage, setErrorMessage] = useState('');

   const handleSubmit = async (event: React.FormEvent) => {
      try {
         event.preventDefault();
         setWorking(true);
         setErrorMessage('');

         const user: UserDTO = await UserService.login(email, password);

         setUser(user);
      } catch (err) {
         setErrorMessage('Wrong Email/Password');
      } finally {
         setWorking(false);
      }
   };

   return (
      <Container>
         {renderSignUp ? (
            <SignupView />
         ) : (
            <Row className={styles.wrapper}>
               <Col sm={10} lg={6}>
                  <Card
                     className={styles.card}
                     title="Login"
                     buttonAction={() => setRenderSignUp(true)}
                     buttonColor="secondary"
                     buttonText="Signup"
                     buttonSize="medium"
                     variant="outlined"
                  >
                     <Form
                        onSubmit={handleSubmit}
                        buttonText="Login"
                        working={working}
                        errorMessage={errorMessage}
                        valid={Boolean(email && password)}
                     >
                        <FormGroup>
                           <TextField
                              className={styles.textField}
                              id="email"
                              label="Email"
                              onChange={(e) => setEmail(e.target.value)}
                              value={email}
                           />
                        </FormGroup>

                        <FormGroup>
                           <TextField
                              className={styles.textField}
                              id="password"
                              label="Password"
                              type="password"
                              onChange={(e) => setPassword(e.target.value)}
                              value={password}
                           />
                        </FormGroup>
                     </Form>
                  </Card>
               </Col>
            </Row>
         )}
      </Container>
   );
};
