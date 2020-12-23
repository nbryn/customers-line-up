import {Col, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React from 'react';

import {Card} from '../../components/card/Card';
import {Form} from '../../components/form/Form';
import {REGISTER_URL} from '../../api/URL';
import {RequestHandler, useRequest} from '../../hooks/useRequest';
import {signupValidationSchema} from '../../validation/UserValidation';
import StringUtil from '../../util/StringUtil';
import {TextField} from '../../components/form/TextField';
import TextFieldUtil from '../../util/TextFieldUtil';
import {useForm} from '../../hooks/useForm';
import {UserDTO} from '../../models/User';


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

   const {formHandler} = useForm<UserDTO>(
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
                     onSubmit={formHandler.handleSubmit}
                     buttonText="Signup"
                     working={requestHandler.working}
                     valid={formHandler.isValid}
                     errorMessage={requestHandler.requestInfo}
                  >
                     {Object.keys(formValues).map((key) => (
                        <FormGroup key={key}>
                           <TextField
                              className={styles.textField}
                              id={key}
                              label={StringUtil.capitalizeFirstLetter(key)}
                              type={TextFieldUtil.mapKeyToType(key)}
                              value={formHandler.values[key] as string}
                              onChange={formHandler.handleChange}
                              onBlur={formHandler.handleBlur}
                              error={formHandler.touched[key] && Boolean(formHandler.errors[key])}
                              helperText={formHandler.touched[key] && formHandler.errors[key]}
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
