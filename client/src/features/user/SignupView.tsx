import React, {useEffect, useState} from 'react';
import {Col, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';

import {Card} from '../../common/components/card/Card';
import {ComboBox, ComboBoxOption} from '../../common/components/form/ComboBox';
import {Form} from '../../common/components/form/Form';
import {register} from './userSlice';
import {selectApiState} from '../../common/api/apiSlice';
import {signupValidationSchema} from './UserValidation';
import StringUtil from '../../common/util/StringUtil';
import {TextField} from '../../common/components/form/TextField';
import TextFieldUtil from '../../common/util/TextFieldUtil';
import {useAppDispatch, useAppSelector} from '../../app/Store';
import {useForm} from '../../common/hooks/useForm';
import {UserDTO} from './User';

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
    const dispatch = useAppDispatch();
    const apiState = useAppSelector(selectApiState);

    const [addresses, setAddresses] = useState<ComboBoxOption[]>([]);
    const [zips, setZips] = useState<ComboBoxOption[]>([]);

    const formValues: UserDTO = {
        email: '',
        name: '',
        zip: '',
        address: '',
        password: '',
    };

    const {addressHandler, formHandler} = useForm<UserDTO>({
        initialValues: formValues,
        validationSchema: signupValidationSchema,
        onSubmit: (data) => dispatch(register(data)),
        formatter: (user: UserDTO) => {
            const address = addresses.find((x) => x.label === user.address);

            user.longitude = address?.longitude;
            user.latitude = address?.latitude;
            return user;
        },
    });

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
                            working={apiState.loading}
                            valid={formHandler.isValid}
                            errorMessage={apiState.message}
                        >
                            {Object.keys(formValues).map((key) => {
                                if (key === 'zip' || key === 'address') {
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
                                                options={key === 'zip' ? zips : addresses}
                                                onBlur={formHandler.handleBlur}
                                                setFieldValue={(
                                                    option: ComboBoxOption,
                                                    formFieldId
                                                ) =>
                                                    formHandler.setFieldValue(
                                                        formFieldId,
                                                        option.label
                                                    )
                                                }
                                                error={
                                                    formHandler.touched[key] &&
                                                    Boolean(formHandler.errors[key])
                                                }
                                                helperText={
                                                    formHandler.touched[key] &&
                                                    formHandler.errors[key]
                                                }
                                                defaultLabel={
                                                    key === 'address'
                                                        ? 'Address - After Zip'
                                                        : 'Zip'
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
        </>
    );
};
