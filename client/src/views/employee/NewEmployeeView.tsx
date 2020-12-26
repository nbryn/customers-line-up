import React, {useEffect, useState} from 'react';
import {Col, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory, useLocation} from 'react-router-dom';

import {BusinessDTO} from '../../models/Business';
import {Card} from '../../components/card/Card';
import {ComboBox} from '../../components/form/ComboBox';
import {NewEmployeeDTO} from '../../models/Employee';
import {employeeValidationSchema} from '../../validation/BusinessValidation';
import {Form} from '../../components/form/Form';
import {Modal} from '../../components/modal/Modal';
import {RequestHandler, useRequest} from '../../hooks/useRequest';
import {TextField} from '../../components/form/TextField';
import URL, {NEW_EMPLOYEE_URL} from '../../api/URL';
import {UserDTO} from '../../models/User';
import {useForm} from '../../hooks/useForm';

const useStyles = makeStyles((theme) => ({
   card: {
      marginTop: 60,
      borderRadius: 15,
      height: 600,
      textAlign: 'center',
   },
   comboBox: {
      marginTop: 10,
      marginLeft: 110,
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

   const {business} = location.state;

   const formValues: NewEmployeeDTO = {
      companyEmail: '',
   };

   const {formHandler, ...form} = useForm<NewEmployeeDTO>(
      formValues,
      employeeValidationSchema,
      NEW_EMPLOYEE_URL,
      'POST',
      requestHandler.mutation
   );

   useEffect(() => {
      (async () => {
         const users = await requestHandler.query(
            URL.getAllUsersNotAlreadyEmployedURL(business.id)
         );

         setUsers(users);
      })();
   }, []);

   useEffect(() => {
      if (users.find((user) => user.email === selectedUser.email)) {
         setShowComBox(false);
      }
   }, [selectedUser]);

   return (
      <>
         <Row className={styles.wrapper}>
            <Col sm={6} lg={6}>
               <Modal
                  show={requestHandler.requestInfo ? true : false}
                  title="Employee Info"
                  text={requestHandler.requestInfo}
                  primaryAction={() => history.push('/business/employees/manage')}
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
                     <ComboBox
                        style={styles.comboBox}
                        label="Email"
                        options={users}
                        setChosenValue={setSelectedUser}
                     />
                  )}
                  {!showComboBox && (
                     <>
                        <Form
                           style={{marginTop: 20}}
                           onSubmit={(e: React.FormEvent) => {
                              e.preventDefault();
                              form.setRequest({
                                 businessId: business.id,
                                 privateEmail: selectedUser.email,
                                 companyEmail: formHandler.values.companyEmail,
                              });

                              formHandler.handleSubmit();
                           }}
                           buttonText="Create"
                           working={requestHandler.working}
                           valid={formHandler.isValid}
                        >
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
                                 value={formHandler.values.companyEmail}
                                 onChange={formHandler.handleChange}
                                 onBlur={formHandler.handleBlur}
                                 error={
                                    formHandler.touched.companyEmail &&
                                    Boolean(formHandler.errors.companyEmail)
                                 }
                                 helperText={
                                    formHandler.touched.companyEmail &&
                                    formHandler.errors.companyEmail
                                 }
                              />
                           </FormGroup>
                        </Form>
                     </>
                  )}
               </Card>
            </Col>
         </Row>
      </>
   );
};
