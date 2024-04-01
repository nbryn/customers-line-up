import React from 'react';
import {Col, FormGroup, Row} from 'react-bootstrap';
import makeStyles from '@mui/styles/makeStyles';

import {Card} from '../../shared/components/card/Card';
import {ComboBox, type ComboBoxOption} from '../../shared/components/form/ComboBox';
import {Form} from '../../shared/components/form/Form';
import {registerValidationSchema} from '../user/UserValidation';
import StringUtil from '../../shared/util/StringUtil';
import {TextField} from '../../shared/components/form/TextField';
import TextFieldUtil from '../../shared/util/TextFieldUtil';
import {type AddressKey, useAddress} from '../../shared/hooks/useAddress';
import {useForm} from '../../shared/hooks/useForm';
import type {Index} from '../../shared/hooks/useForm';
import {useRegisterMutation} from './AuthApi';
import type {RegisterRequest} from '../../autogenerated';

const useStyles = makeStyles(() => ({
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

export const RegisterView: React.FC = () => {
    const styles = useStyles();
    const [register] = useRegisterMutation();

    const formValues = {
        email: '',
        name: '',
        zip: 0,
        street: '',
        password: '',
    };

    const {formHandler} = useForm<typeof formValues & Index>({
        initialValues: formValues,
        validationSchema: registerValidationSchema,
        onSubmit: async (formValues) => {
            const address = addressHandler.addresses.find(
                (address) => address.street === formValues.street
            );

            await register({...formValues, address} as RegisterRequest);
        },
    });

    const addressHandler = useAddress(formHandler);
    return (
        <Row className={styles.wrapper}>
            <Col sm={10} lg={6}>
                <Card className={styles.card} title="Register">
                    <Form
                        onSubmit={formHandler.handleSubmit}
                        submitButtonText="Register"
                        valid={formHandler.isValid}
                        showMessage
                    >
                        {Object.keys(formValues).map((key) => {
                            if (key === 'zip' || key === 'street') {
                                return (
                                    <FormGroup key={key}>
                                        <ComboBox
                                            id={key}
                                            style={{
                                                width: '51.5%',
                                                marginLeft: 129,
                                                marginTop: 25,
                                            }}
                                            label={StringUtil.capitalize(key)}
                                            type="text"
                                            options={addressHandler.getLabels(key as AddressKey)}
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
                                                key === 'street' ? 'street - After Zip' : 'Zip'
                                            }
                                        />
                                    </FormGroup>
                                );
                            }

                            return (
                                <FormGroup key={key}>
                                    <TextField
                                        className={styles.textField}
                                        id={key}
                                        label={StringUtil.capitalize(key)}
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
                                    />
                                </FormGroup>
                            );
                        })}
                    </Form>
                </Card>
            </Col>
        </Row>
    );
};
