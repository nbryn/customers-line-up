import React, {useEffect, useState} from 'react';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {omit} from 'lodash-es';

import {BusinessDTO} from './Business';
import {businessValidationSchema} from './BusinessValidation';
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

const getIndex = (key: string) => {
    if (key === 'name') return 1;
    if (key === 'zip') return 2;
    if (key === 'street') return 3;
    if (key === 'type') return 4;
    if (key === 'capacity') return 5;
    if (key === 'timeSlotLength') return 6;
    if (key === 'opens') return 7;
    if (key === 'closes') return 8; 
} 

export const BusinessProfileView: React.FC = () => {
    const styles = useStyles();
    const dispatch = useAppDispatch();

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
        onSubmit: (updatedBusinessInfo) =>
            dispatch(
                updateBusinessInfo({
                    businessId: business.id,
                    ownerEmail: business.ownerEmail!,
                    business: updatedBusinessInfo,
                })
            ),
        beforeSubmit: (updatedBusiness) => {
            const address = addressHandler.addresses.find((x) => x.street === updatedBusiness.street);
            updatedBusiness.longitude = address?.longitude ?? business.longitude;
            updatedBusiness.latitude = address?.latitude ?? business.latitude;
            updatedBusiness.city = address?.city ?? business.city;
            updatedBusiness.zip = address?.zip ?? business.zip;

            updatedBusiness.opens = updatedBusiness.opens.replace(':', '.');
            updatedBusiness.closes = updatedBusiness.closes.replace(':', '.');

            return updatedBusiness;
        },
    });

    useEffect(() => {
        (async () => {
            dispatch(fetchBusinessesTypes());
        })();
    }, []);

    const businessData: FormCardData[] = Object.keys(formValues).map((key) => ({
        key,
        index: getIndex(key),
        label: TextFieldUtil.mapKeyToLabel(key),
        type: 'text',
        value: TextFieldUtil.mapKeyToValue(key, formHandler.values, addressHandler.addresses),
        buttonAction: () => setModalKey(key),
        buttonText: 'Update',
    }));

    console.log(businessData);

    return (
        <>
            <Row className={styles.row}>
                <Header text={`Manage ${business.name}`} />
            </Row>
            <Row className={styles.row}>
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
