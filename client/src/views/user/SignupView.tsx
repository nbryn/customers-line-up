import {Col, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React from 'react';

import {Card} from '../../components/Card';
import {Form} from '../../components/Form';
import {REGISTER_URL} from '../../api/URL';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {signupValidationSchema} from '../../validation/UserValidation';
import StringUtil from '../../util/StringUtil';
import {TextField} from '../../components/TextField';
import TextFieldUtil from '../../util/TextFieldUtil';
import {useForm} from '../../validation/useForm';
import {UserDTO} from '../../dto/User';


import {useUserContext} from '../../context/UserContext';

const useStyles = makeStyles((theme) => ({
   card: {
      marginTop: 60,
      height: 550,
      borderRadius: 15,
      //boxShadow: '0px 0px 0px 8px rgba(12, 12, 242, 0.1)',
      textAlign: 'center',
   },
   helperText: {
      color: 'red',
   },
   textField: {
      width: '35.5%',
   },
   wrapper: {
      justifyContent: 'center',
   },
}));

export const SignupView: React.FC = () => {
   const styles = useStyles();
   const {setUser} = useUserContext();

   const requestHandler: RequestHandler<UserDTO> = useRequest();

   const formValues: UserDTO = {
      email: '',
      name: '',
      zip: '',
      password: '',
   };

   const form = useForm<UserDTO>(
      formValues,
      signupValidationSchema,
      REGISTER_URL,
      'POST',
      requestHandler.mutation,
      undefined,
      setUser
   );

   return (
      <>
         <Row className={styles.wrapper}>
            <Col sm={10} lg={6}>
               <Card className={styles.card} title="Signup">
                  <Form
                     onSubmit={form.handleSubmit}
                     buttonText="Signup"
                     working={requestHandler.working}
                     valid={form.isValid}
                     errorMessage={requestHandler.requestInfo}
                  >
                     {Object.keys(formValues).map((key) => (
                        <FormGroup key={key}>
                           <TextField
                              className={styles.textField}
                              id={key}
                              label={StringUtil.capitalizeFirstLetter(key)}
                              type={TextFieldUtil.getTextFieldTypeFromKey(key)}
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
      </>
   );
};
