import React, {useEffect, useState} from 'react';
import {Col, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';

import {Card} from '../../components/card/Card';
import {ComboBox, ComboBoxOption} from '../../components/form/ComboBox';
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
    height: 675,
    borderRadius: 15,
    //boxShadow: '0px 0px 0px 8px rgba(12, 12, 242, 0.1)',
    textAlign: 'center',
  },
  helperText: {
    color: 'red',
  },
  textField: {
    width: '51%',
    marginTop: 10,
  },
  wrapper: {
    justifyContent: 'center',
  },
}));

export const SignupView: React.FC = () => {
  const styles = useStyles();
  const {setUser} = useUserContext();

  const [addresses, setAddresses] = useState<ComboBoxOption[]>([]);
  const [zips, setZips] = useState<ComboBoxOption[]>([]);

  const requestHandler: RequestHandler<UserDTO> = useRequest();

  const formValues: UserDTO = {
    email: '',
    name: '',
    zip: '',
    address: '',
    password: '',
  };

  const {addressHandler, formHandler} = useForm<UserDTO>(
    formValues,
    signupValidationSchema,
    REGISTER_URL,
    'POST',
    requestHandler.mutation,
    (user: UserDTO) => {
      const address = addresses.find((x) => x.label === user.address);

      user.longitude = address?.longitude;
      user.latitude = address?.latitude;
      return user;
    },
    setUser
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
        <Col sm={10} lg={6}>
          <Card className={styles.card} title="Signup">
            <Form
              onSubmit={formHandler.handleSubmit}
              buttonText="Signup"
              working={requestHandler.working}
              valid={formHandler.isValid}
              errorMessage={requestHandler.requestInfo}
            >
              {Object.keys(formValues).map((key) => {
                if (key === 'zip' || key === 'address') {
                  return (
                    <FormGroup key={key}>
                      <ComboBox
                        id={key}
                        style={{width: '51.5%', marginLeft: 129, marginTop: 25}}
                        label={StringUtil.capitalizeFirstLetter(key)}
                        type="text"
                        options={key === 'zip' ? zips : addresses}
                        onBlur={formHandler.handleBlur}
                        setFieldValue={(option: ComboBoxOption, formFieldId) =>
                          formHandler.setFieldValue(formFieldId, option.label)
                        }
                        error={formHandler.touched[key] && Boolean(formHandler.errors[key])}
                        helperText={formHandler.touched[key] && formHandler.errors[key]}
                        defaultLabel={key === 'address' ? 'Address - After Zip' : 'Zip'}
                      />
                    </FormGroup>
                  );
                }
                return (
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
                );
              })}
            </Form>
          </Card>
        </Col>
      </Row>
    </>
  );
};
