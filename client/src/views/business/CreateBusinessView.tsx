import {Col, Container, FormGroup, Row} from 'react-bootstrap';
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
import {useForm} from '../../util/useForm';

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

   const formik = useForm<BusinessDTO>(
      initialValues,
      businessValidationSchema,
      requestHandler.mutation,
      CREATE_BUSINESS_URL,
      'POST',
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
                     onSubmit={formik.handleSubmit}
                     buttonText="Create"
                     working={requestHandler.working}
                     valid={formik.isValid}
                  >
                     <Row>
                        <Col sm={6} lg={6}>
                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="name"
                                 label="Name"
                                 type="text"
                                 value={formik.values.name}
                                 onChange={formik.handleChange}
                                 onBlur={formik.handleBlur}
                                 error={formik.touched.name && Boolean(formik.errors.name)}
                                 helperText={formik.touched.name && formik.errors.name}
                              />
                           </FormGroup>

                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="zip"
                                 label="Zip"
                                 type="number"
                                 value={formik.values.zip}
                                 onChange={formik.handleChange}
                                 onBlur={formik.handleBlur}
                                 error={formik.touched.zip && Boolean(formik.errors.zip)}
                                 helperText={formik.touched.zip && formik.errors.zip}
                              />
                           </FormGroup>
                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="capacity"
                                 label="Capacity"
                                 type="number"
                                 value={formik.values.capacity}
                                 onChange={formik.handleChange}
                                 onBlur={formik.handleBlur}
                                 error={formik.touched.capacity && Boolean(formik.errors.capacity)}
                                 helperText={formik.touched.capacity && formik.errors.capacity}
                              />
                           </FormGroup>
                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="type"
                                 label="Type"
                                 type="text"
                                 select
                                 value={formik.values.type}
                                 onChange={formik.handleChange('type')}
                                 onBlur={formik.handleBlur}
                                 error={formik.touched.type && Boolean(formik.errors.type)}
                                 helperText={formik.touched.type && formik.errors.type}
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
                                 value={formik.values.timeSlotLength}
                                 onChange={formik.handleChange}
                                 onBlur={formik.handleBlur}
                                 error={
                                    formik.touched.timeSlotLength &&
                                    Boolean(formik.errors.timeSlotLength)
                                 }
                                 helperText={
                                    formik.touched.timeSlotLength && formik.errors.timeSlotLength
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
                                 value={formik.values.opens}
                                 onChange={formik.handleChange}
                                 onBlur={formik.handleBlur}
                                 error={formik.touched.opens && Boolean(formik.errors.opens)}
                                 helperText={formik.touched.opens && formik.errors.opens}
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
                                 value={formik.values.closes}
                                 onChange={formik.handleChange}
                                 onBlur={formik.handleBlur}
                                 error={formik.touched.closes && Boolean(formik.errors.closes)}
                                 helperText={formik.touched.closes && formik.errors.closes}
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
