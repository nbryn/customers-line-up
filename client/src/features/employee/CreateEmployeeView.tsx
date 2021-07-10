import React, {useEffect, useState} from 'react';
import {Col, FormGroup, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {useHistory, useLocation} from 'react-router-dom';
import {useSelector} from 'react-redux';

import {BusinessDTO} from '../business/Business';
import {Card} from '../../common/components/card/Card';
import {ComboBox, ComboBoxOption} from '../../common/components/form/ComboBox';
import {createEmployee} from './employeeSlice';
import EmployeeService from './EmployeeService';
import {employeeValidationSchema} from '../business/BusinessValidation';
import {ErrorView} from '../../common/views/ErrorView';
import {fetchUsersNotEmployedByBusiness, selectUsersAsComboBoxOption} from '../user/userSlice';
import {Form} from '../../common/components/form/Form';
import {Header} from '../../common/components/Texts';
import {Modal} from '../../common/components/modal/Modal';
import {NewEmployeeDTO} from './Employee';
import {TextField} from '../../common/components/form/TextField';
import {
    clearApiMessage,
    isLoading,
    RootState,
    selectApiMessage,
    useAppDispatch,
    useAppSelector,
} from '../../app/Store';
import {useForm} from '../../common/hooks/useForm';
import {useUserService} from '../user/UserService';

const useStyles = makeStyles((theme) => ({
    card: {
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

export const CreateEmployeeView: React.FC = () => {
    const styles = useStyles();
    const history = useHistory();
    const location = useLocation<LocationState>();
    
    const loading = useAppSelector(isLoading);
    const dispatch = useAppDispatch();

    const [selectedUser, setSelectedUser] = useState<ComboBoxOption>({label: ''});
    const [showComboBox, setShowComBox] = useState(true);

    const userService = useUserService();

    if (!location.state) {
        return <ErrorView />;
    }

    const {business} = location.state;
    const apiMessage = useAppSelector(selectApiMessage);
    const usersNotEmployedByBusiness = useSelector<RootState, ComboBoxOption[] | null>((state) =>
        selectUsersAsComboBoxOption(state, business.id)
    );

    const formValues: NewEmployeeDTO = {
        companyEmail: '',
    };

    const {formHandler, ...form} = useForm<NewEmployeeDTO>({
        initialValues: formValues,
        validationSchema: employeeValidationSchema,
        onSubmit: EmployeeService.createEmployee,
    });

    useEffect(() => {
        (async () => {
            dispatch(fetchUsersNotEmployedByBusiness({service: userService, data: business.id}));
        })();
    }, []);

    useEffect(() => {
        if (usersNotEmployedByBusiness?.find((user) => user.label === selectedUser?.label)) {
            setShowComBox(false);
        }
    }, [selectedUser]);

    return (
        <>
            <Row className={styles.wrapper}>
                <Header text={business.name} />
            </Row>
            <Row className={styles.wrapper}>
                <Col sm={6} lg={6}>
                    <Modal
                        show={apiMessage ? true : false}
                        title="Employee Info"
                        text={apiMessage}
                        primaryAction={() => history.push('/business/employees/manage', {business})}
                        primaryActionText="My Employees"
                        secondaryAction={() => dispatch(clearApiMessage)}
                    />
                    <Card
                        className={styles.card}
                        title="New Employee"
                        subtitle="Choose an existing user you want to add as an employee"
                        variant="outlined"
                    >
                        {showComboBox && usersNotEmployedByBusiness && (
                            <ComboBox
                                style={{marginTop: 10, marginLeft: 110, width: '60%'}}
                                label="Email"
                                id="email"
                                type="text"
                                options={usersNotEmployedByBusiness}
                                setFieldValue={(option: ComboBoxOption) => setSelectedUser(option)}
                                partOfForm={false}
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
                                            privateEmail: selectedUser?.label,
                                            companyEmail: formHandler.values.companyEmail,
                                        });

                                        formHandler.handleSubmit();
                                    }}
                                    buttonText="Create"
                                    working={loading}
                                    valid={formHandler.isValid}
                                >
                                    <FormGroup className={styles.formGroup}>
                                        <TextField
                                            className={styles.textField}
                                            id="name"
                                            label="Name"
                                            type="text"
                                            value={selectedUser.value}
                                            disabled={true}
                                        />
                                    </FormGroup>
                                    <FormGroup className={styles.formGroup}>
                                        <TextField
                                            className={styles.textField}
                                            id="privateEmail"
                                            label="Private Email"
                                            type="email"
                                            value={selectedUser.label}
                                            disabled={true}
                                        />
                                    </FormGroup>
                                    <FormGroup className={styles.formGroup}>
                                        <TextField
                                            className={styles.textField}
                                            id="companyEmail"
                                            label="Company Email"
                                            type="email"
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
