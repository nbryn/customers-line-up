import React, {useEffect, useState} from 'react';
import {pick} from 'lodash-es';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';

import {ComboBoxOption} from '../../common/components/form/ComboBox';
import {ErrorView} from '../../common/views/ErrorView';
import {FormCard, FormCardData} from '../../common/components/card/FormCard';
import {Header} from '../../common/components/Texts';
import {selectCurrentUser, updateUserInfo} from './userSlice';
import StringUtil from '../../common/util/StringUtil';
import {TextFieldModal} from '../../common/components/modal/TextFieldModal';
import TextFieldUtil from '../../common/util/TextFieldUtil';
import {useAppDispatch, useAppSelector} from '../../app/Store';
import {useForm} from '../../common/hooks/useForm';
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
    const [addresses, setAddresses] = useState<ComboBoxOption[]>([]);
    const [zips, setZips] = useState<ComboBoxOption[]>([]);

    if (!user) {
        return <ErrorView />;
    }

    const formValues = pick(user, ['name', 'email', 'zip', 'address']) as UserDTO;

    const {addressHandler, formHandler} = useForm<UserDTO>({
        initialValues: formValues,
        validationSchema: userValidationSchema,
        onSubmit: (updatedUserInfo) => dispatch(updateUserInfo(updatedUserInfo)),
        beforeSubmit: (updatedUser) => {
            const address = addresses.find((x) => x.label === user.address);
            updatedUser.longitude = address?.longitude ?? user.longitude;
            updatedUser.latitude = address?.latitude ?? user.latitude;
            updatedUser.role = user.role;
            updatedUser.id = user.id;

            return updatedUser;
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

    const profileData: FormCardData[] = Object.keys(formValues).map((key) => ({
        key,
        label: StringUtil.capitalize(key),
        type: TextFieldUtil.mapKeyToType(key),
        value: formHandler.values[key] as string,
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
                        isComboBox={modalKey === 'zip' || modalKey === 'address'}
                        addressOptions={addresses}
                        zipOptions={zips}
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
