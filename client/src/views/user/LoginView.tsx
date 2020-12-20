import {Col, Container, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';

import {Card} from '../../components/Card';
import {Form} from '../../components/Form';
import {LOGIN_URL} from '../../api/URL';
import {loginValidationSchema} from '../../validation/UserValidation';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {SignupView} from './SignupView';
import StringUtil from '../../util/StringUtil';
import {TextField} from '../../components/TextField';
import TextFieldUtil from '../../util/TextFieldUtil';
import {useForm} from '../../validation/useForm';
import {UserDTO} from '../../dto/User';
import {useUserContext} from '../../context/UserContext';

const useStyles = makeStyles((theme) => ({
   card: {
      marginTop: 60,
      borderRadius: 15,
      height: 450,
      //boxShadow: '0px 0px 0px 8px rgba(12, 12, 242, 0.1)',
      textAlign: 'center',
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

   const requestHandler: RequestHandler<UserDTO> = useRequest();

   const formValues: UserDTO = {
      email: '',
      password: '',
   };

   const form = useForm<UserDTO>(
      formValues,
      loginValidationSchema,
      LOGIN_URL,
      'POST',
      requestHandler.mutation,
      undefined,
      setUser
   );

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
                        onSubmit={form.handleSubmit}
                        buttonText="Login"
                        working={requestHandler.working}
                        valid={form.isValid}
                        errorMessage={requestHandler.requestInfo ? 'Wrong Email/Password' : ''}
                     >
                        {Object.keys(formValues).map((key) => (
                           <FormGroup key={key}>
                              <TextField
                                 className={styles.textField}
                                 id={key}
                                 label={StringUtil.capitalizeFirstLetter(key)}
                                 type={TextFieldUtil.mapKeyToType(key)}
                                 value={form.values[key] as string}
                                 onChange={form.handleChange}
                                 onBlur={form.handleBlur}
                                 error={form.touched[key] && Boolean(form.errors[key])}
                                 helperText={form.touched[key] && form.errors[key]}
                              />
                           </FormGroup>
                        ))}
                     </Form>
                  </Card>
               </Col>
            </Row>
         )}
      </Container>
   );
};
