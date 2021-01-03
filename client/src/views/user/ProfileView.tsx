import React, {useEffect, useState} from 'react';
import {pick} from 'lodash-es';
import {Col, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory} from 'react-router-dom';

import {Card} from '../../components/card/Card';
import {ComboBox, ComboBoxOption} from '../../components/form/ComboBox';
import {Form} from '../../components/form/Form';
import {FormCard, FormCardData} from '../../components/card/FormCard';
import {Header} from '../../components/Texts';
import {RequestHandler, useRequest} from '../../hooks/useRequest';
import {signupValidationSchema} from '../../validation/UserValidation';
import StringUtil from '../../util/StringUtil';
import {TextFieldCardRow} from '../../components/card/TextFieldCardRow';
import {TextFieldModal} from '../../components/modal/TextFieldModal';
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
   header: {
      justifyContent: 'center',
      marginBottom: 20,
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

   const [modalKey, setModalKey] = useState('');
   const [addresses, setAddresses] = useState<ComboBoxOption[]>([]);
   const [zips, setZips] = useState<ComboBoxOption[]>([]);

   const requestHandler: RequestHandler<string[]> = useRequest(SUCCESS_MESSAGE);

   const formValues = pick(user, ['name', 'email', 'zip', 'address']);

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

   const profileData: FormCardData[] = Object.keys(formValues).map((key) => ({
      key,
      label: StringUtil.capitalizeFirstLetter(key),
      type: TextFieldUtil.mapKeyToType(key),
      value: formHandler.values[key] as string,
      buttonAction: () => setModalKey(key),
      buttonText: 'Edit',
   }));

   return (
      <>
         <Row className={styles.header}>
            <Header text="Profile" />
         </Row>
         <Row className={styles.wrapper}>
            <Col sm={6} lg={6}>
               <TextFieldModal
                  show={modalKey ? true : false}
                  isComboBox={(modalKey === 'zip' || modalKey === 'address') ? true : false}
                  comboBoxOptions={modalKey === 'zip' ? zips : addresses}
                  showModal={setModalKey}
                  textFieldKey={modalKey}
                  textFieldType={TextFieldUtil.mapKeyToType(modalKey)}
                  primaryAction={async () => {
                     await formHandler.handleSubmit();
                     setModalKey('');
                  }}
                  formHandler={formHandler}
                  primaryActionText="Save Changes"
                  secondaryAction={() => setModalKey('')}
               />
               <FormCard title="User Data" data={profileData} />
            </Col>
         </Row>
      </>
   );
};
