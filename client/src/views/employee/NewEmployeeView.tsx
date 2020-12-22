import {Col, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useEffect, useState} from 'react';
import {useHistory, useLocation} from 'react-router-dom';

import {ALL_USERS_URL} from '../../api/URL';
import {BusinessDTO} from '../../models/Business';
import {Card} from '../../components/card/Card';
import {ComboBox} from '../../components/form/ComboBox';
import {EmployeeDTO} from '../../models/Employee';
import {employeeValidationSchema} from '../../validation/BusinessValidation';
import {Form} from '../../components/form/Form';
import {Modal} from '../../components/modal/Modal';
import {RequestHandler, useRequest} from '../../hooks/useRequest';
import {TextField} from '../../components/form/TextField';
import URL from '../../api/URL';
import {UserDTO} from '../../models/User';
import {useForm} from '../../hooks/useForm';

const useStyles = makeStyles((theme) => ({
   box: {
      textAlign: 'center',
   },
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
      width: '35%',
   },
   wrapper: {
      justifyContent: 'center',
   },
}));

interface LocationState {
   business: BusinessDTO;
}

const SUCCESS_MESSAGE = 'Employee Created - Go to my employees to see your employees';

export const NewEmployeeView: React.FC = () => {
   const styles = useStyles();
   const history = useHistory();
   const location = useLocation<LocationState>();

   const [users, setUsers] = useState<UserDTO[]>([]);
   const [selectedUser, setSelectedUser] = useState<UserDTO>({email: ''});
   const [showComboBox, setShowComBox] = useState(true);

   const requestHandler: RequestHandler<UserDTO[]> = useRequest(SUCCESS_MESSAGE);

   const business = location.state.business;

   useEffect(() => {
      (async () => {
         const users = await requestHandler.query(ALL_USERS_URL);

         setUsers(users);
      })();
   }, []);

   useEffect(() => {
      if (users.find((user) => user.email === selectedUser.email)) {
         setShowComBox(false);
      }
   }, [selectedUser]);

   const formValues: EmployeeDTO = {
      id: 1,
      companyEmail: '',
   };

   const form = useForm<EmployeeDTO>(
      formValues,
      employeeValidationSchema,
      URL.getNewEmployeeURL(business.id),
      'POST',
      requestHandler.mutation
   );

   return (
      <>
         <Row className={styles.wrapper}>
            <Col sm={6} lg={6}>
               <Modal
                  show={requestHandler.requestInfo ? true : false}
                  title="Employee Info"
                  text={requestHandler.requestInfo}
                  primaryAction={() => history.push('/business/employees')}
                  primaryActionText="My Employees"
                  secondaryAction={() => requestHandler.setRequestInfo('')}
               />
               <Card
                  className={styles.card}
                  title="New Employee"
                  subTitle="Choose an existing user you want add as an employee"
                  variant="outlined"
               >
                  {showComboBox && (
                     <ComboBox label="Email" options={users} setChosenValue={setSelectedUser} />
                  )}
                  <Form
                     onSubmit={form.handleSubmit}
                     buttonText="Create"
                     working={requestHandler.working}
                     valid={form.isValid}
                  >
                     {!showComboBox && (
                        <>
                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="name"
                                 label="Name"
                                 type="Text"
                                 value={selectedUser.name}
                                 disabled={true}
                              />
                           </FormGroup>
                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="privateEmail"
                                 label="Private Email"
                                 type="email"
                                 value={selectedUser.email}
                                 disabled={true}
                              />
                           </FormGroup>
                           <FormGroup className={styles.formGroup}>
                              <TextField
                                 className={styles.textField}
                                 id="companyEmail"
                                 label="Company Email"
                                 type="Email"
                                 value={form.values.companyEmail}
                                 onChange={form.handleChange}
                                 onBlur={form.handleBlur}
                                 error={
                                    form.touched.companyEmail && Boolean(form.errors.companyEmail)
                                 }
                                 helperText={form.touched.companyEmail && form.errors.companyEmail}
                              />
                           </FormGroup>
                        </>
                     )}
                  </Form>
               </Card>
            </Col>
         </Row>
      </>
   );
};
