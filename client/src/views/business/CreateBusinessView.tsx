import {Alert, Button, Badge, Col, Container, Form, Row} from 'react-bootstrap';
import Card from '@material-ui/core/Card';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';

import {CreateBusinessDTO} from '../../models/dto/Business';
import BusinessService from '../../services/BusinessService';
import {
   createBusinessValidationSchema,
   generalCreateBusinessErrorMsg,
} from '../../validation/BusinessValidation';
import {TextField} from '../../components/TextField';
import {useForm} from '../../util/useForm';

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
      height: 700,
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

export const CreateBusinessView: React.FC = () => {
   const styles = useStyles();

   const initialValues: CreateBusinessDTO = {
      name: '',
      zip: 0,
      capacity: 0,
      type: '',
      opens: '',
      closes: '',
   };

   const {formik, errorMessage} = useForm<CreateBusinessDTO>(
      initialValues,
      createBusinessValidationSchema,
      BusinessService.createBusiness,
      generalCreateBusinessErrorMsg
   );

   return (
      <Container>
         <Row className={styles.wrapper}>
            <Col sm={10} lg={6}>
               <Card className={styles.card}>
                  <h1>
                     <Badge className={styles.badge} variant="primary">
                        Create Business
                     </Badge>
                  </h1>

                  <Form onSubmit={formik.handleSubmit}>
                     {errorMessage && (
                        <Alert className={styles.alert} variant="danger">
                           {errorMessage}
                        </Alert>
                     )}
                     <Form.Group>
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
                     </Form.Group>

                     <Form.Group>
                        <TextField
                           className={styles.textField}
                           id="zip"
                           label="Zip"
                           type="text"
                           value={formik.values.zip}
                           onChange={formik.handleChange}
                           onBlur={formik.handleBlur}
                           error={formik.touched.zip && Boolean(formik.errors.zip)}
                           helperText={formik.touched.zip && formik.errors.zip}
                        />
                     </Form.Group>
                     <Form.Group>
                        <TextField
                           className={styles.textField}
                           id="capacity"
                           label="Capacity"
                           type="text"
                           value={formik.values.capacity}
                           onChange={formik.handleChange}
                           onBlur={formik.handleBlur}
                           error={formik.touched.capacity && Boolean(formik.errors.capacity)}
                           helperText={formik.touched.capacity && formik.errors.capacity}
                        />
                     </Form.Group>
                     <Form.Group>
                        <TextField
                           className={styles.textField}
                           id="type"
                           label="Type"
                           type="text"
                           value={formik.values.type}
                           onChange={formik.handleChange}
                           onBlur={formik.handleBlur}
                           error={formik.touched.type && Boolean(formik.errors.type)}
                           helperText={formik.touched.type && formik.errors.type}
                        />
                     </Form.Group>
                     <Form.Group>
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
                     </Form.Group>
                     <Form.Group>
                        <TextField
                           className={styles.textField}
                           id="closes"
                           label="Closes"
                           type="time"
                           defaultValue="04:00"
                           value={formik.values.closes}
                           onChange={formik.handleChange}
                           error={formik.touched.closes && Boolean(formik.errors.closes)}
                           helperText={formik.touched.closes && formik.errors.closes}
                           inputLabelProps={{
                              shrink: true,
                           }}
                           inputProps={{
                              step: 1800,
                           }}
                        />
                     </Form.Group>
                     <div className={styles.buttonGroup}>
                        <Button
                           className={styles.button}
                           variant="primary"
                           type="submit"
                           disabled={!formik.isValid}
                        >
                           Create
                        </Button>
                     </div>
                  </Form>
               </Card>
            </Col>
         </Row>
      </Container>
   );
};
