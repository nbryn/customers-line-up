import {Col, Container, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import React, {useState} from 'react';

import {Card} from '../../common/components/card/Card';
import {Form} from '../../common/components/form/Form';
import {login} from './userSlice';
import {LoginDTO} from './User';
import {loginValidationSchema} from './UserValidation';
import {SignupView} from './SignupView';
import StringUtil from '../../common/util/StringUtil';
import {TextField} from '../../common/components/form/TextField';
import TextFieldUtil from '../../common/util/TextFieldUtil';
import {useAppDispatch} from '../../app/Store';
import {useForm} from '../../common/hooks/useForm';

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
    const dispatch = useAppDispatch();
    const [renderSignUp, setRenderSignUp] = useState(false);

    const formValues: LoginDTO = {
        email: '',
        password: '',
    };

    const {formHandler} = useForm<LoginDTO>({
        initialValues: formValues,
        validationSchema: loginValidationSchema,
        onSubmit: (data) => dispatch(login(data)),
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
                                valid={formHandler.isValid}
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
