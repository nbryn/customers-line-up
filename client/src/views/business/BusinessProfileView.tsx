import React, {useEffect, useState} from 'react';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {omit} from 'lodash-es';
import {useLocation} from 'react-router-dom';

import {BusinessDTO} from '../../models/Business';
import {businessValidationSchema} from '../../validation/BusinessValidation';
import {ComboBoxOption} from '../../components/form/ComboBox';
import {ErrorView} from '../ErrorView';
import {FormCard, FormCardData} from '../../components/card/FormCard';
import {Header} from '../../components/Texts';
import TextFieldUtil from '../../util/TextFieldUtil';
import {TextFieldModal} from '../../components/modal/TextFieldModal';
import {useForm} from '../../hooks/useForm';
import {useBusinessService} from '../../services/BusinessService';

const useStyles = makeStyles((theme) => ({
    col: {
        marginTop: 25,
    },
    row: {
        justifyContent: 'center',
    },
}));

interface LocationState {
    business: BusinessDTO;
}

export const BusinessProfileView: React.FC = () => {
    const styles = useStyles();
    const location = useLocation<LocationState>();

    const [modalKey, setModalKey] = useState('');
    const [businessTypeOptions, setBusinessTypeOptions] = useState<string[]>([]);
    const [addresses, setAddresses] = useState<ComboBoxOption[]>([]);
    const [zips, setZips] = useState<ComboBoxOption[]>([]);

    if (!location.state) {
        return <ErrorView />;
    }

    const business = location.state.business;

    const businessService = useBusinessService();

    const formValues = omit(business, ['id', 'longitude', 'latitude']) as BusinessDTO;

    const {addressHandler, formHandler} = useForm<BusinessDTO>({
        initialValues: formValues,
        validationSchema: businessValidationSchema,
        onSubmit: businessService.updateBusinessInfo(business.id),
        formatter: (business) => {
            business.opens = business.opens.replace(':', '.');
            business.closes = business.closes.replace(':', '.');

            return business;
        }
    });

    useEffect(() => {
        (async () => {
            setZips(await addressHandler.fetchZips());

            setBusinessTypeOptions(await businessService.fetchBusinessTypes());
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
                        await formHandler.handleSubmit();
                        setModalKey('');
                    }}
                    formHandler={formHandler}
                    initialValue={business[modalKey] as string}
                    primaryActionText="Save Changes"
                    selectOptions={businessTypeOptions}
                />
                <Col sm={12} md={6} lg={12} className={styles.col}>
                    <FormCard title="Business Data" data={businessData} />
                </Col>
            </Row>
        </>
    );
};
