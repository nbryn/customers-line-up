import {Col, Container, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';

import {useAuthService} from '../../services/AuthService';
import {Card} from '../../components/card/Card';
import {Form} from '../../components/form/Form';
import {loginValidationSchema} from '../../validation/UserValidation';
import {SignupView} from './SignupView';
import StringUtil from '../../util/StringUtil';
import {TextField} from '../../components/form/TextField';
import TextFieldUtil from '../../util/TextFieldUtil';
import {useForm} from '../../hooks/useForm';
import {UserDTO} from '../../models/User';
import {useUserContext} from '../../context/UserContext';

const useStyles = makeStyles((theme) => ({
    button: {
        width: '38%',
    },
    card: {
        marginTop: 60,
        borderRadius: 15,
        height: 450,
        //boxShadow: '0px 0px 0px 8px rgba(12, 12, 242, 0.1)',
        textAlign: 'center',
    },
    textField: {
        width: '42%',
    },
    wrapper: {
        justifyContent: 'center',
    },
}));

export const LoginView: React.FC = () => {
    const styles = useStyles();
    const {setUser} = useUserContext();

    const [renderSignUp, setRenderSignUp] = useState(false);
    const authService = useAuthService();

    const formValues: UserDTO = {
        email: '',
        password: '',
    };

    const {formHandler} = useForm<UserDTO>({
        initialValues: formValues,
        validationSchema: loginValidationSchema,
        onSubmit: authService.login,
        setUser,
    });

    return (
        <Container>
            {renderSignUp ? (
                <SignupView />
            ) : (
                <Row className={styles.wrapper}>
                    <Col sm={10} lg={6}>
                        <Card
                            className={styles.card}
                            title="Login"
                            buttonAction={() => setRenderSignUp(true)}
                            buttonColor="secondary"
                            buttonText="Signup"
                            buttonSize="medium"
                            buttonStyle={styles.button}
                            variant="outlined"
                        >
                            <Form
                                onSubmit={formHandler.handleSubmit}
                                buttonText="Login"
                                working={authService.working}
                                valid={formHandler.isValid}
                                errorMessage={authService.requestInfo ? 'Wrong Email/Password' : ''}
                            >
                                {Object.keys(formValues).map((key) => (
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
                                                !!formHandler.errors[key]
                                            }
                                            helperText={
                                                formHandler.touched[key] && formHandler.errors[key]
                                            }
                                            variant="outlined"
                                        />
                                    </FormGroup>
                                ))}
                            </Form>
                        </Card>
                    </Col>
                </Row>
            )}
        </Container>
    );
};
