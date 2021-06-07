import React, {useEffect, useState} from 'react';
import {Col, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {MenuItem} from '@material-ui/core';
import {useHistory} from 'react-router-dom';

import {BusinessDTO} from '../../models/Business';
import {businessValidationSchema} from '../../validation/BusinessValidation';
import {Card} from '../../components/card/Card';
import {ComboBox, ComboBoxOption} from '../../components/form/ComboBox';
import {Form} from '../../components/form/Form';
import {Header} from '../../components/Texts';
import {Modal} from '../../components/modal/Modal';
import StringUtil from '../../util/StringUtil';
import {TextField} from '../../components/form/TextField';
import TextFieldUtil from '../../util/TextFieldUtil';
import {useBusinessService} from '../../services/BusinessService';
import {useForm} from '../../hooks/useForm';

const useStyles = makeStyles((theme) => ({
  card: {
    marginTop: 20,
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

export const CreateBusinessView: React.FC = () => {
  const styles = useStyles();
  const history = useHistory();

  const [businessTypes, setBusinessTypes] = useState<string[]>([]);
  const [addresses, setAddresses] = useState<ComboBoxOption[]>([]);
  const [zips, setZips] = useState<ComboBoxOption[]>([]);

  const businessService = useBusinessService(SUCCESS_MESSAGE);

  const formValues: BusinessDTO = {
    id: '',
    name: '',
    zip: '',
    address: '',
    type: '',
    capacity: '',
    timeSlotLength: '',
    opens: '',
    closes: '',
  };

  const {addressHandler, formHandler} = useForm<BusinessDTO>({
    initialValues: formValues,
    validationSchema: businessValidationSchema,
    onSubmit: businessService.createBusiness,
    formatter: (business) => {
      const address = addresses.find((x) => x.label === business.address);

      business.longitude = address?.longitude;
      business.latitude = address?.latitude;

      business.opens = business.opens.replace(':', '.');
      business.closes = business.closes.replace(':', '.');

      return business;
    }
  });

  useEffect(() => {
    (async () => {
      const types = await businessService.fetchBusinessTypes();

      setBusinessTypes(types);

      setZips(await addressHandler.fetchZips());
    })();
  }, []);

  useEffect(() => {
    (async () => {
      const {zip} = formHandler.values;
      if (!zip) return;

      setAddresses(await addressHandler.fetchAddresses(zip.substring(0, 4)));
    })();
  }, [formHandler.values.zip]);

  return (
    <>
      <Row className={styles.wrapper}>
        <Header text="New Business" />
      </Row>
      <Row className={styles.wrapper}>
        <Col sm={6} lg={8}>
          <Modal
            show={businessService.requestInfo ? true : false}
            title="Business Info"
            text={businessService.requestInfo}
            primaryAction={() => history.push('/business')}
            primaryActionText="My Businesses"
            secondaryAction={() => businessService.setRequestInfo('')}
          />
          <Card className={styles.card} title="Business Data" variant="outlined">
            <Form
              onSubmit={formHandler.handleSubmit}
              buttonText="Create"
              working={businessService.working}
              valid={formHandler.isValid}
            >
              <Row>
                <Col sm={6} lg={6}>
                  {Object.keys(formValues)
                    .slice(1, 5)
                    .map((key) => {
                      if (key === 'zip' || key === 'address') {
                        return (
                          <FormGroup key={key} className={styles.formGroup}>
                            <ComboBox
                              id={key}
                              style={{
                                width: '75%',
                                marginLeft: 43,
                                marginTop: 25,
                              }}
                              label={StringUtil.capitalize(key)}
                              type="text"
                              options={key === 'zip' ? zips : addresses}
                              onBlur={formHandler.handleBlur}
                              setFieldValue={(option: ComboBoxOption, formFieldId) =>
                                formHandler.setFieldValue(formFieldId, option.label)
                              }
                              error={formHandler.touched[key] && Boolean(formHandler.errors[key])}
                              helperText={formHandler.touched[key] && formHandler.errors[key]}
                              defaultLabel={key === 'address' ? 'Address - After Zip' : ''}
                            />
                          </FormGroup>
                        );
                      }
                      return (
                        <FormGroup key={key} className={styles.formGroup}>
                          <TextField
                            className={styles.textField}
                            id={key}
                            label={TextFieldUtil.mapKeyToLabel(key)}
                            type={TextFieldUtil.mapKeyToType(key)}
                            value={formHandler.values[key]}
                            onChange={formHandler.handleChange(key)}
                            onBlur={formHandler.handleBlur}
                            select={key === 'type' ? true : false}
                            error={formHandler.touched[key] && Boolean(formHandler.errors[key])}
                            helperText={formHandler.touched[key] && formHandler.errors[key]}
                          >
                            {key === 'type' &&
                              businessTypes.map((type) => (
                                <MenuItem key={type} value={type}>
                                  {type}
                                </MenuItem>
                              ))}
                          </TextField>
                        </FormGroup>
                      );
                    })}
                </Col>
                <Col sm={6} lg={6}>
                  {Object.keys(formValues)
                    .slice(5)
                    .map((key) => (
                      <FormGroup key={key} className={styles.formGroup}>
                        <TextField
                          className={styles.textField}
                          id={key}
                          label={TextFieldUtil.mapKeyToLabel(key)}
                          type={TextFieldUtil.mapKeyToType(key)}
                          value={formHandler.values[key]}
                          onChange={formHandler.handleChange}
                          onBlur={formHandler.handleBlur}
                          error={formHandler.touched[key] && Boolean(formHandler.errors[key])}
                          helperText={formHandler.touched[key] && formHandler.errors[key]}
                          inputLabelProps={{
                            shrink: TextFieldUtil.shouldInputLabelShrink(key),
                          }}
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
