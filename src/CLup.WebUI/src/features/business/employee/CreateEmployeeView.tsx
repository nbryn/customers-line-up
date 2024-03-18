import React, {useEffect, useState} from 'react';
import {Col, FormGroup, Row} from 'react-bootstrap';
import makeStyles from '@mui/styles/makeStyles';

import {Card} from '../../../shared/components/card/Card';
import {ComboBox} from '../../../shared/components/form/ComboBox';
import type {ComboBoxOption} from '../../../shared/components/form/ComboBox';
import {createEmployee} from './EmployeeState';
import {employeeValidationSchema} from '../BusinessValidation';
import {ErrorView} from '../../../shared/views/ErrorView';
import {
    fetchUsersNotEmployedByBusiness,
    selectUsersNotEmployedByBusiness,
} from '../../user/UserState';
import {Form} from '../../../shared/components/form/Form';
import {Header} from '../../../shared/components/Texts';
import type {EmployeeDTO, NewEmployeeDTO} from './Employee';
import {selectCurrentBusiness} from '../BusinessState';
import {TextField} from '../../../shared/components/form/TextField';
import {useAppDispatch, useAppSelector} from '../../../app/Store';
import {useForm} from '../../../shared/hooks/useForm';
import type {UserDTO} from '../../user/User';

const useStyles = makeStyles(() => ({
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

export const CreateEmployeeView: React.FC = () => {
    const styles = useStyles();
    const dispatch = useAppDispatch();

    const [selectedUser, setSelectedUser] = useState<UserDTO>();
    const [showComboBox, setShowComBox] = useState(true);
    const business = useAppSelector(selectCurrentBusiness);

    if (!business) {
        return <ErrorView />;
    }

    const usersNotEmployedByBusiness = useAppSelector(
        selectUsersNotEmployedByBusiness(business.id)
    );

    const {formHandler, ...form} = useForm<NewEmployeeDTO>({
        initialValues: {companyEmail: ''} as NewEmployeeDTO,
        validationSchema: employeeValidationSchema,
        onSubmit: (employee) => dispatch(createEmployee(employee as EmployeeDTO)),
    });

    useEffect(() => {
        (async () => {
            dispatch(fetchUsersNotEmployedByBusiness(business.id));
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
                                options={usersNotEmployedByBusiness.map((user) => ({
                                    label: user.email,
                                    value: user.name,
                                }))}
                                setFieldValue={(option: ComboBoxOption) =>
                                    setSelectedUser(
                                        usersNotEmployedByBusiness.find(
                                            (u) => u.email === option.label
                                        )
                                    )
                                }
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
                                            userId: selectedUser?.id,
                                            companyEmail: formHandler.values.companyEmail,
                                        });

                                        formHandler.handleSubmit();
                                    }}
                                    buttonText="Create"
                                    valid={formHandler.isValid}
                                >
                                    <FormGroup className={styles.formGroup}>
                                        <TextField
                                            className={styles.textField}
                                            id="name"
                                            label="Name"
                                            type="text"
                                            value={selectedUser?.name}
                                            disabled={true}
                                        />
                                    </FormGroup>
                                    <FormGroup className={styles.formGroup}>
                                        <TextField
                                            className={styles.textField}
                                            id="privateEmail"
                                            label="Private Email"
                                            type="email"
                                            value={selectedUser?.email}
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
