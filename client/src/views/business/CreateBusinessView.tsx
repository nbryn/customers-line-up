import {Col, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {MenuItem} from '@material-ui/core';
import React, {useEffect, useState} from 'react';
import {useHistory} from 'react-router-dom';

import {Card} from '../../components/Card';
import {BusinessDTO} from '../../dto/Business';
import {businessValidationSchema} from '../../validation/BusinessValidation';
import {Form} from '../../components/Form';
import {Modal} from '../../components/Modal';
import {RequestHandler, useRequest} from '../../api/RequestHandler';
import {TextField} from '../../components/TextField';
import {BUSINESS_TYPES_URL, CREATE_BUSINESS_URL} from '../../api/URL';
import {useForm} from '../../validation/useForm';

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
      width: '50%',
   },
   wrapper: {
      justifyContent: 'center',
   },
}));

const SUCCESS_MESSAGE = 'Business Created - Go to my businesses to see your businesses';

export const CreateBusinessView: React.FC = () => {
   const styles = useStyles();
   const history = useHistory();

   const [businessTypes, setBusinessTypes] = useState<string[]>([]);

   const requestHandler: RequestHandler<string[]> = useRequest(SUCCESS_MESSAGE);

   useEffect(() => {
      (async () => {
         const types = await requestHandler.query(BUSINESS_TYPES_URL);

         setBusinessTypes(types);
      })();
   }, []);

   const initialValues: BusinessDTO = {
      id: 0,
      name: '',
      zip: '',
      capacity: '',
      type: '',
      timeSlotLength: '',
      opens: '',
      closes: '',
   };

   const form = useForm<BusinessDTO>(
      initialValues,
      businessValidationSchema,
      CREATE_BUSINESS_URL,
      'POST',
      requestHandler.mutation,   
      (business) => {
         business.opens = business.opens.replace(':', '.');
         business.closes = business.closes.replace(':', '.');

         return business;
      }
   );

   return (
      <>
         <Row className={styles.wrapper}>
            <Col sm={6} lg={6}>
               <Modal
                  show={requestHandler.requestInfo ? true : false}
                  title="Business Info"
                  text={requestHandler.requestInfo}
                  primaryAction={() => history.push('/mybusinesses')}
                  primaryActionText="My Businesses"
                  secondaryAction={() => requestHandler.setRequestInfo('')}
               />
               <Card className={styles.card} title="Create Business" variant="outlined">
                  <Form
                     onSubmit={form.handleSubmit}
                     buttonText="Create"
                     working={requestHandler.working}
                     valid={form.isValid}
                  >
                     <Row>
                        <Col sm={6} lg={6}>
                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="name"
                                 label="Name"
                                 type="text"
                                 value={form.values.name}
                                 onChange={form.handleChange}
                                 onBlur={form.handleBlur}
                                 error={form.touched.name && Boolean(form.errors.name)}
                                 helperText={form.touched.name && form.errors.name}
                              />
                           </FormGroup>

                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="zip"
                                 label="Zip"
                                 type="number"
                                 value={form.values.zip}
                                 onChange={form.handleChange}
                                 onBlur={form.handleBlur}
                                 error={form.touched.zip && Boolean(form.errors.zip)}
                                 helperText={form.touched.zip && form.errors.zip}
                              />
                           </FormGroup>
                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="capacity"
                                 label="Capacity"
                                 type="number"
                                 value={form.values.capacity}
                                 onChange={form.handleChange}
                                 onBlur={form.handleBlur}
                                 error={form.touched.capacity && Boolean(form.errors.capacity)}
                                 helperText={form.touched.capacity && form.errors.capacity}
                              />
                           </FormGroup>
                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="type"
                                 label="Type"
                                 type="text"
                                 select
                                 value={form.values.type}
                                 onChange={form.handleChange('type')}
                                 onBlur={form.handleBlur}
                                 error={form.touched.type && Boolean(form.errors.type)}
                                 helperText={form.touched.type && form.errors.type}
                              >
                                 {businessTypes.map((type) => (
                                    <MenuItem key={type} value={type}>
                                       {type}
                                    </MenuItem>
                                 ))}
                              </TextField>
                           </FormGroup>
                        </Col>
                        <Col sm={6} lg={6}>
                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="timeSlotLength"
                                 label="Visit Length"
                                 type="number"
                                 value={form.values.timeSlotLength}
                                 onChange={form.handleChange}
                                 onBlur={form.handleBlur}
                                 error={
                                    form.touched.timeSlotLength &&
                                    Boolean(form.errors.timeSlotLength)
                                 }
                                 helperText={
                                    form.touched.timeSlotLength && form.errors.timeSlotLength
                                 }
                              />
                           </FormGroup>
                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="opens"
                                 label="Opens"
                                 type="time"
                                 defaultValue="08:00"
                                 value={form.values.opens}
                                 onChange={form.handleChange}
                                 onBlur={form.handleBlur}
                                 error={form.touched.opens && Boolean(form.errors.opens)}
                                 helperText={form.touched.opens && form.errors.opens}
                                 inputLabelProps={{
                                    shrink: true,
                                 }}
                                 inputProps={{
                                    step: 1800,
                                 }}
                              />
                           </FormGroup>
                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="closes"
                                 label="Closes"
                                 type="time"
                                 defaultValue="04:00"
                                 value={form.values.closes}
                                 onChange={form.handleChange}
                                 onBlur={form.handleBlur}
                                 error={form.touched.closes && Boolean(form.errors.closes)}
                                 helperText={form.touched.closes && form.errors.closes}
                                 inputLabelProps={{
                                    shrink: true,
                                 }}
                                 inputProps={{
                                    step: 1800,
                                 }}
                              />
                           </FormGroup>
                        </Col>
                     </Row>
                  </Form>
               </Card>
            </Col>
         </Row>
      </>
   );
};
