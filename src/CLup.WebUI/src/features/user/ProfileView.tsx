import React from 'react';
import {pick} from 'lodash-es';
import {Col, Row} from 'react-bootstrap';
import makeStyles from '@mui/styles/makeStyles';

import {ErrorView} from '../../shared/views/ErrorView';
import {FormCard} from '../../shared/components/card/FormCard';
import {Header} from '../../shared/components/Texts';
import {selectCurrentUser} from './UserState';
import {updateUserInfo} from './UserState';
import {useAppDispatch, useAppSelector} from '../../app/Store';
import {useAddress} from '../../shared/hooks/useAddress';
import {useForm} from '../../shared/hooks/useForm';
import type {UserDTO} from './User';
import {userValidationSchema} from './UserValidation';

const useStyles = makeStyles(() => ({
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

export const ProfileView: React.FC = () => {
    const styles = useStyles();
    const user = useAppSelector(selectCurrentUser);
    const dispatch = useAppDispatch();

    if (!user) {
        return <ErrorView />;
    }

    const formValues = pick(user, ['name', 'email', 'zip', 'street']) as UserDTO;

    const {formHandler} = useForm<UserDTO>({
        initialValues: formValues,
        validationSchema: userValidationSchema,
        onSubmit: (updatedUserInfo) => dispatch(updateUserInfo(updatedUserInfo)),
        beforeSubmit: (updatedUser) => {
            const address = addressHandler.addresses.find((x) => x.street === updatedUser.street);

            updatedUser.longitude = address?.longitude ?? user.longitude;
            updatedUser.latitude = address?.latitude ?? user.latitude;
            updatedUser.city = address?.city ?? user.city;
            updatedUser.zip = address?.zip ?? user.zip;
            updatedUser.role = user.role;
            updatedUser.id = user.id;

            return updatedUser;
        },
    });

    const addressHandler = useAddress(formHandler);

    return (
        <>
            <Row className={styles.header}>
                <Header text="Profile" />
            </Row>
            <Row className={styles.wrapper}>
                <Col sm={6} lg={6}>
                    <FormCard
                        title="User Data"
                        formData={formValues}
                        entity={user}
                        buttonText="Save Changes"
                        primaryDisabled={!formHandler.isValid}
                        buttonAction={formHandler.handleSubmit}
                        formHandler={formHandler}
                        addressHandler={addressHandler}
                    />
                </Col>
            </Row>
        </>
    );
};
