import React, {useEffect, useState} from 'react';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {omit} from 'lodash-es';

import {BusinessDTO} from './Business';
import {businessValidationSchema} from './BusinessValidation';
import {ComboBoxOption} from '../../common/components/form/ComboBox';
import {ErrorView} from '../../common/views/ErrorView';
import {fetchBusinessesTypes, selectBusinessTypes, updateBusinessInfo} from './businessSlice';
import {FormCard, FormCardData} from '../../common/components/card/FormCard';
import {Header} from '../../common/components/Texts';
import {selectCurrentBusiness} from './businessSlice';
import TextFieldUtil from '../../common/util/TextFieldUtil';
import {TextFieldModal} from '../../common/components/modal/TextFieldModal';
import {useAppDispatch, useAppSelector} from '../../app/Store';
import {useForm} from '../../common/hooks/useForm';

const useStyles = makeStyles((theme) => ({
    col: {
        marginTop: 25,
    },
    row: {
        justifyContent: 'center',
    },
}));

export const BusinessProfileView: React.FC = () => {
    const styles = useStyles();
    const dispatch = useAppDispatch();

    const [addresses, setAddresses] = useState<ComboBoxOption[]>([]);
    const [zips, setZips] = useState<ComboBoxOption[]>([]);
    const [modalKey, setModalKey] = useState('');

    const business = useAppSelector(selectCurrentBusiness);
    const businessTypes = useAppSelector(selectBusinessTypes);

    if (!business) {
        return <ErrorView />;
    }

    const formValues = omit(business, [
        'businessHours',
        'city',
        'id',
        'longitude',
        'latitude',
        'ownerEmail',
    ]) as BusinessDTO;

    const {addressHandler, formHandler} = useForm<BusinessDTO>({
        initialValues: formValues,
        validationSchema: businessValidationSchema,
        onSubmit: (b) =>
            dispatch(
                updateBusinessInfo({
                    businessId: business.id,
                    ownerEmail: business.ownerEmail!,
                    business: b,
                })
            ),
        formatter: (business) => {
            business.opens = business.opens.replace(':', '.');
            business.closes = business.closes.replace(':', '.');

            return business;
        },
    });

    useEffect(() => {
        (async () => {
            dispatch(fetchBusinessesTypes());
            setZips(await addressHandler.fetchZips());
        })();
    }, []);

    useEffect(() => {
        (async () => {
            const {zip} = formHandler.values;
            const addresses = await addressHandler.fetchAddresses(zip?.substring(0, 4));

            setAddresses(addresses);
        })();
    }, [formHandler.values.zip]);

    const businessData: FormCardData[] = Object.keys(formValues).map((key) => ({
        key,
        label: TextFieldUtil.mapKeyToLabel(key),
        type: 'text',
        value:
            key === 'timeSlotLength'
                ? `${formHandler.values[key]} minutes`
                : (formHandler.values[key] as any),
        buttonAction: () => setModalKey(key),
        buttonText: 'Edit',
    }));

    return (
        <>
            <Row className={styles.row}>
                <Header text={`Manage ${business.name}`} />
            </Row>
            <Row className={styles.row}>
                <TextFieldModal
                    show={modalKey ? true : false}
                    isComboBox={modalKey === 'zip' || modalKey === 'address' ? true : false}
                    comboBoxOptions={modalKey === 'zip' ? zips : addresses}
                    showModal={setModalKey}
                    textFieldKey={modalKey}
                    textFieldType={TextFieldUtil.mapKeyToType(modalKey)}
                    primaryAction={async () => {
                        formHandler.handleSubmit();
                        setModalKey('');
                    }}
                    formHandler={formHandler}
                    initialValue={business[modalKey] as string}
                    primaryActionText="Save Changes"
                    selectOptions={businessTypes}
                />
                <Col sm={12} md={6} lg={12} className={styles.col}>
                    <FormCard title="Business Data" data={businessData} />
                </Col>
            </Row>
        </>
    );
};
