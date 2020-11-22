import Alert from 'react-bootstrap/Alert';
import Badge from 'react-bootstrap/Badge';
import Button from 'react-bootstrap/Button';
import Card from '@material-ui/core/Card';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';

import {TextField} from '../components/TextField';

const useStyles = makeStyles((theme) => ({
   alert: {
      display: 'inline-block',
      marginTop: -20,
      marginBottom: 40,
      maxWidth: 380,
   },
   badge: {
      marginBottom: 30,
      marginTop: 30,
      width: '45%',
   },
   button: {
      marginTop: 45,
      width: '30%',
   },
   card: {
      marginTop: 70,
      height: 650,
      width: 550,
      borderRadius: 15,
      boxShadow: '0px 0px 0px 8px rgba(12, 12, 242, 0.1)',
      textAlign: 'center',
      display: 'inline-block',
   },
   helperText: {
      color: 'red',
   },
   textField: {
      width: '35.5%',
   },
}));

export const SignupView: React.FC = () => {
   const styles = useStyles();

   const [email, setEmail] = useState('');
   const [emailError, setEmailError] = useState('');

   const [name, setName] = useState('');
   const [nameError, setNameError] = useState('');

   const [zip, setZip] = useState('');
   const [zipError, setZipError] = useState('');

   const [password, setPassword] = useState('');
   const [passwordError, setPassWordError] = useState('');

   const [errorMsg, setErrorMsg] = useState('');

   const handleSubmit = async (event: React.FormEvent) => {
      try {
         event.preventDefault();
      } catch (err) {
         const errors = err.getErrors();

         setEmailError(errors.get('email') || '');
         setNameError(errors.get('name') || '');
         setZipError(errors.get('zip') || '');
         setPassWordError(errors.get('password') || '');

         setErrorMsg('An Error Occured');
      }
   };
   return (
      <Container fluid>
         <Card className={styles.card}>
            <h1>
               <Badge className={styles.badge} variant="primary">
                  Signup
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
                     id="email"
                     label="Email"
                     onChange={(e) => setEmail(e.target.value)}
                     value={email}
                     helperText={emailError}
                     formHelperTextProps={{className: styles.helperText}}
                  />
               </Form.Group>

               <Form.Group>
                  <TextField
                     className={styles.textField}
                     id="name"
                     label="Name"
                     onChange={(e) => setName(e.target.value)}
                     value={name}
                     helperText={nameError}
                     formHelperTextProps={{className: styles.helperText}}
                  />
               </Form.Group>

               <Form.Group>
                  <TextField
                     className={styles.textField}
                     id="zip"
                     label="Zip"
                     onChange={(e) => setZip(e.target.value)}
                     value={zip}
                     helperText={zipError}
                     formHelperTextProps={{className: styles.helperText}}
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
                     helperText={passwordError}
                     formHelperTextProps={{className: styles.helperText}}
                  />
               </Form.Group>

               <Button
                  className={styles.button}
                  variant="primary"
                  type="submit"
                  disabled={!(email && name && zip && password)}
               >
                  Go!
               </Button>
            </Form>
         </Card>
      </Container>
   );
};
