import React from 'react';
import {useHistory} from 'react-router-dom';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Avatar from '@mui/material/Avatar';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import makeStyles from '@mui/styles/makeStyles';
import Link from '@mui/material/Link';

import {Form} from '../../shared/components/form/Form';
import {loginValidationSchema} from '../user/UserValidation';
import StringUtil from '../../shared/util/StringUtil';
import {TextField} from '../../shared/components/form/TextField';
import TextFieldUtil from '../../shared/util/TextFieldUtil';
import {useForm} from '../../shared/hooks/useForm';
import type {Index} from '../../shared/hooks/useForm';
import {useLoginMutation} from './AuthApi';
import {REGISTER_ROUTE} from '../../app/RouteConstants';
import {Checkbox, FormControlLabel} from '@mui/material';

const useStyles = makeStyles({
    avatar: {
        margin: 7,
    },
    box: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        marginTop: 65,
    },
    links: {
        marginBottom: 50,
        marginTop: 15,
    },
    submitButton: {
        marginBottom: 2,
        marginTop: 3,
    },
});

export const LoginView: React.FC = () => {
    const [login] = useLoginMutation();
    const history = useHistory();
    const styles = useStyles();

    const formValues = {
        email: '',
        password: '',
    };

    const {formHandler} = useForm<typeof formValues & Index>({
        initialValues: formValues,
        validationSchema: loginValidationSchema,
        onSubmit: async (data) => await login({...data}),
    });

    return (
        <Container component="main" maxWidth="xs">
            <Box className={styles.box}>
                <Avatar className={styles.avatar} sx={{bgcolor: 'secondary.main'}}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    {'Login'}
                </Typography>
                <Form
                    submitButtonText="Login"
                    submitButtonStyle={styles.submitButton}
                    onSubmit={formHandler.handleSubmit}
                    valid={formHandler.isValid}
                >
                    {Object.keys(formValues).map((key) => (
                        <TextField
                            key={key}
                            id={key}
                            label={StringUtil.capitalize(key)}
                            type={TextFieldUtil.mapKeyToType(key)}
                            value={formHandler.values[key] as string}
                            onChange={formHandler.handleChange}
                            onBlur={formHandler.handleBlur}
                            error={formHandler.touched[key] && !!formHandler.errors[key]}
                            helperText={formHandler.touched[key] && formHandler.errors[key]}
                            variant="outlined"
                            margin="normal"
                        />
                    ))}
                    <FormControlLabel
                        control={<Checkbox value="remember" color="primary" />}
                        label="Remember me"
                    />
                </Form>
                <Grid container className={styles.links}>
                    <Grid item xs>
                        <Link href="#" variant="body2">
                            {'Forgot password?'}
                        </Link>
                    </Grid>

                    <Grid item xs marginLeft={15}>
                        <Link onClick={() => history.push(REGISTER_ROUTE)} href="" variant="body2">
                            {'No account? Register'}
                        </Link>
                    </Grid>
                </Grid>
            </Box>

            <Typography align="center" color="text.secondary" variant="body2">
                {'Copyright © Customers Lineup '}
                {new Date().getFullYear()}
                {'.'}
            </Typography>
        </Container>
    );
};
