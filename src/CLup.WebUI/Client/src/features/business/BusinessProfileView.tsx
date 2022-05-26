import React, {useEffect} from 'react';
import {Col, Row} from 'react-bootstrap';
import {makeStyles} from '@material-ui/core/styles';
import {omit} from 'lodash-es';

import {BusinessDTO} from './Business';
import {businessValidationSchema} from './BusinessValidation';
import {ErrorView} from '../../shared/views/ErrorView';
import {fetchBusinessesTypes, selectBusinessTypes, updateBusinessInfo} from './BusinessState';
import {FormCard} from '../../shared/components/card/FormCard';
import {Header} from '../../shared/components/Texts';
import {selectCurrentBusiness} from './BusinessState';
import {useAppDispatch, useAppSelector} from '../../app/Store';
import {useAddress} from '../../shared/hooks/useAddress';
import {useForm} from '../../shared/hooks/useForm';

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
};

export const BusinessProfileView: React.FC = () => {
    const styles = useStyles();
    const dispatch = useAppDispatch();

    const business = useAppSelector(selectCurrentBusiness);
    const businessTypes = useAppSelector(selectBusinessTypes);

    if (!business) {
        return <ErrorView />;
    }

    const formValues = omit(business, [
        'bookings',
        'businessHours',
        'city',
        'employees',
        'id',
        'longitude',
        'latitude',
        'messages',
        'ownerEmail',
        'timeSlots'
    ]) as BusinessDTO;

    const {formHandler} = useForm<BusinessDTO>({
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
            const address = addressHandler.addresses.find(
                (x) => x.street === updatedBusiness.street
            );
            updatedBusiness.longitude = address?.longitude ?? business.longitude;
            updatedBusiness.latitude = address?.latitude ?? business.latitude;
            updatedBusiness.city = address?.city ?? business.city;
            updatedBusiness.zip = address?.zip ?? business.zip;

            updatedBusiness.opens = updatedBusiness.opens.replace(':', '.');
            updatedBusiness.closes = updatedBusiness.closes.replace(':', '.');

            return updatedBusiness;
        },
    });

    const addressHandler = useAddress(formHandler);

    useEffect(() => {
        (async () => {
            dispatch(fetchBusinessesTypes());
        })();
    }, []);

    return (
        <>
            <Row className={styles.row}>
                <Header text={`Manage ${business.name}`} />
            </Row>
            <Row className={styles.row}>
                <Col sm={12} md={6} lg={10} className={styles.col}>
                    <FormCard
                        title="Business Data"
                        formData={formValues}
                        entity={business}
                        buttonText="Save Changes"
                        primaryDisabled={!formHandler.isValid}
                        buttonAction={formHandler.handleSubmit}
                        formHandler={formHandler}
                        addressHandler={addressHandler}
                        getIndex={getIndex}
                        selectOptions={businessTypes}
                    />
                </Col>
            </Row>
        </>
    );
};
