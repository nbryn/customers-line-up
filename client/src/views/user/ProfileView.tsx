import React, {useEffect, useState} from 'react';
import {Col, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {MenuItem} from '@material-ui/core';
import {useHistory} from 'react-router-dom';

import {Card} from '../../components/card/Card';
import {ComboBox, ComboBoxOption} from '../../components/form/ComboBox';
import {Form} from '../../components/form/Form';
import {TextFieldModal} from '../../components/modal/TextFieldModal';
import {RequestHandler, useRequest} from '../../hooks/useRequest';
import {signupValidationSchema} from '../../validation/UserValidation';
import StringUtil from '../../util/StringUtil';
import {TextField} from '../../components/form/TextField';
import {TextFieldCardRow} from '../../components/card/TextFieldCardRow';
import TextFieldUtil from '../../util/TextFieldUtil';
import {BUSINESS_TYPES_URL, CREATE_BUSINESS_URL} from '../../api/URL';
import {useForm} from '../../hooks/useForm';
import {UserDTO} from '../../models/User';
import {useUserContext} from '../../context/UserContext';

const useStyles = makeStyles((theme) => ({
   card: {
      marginTop: 60,
      borderRadius: 15,
      height: 600,
      textAlign: 'center',
   },
   formGroup: {
      marginBottom: 30,
   },
   helperText: {
      color: 'red',
   },
   textField: {
      width: '75%',
   },
   wrapper: {
      justifyContent: 'center',
   },
}));

const SUCCESS_MESSAGE = 'Business Created - Go to my businesses to see your businesses';

export const ProfileView: React.FC = () => {
   const styles = useStyles();
   const history = useHistory();
   const {user} = useUserContext();

   const [addresses, setAddresses] = useState<ComboBoxOption[]>([]);
   const [zips, setZips] = useState<ComboBoxOption[]>([]);

   const requestHandler: RequestHandler<string[]> = useRequest(SUCCESS_MESSAGE);

   const formValues: UserDTO = {
      email: user.email,
      name: user.name,
      zip: user.zip,
      address: user.address,
   };

   const {addressHandler, formHandler} = useForm<UserDTO>(
      formValues,
      signupValidationSchema,
      CREATE_BUSINESS_URL,
      'POST',
      requestHandler.mutation
   );

   useEffect(() => {
      (async () => {
         setZips(await addressHandler.fetchZips());
      })();
   }, []);

   useEffect(() => {
      (async () => {
         const {zip} = formHandler.values;
         setAddresses(await addressHandler.fetchAddresses(zip?.substring(0, 4)));
      })();
   }, [formHandler.values.zip]);

   return (
      <>
         <Row className={styles.wrapper}>
            <Col sm={6} lg={8}>
               <Card className={styles.card} title="Profile" variant="outlined">
                  <Form
                     onSubmit={formHandler.handleSubmit}
                     buttonText="Create"
                     working={requestHandler.working}
                     valid={formHandler.isValid}
                  >
                     <Row className={styles.wrapper}>
                        <Col sm={6} lg={6}>
                           {Object.keys(formValues).map((key) => {
                              if (key === 'zip' || key === 'address') {
                                 return (
                                    <FormGroup key={key}>
                                       <ComboBox
                                          id={key}
                                          style={{width: '75%', marginLeft: 42, marginTop: 25}}
                                          label={StringUtil.capitalizeFirstLetter(key)}
                                          type="text"
                                          options={key === 'zip' ? zips : addresses}
                                          onBlur={formHandler.handleBlur}
                                          setFieldValue={(option: ComboBoxOption, formFieldId) =>
                                             formHandler.setFieldValue(formFieldId, option.label)
                                          }
                                          error={
                                             formHandler.touched[key] &&
                                             Boolean(formHandler.errors[key])
                                          }
                                          helperText={
                                             formHandler.touched[key] && formHandler.errors[key]
                                          }
                                          defaultLabel={
                                             key === 'address' ? 'Address - After Zip' : ''
                                          }
                                       />
                                    </FormGroup>
                                 );
                              }
                              return (
                                 <FormGroup key={key}>
                                    <TextFieldCardRow 
                                    id={key}
                                    label={StringUtil.capitalizeFirstLetter(key)}
                                    value={formHandler.values[key] as string}
                                    />
                                    {/* <TextField
                                       className={styles.textField}
                                       id={key}
                                       disabled={true}
                                       label={StringUtil.capitalizeFirstLetter(key)}
                                       type={TextFieldUtil.mapKeyToType(key)}
                                       value={formHandler.values[key] as string}
                                       onChange={formHandler.handleChange}
                                       onBlur={formHandler.handleBlur}
                                       error={
                                          formHandler.touched[key] &&
                                          Boolean(formHandler.errors[key])
                                       }
                                       helperText={
                                          formHandler.touched[key] && formHandler.errors[key]
                                       }
                                    /> */}
                                 </FormGroup>
                              );
                           })}
                        </Col>
                     </Row>
                  </Form>
               </Card>
            </Col>
         </Row>
      </>
   );
};
