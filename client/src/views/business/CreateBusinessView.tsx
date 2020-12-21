import {Col, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {MenuItem} from '@material-ui/core';
import React, {useEffect, useState} from 'react';
import {useHistory} from 'react-router-dom';

import {Card} from '../../components/card/Card';
import {BusinessDTO} from '../../models/Business';
import {businessValidationSchema} from '../../validation/BusinessValidation';
import {Form} from '../../components/form/Form';
import {Modal} from '../../components/modal/Modal';
import {RequestHandler, useRequest} from '../../hooks/useRequest';
import {TextField} from '../../components/form/TextField';
import TextFieldUtil from '../../util/TextFieldUtil';
import {BUSINESS_TYPES_URL, CREATE_BUSINESS_URL} from '../../api/URL';
import {useForm} from '../../hooks/useForm';

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

   const formValues: BusinessDTO = {
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
      formValues,
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
                           {Object.keys(formValues)
                              .slice(1, 5)
                              .map((x) => (
                                 <FormGroup className={styles.formGroup}>
                                    <TextField
                                       className={styles.textField}
                                       id={x}
                                       label={TextFieldUtil.mapKeyToLabel(x)}
                                       type={TextFieldUtil.mapKeyToType(x)}
                                       value={form.values[x]}
                                       onChange={form.handleChange}
                                       onBlur={form.handleBlur}
                                       error={form.touched[x] && Boolean(form.errors[x])}
                                       helperText={form.touched[x] && form.errors[x]}
                                    >
                                       {x === 'type' &&
                                          businessTypes.map((type) => (
                                             <MenuItem key={type} value={type}>
                                                {type}
                                             </MenuItem>
                                          ))}
                                    </TextField>
                                 </FormGroup>
                              ))}
                        </Col>
                        <Col sm={6} lg={6}>
                           {Object.keys(formValues)
                              .slice(5)
                              .map((x) => (
                                 <FormGroup className={styles.formGroup}>
                                    <TextField
                                       className={styles.textField}
                                       id={x}
                                       label={TextFieldUtil.mapKeyToLabel(x)}
                                       type={TextFieldUtil.mapKeyToType(x)}
                                       value={form.values[x]}
                                       onChange={form.handleChange}
                                       onBlur={form.handleBlur}
                                       error={form.touched[x] && Boolean(form.errors[x])}
                                       helperText={form.touched[x] && form.errors[x]}
                                       inputLabelProps={
                                          x === 'timeSlotLength' ? {shrink: false} : {shrink: true}
                                       }
                                       inputProps={{
                                          step: 1800,
                                       }}
                                    />
                                 </FormGroup>
                              ))}
                        </Col>
                     </Row>
                  </Form>
               </Card>
            </Col>
         </Row>
      </>
   );
};
