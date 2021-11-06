import React, {useEffect, useState} from 'react';
import {pick} from 'lodash-es';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';

import {ErrorView} from '../../shared/views/ErrorView';
import {FormCard, FormCardData} from '../../shared/components/card/FormCard';
import {Header} from '../../shared/components/Texts';
import {selectCurrentUser, updateUserInfo} from './userSlice';
import StringUtil from '../../shared/util/StringUtil';
import {TextFieldModal} from '../../shared/components/modal/TextFieldModal';
import TextFieldUtil from '../../shared/util/TextFieldUtil';
import {useAppDispatch, useAppSelector} from '../../app/Store';
import {useForm} from '../../shared/hooks/useForm';
import {UserDTO} from './User';
import {userValidationSchema} from './UserValidation';

const useStyles = makeStyles((theme) => ({
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
    const [modalKey, setModalKey] = useState('');

    if (!user) {
        return <ErrorView />;
    }

    const formValues = pick(user, ['name', 'email', 'zip', 'street']) as UserDTO;

    const {addressHandler, formHandler} = useForm<UserDTO>({
        initialValues: formValues,
        validationSchema: userValidationSchema,
        onSubmit: (updatedUserInfo) => dispatch(updateUserInfo(updatedUserInfo)),
        beforeSubmit: (updatedUser) => {
            const address = addressHandler.addresses.find((x) => x.zipCity === user.address);

            updatedUser.longitude = address?.longitude;
            updatedUser.latitude = address?.latitude;
            updatedUser.city = address?.city ?? '';
            updatedUser.zip = address?.zip ?? '';
            updatedUser.role = user.role;
            updatedUser.id = user.id;

            return updatedUser;
        },
    });

    const profileData: FormCardData[] = Object.keys(formValues).map((key) => ({
        key,
        label: StringUtil.capitalize(key),
        type: TextFieldUtil.mapKeyToType(key),
        value: TextFieldUtil.mapKeyToValue(key, formHandler.values, addressHandler.addresses),
        buttonAction: () => setModalKey(key),
        buttonText: 'Update',
    }));

    return (
        <>
            <Row className={styles.header}>
                <Header text="Profile" />
            </Row>
            <Row className={styles.wrapper}>
                <Col sm={6} lg={6}>
                    <TextFieldModal
                        show={modalKey ? true : false}
                        isComboBox={modalKey === 'zip' || modalKey === 'street'}
                        streetOptions={addressHandler.getLabels('street')}
                        zipOptions={addressHandler.getLabels('zip')}
                        showModal={setModalKey}
                        textFieldKey={modalKey}
                        textFieldType={TextFieldUtil.mapKeyToType(modalKey)}
                        primaryAction={async () => {
                            formHandler.handleSubmit();
                            setModalKey('');
                        }}
                        initialValue={user[modalKey] as string}
                        formHandler={formHandler}
                        primaryActionText="Save Changes"
                    />
                    <FormCard title="User Data" data={profileData} />
                </Col>
            </Row>
        </>
    );
};
