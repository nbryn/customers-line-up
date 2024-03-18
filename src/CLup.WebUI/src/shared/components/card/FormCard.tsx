import React from 'react';
import {Col, Row} from 'react-bootstrap';
import Divider from '@mui/material/Divider';
import makeStyles from '@mui/styles/makeStyles';

import type {AddressHandler} from '../../hooks/useAddress';
import type {BusinessDTO} from '../../../features/business/Business';
import {Card} from './Card';
import type {FormHandler} from '../../hooks/useForm';
import type {HasAddress} from '../../models/General';
import {TextFieldCardRow} from './TextFieldCardRow';
import type {FormCardData} from './TextFieldCardRow';
import TextFieldUtil from '../../util/TextFieldUtil';
import type {UserDTO} from '../../../features/user/User';

const useStyles = makeStyles({
    root: {
        minWidth: 275,
    },
    button: {
        marginBottom: 25,
        marginTop: 25,
        textAlign: 'center',
    },
    card: {
        textAlign: 'left',
    },
    divider: {
        marginLeft: 60,
    },
    end: {
        marginTop: -20,
    },
});

function convertToFormData(
    data: UserDTO | BusinessDTO,
    formHandler: FormHandler<any>,
    addressHandler: AddressHandler,
    entity: HasAddress,
    getIndex?: (key: string) => number | undefined
): FormCardData[] {
    return Object.keys(data).map((key) => ({
        key,
        index: getIndex ? getIndex(key) : undefined,
        label: TextFieldUtil.mapKeyToLabel(key),
        type: TextFieldUtil.mapKeyToType(key),
        error: formHandler.touched[key] && !!formHandler.errors[key],
        helperText: formHandler.touched[key] && (formHandler.errors[key] as any),
        isComboBox: key === 'zip' || key === 'street',
        streetOptions: addressHandler.getLabels('street'),
        zipOptions: addressHandler.getLabels('zip'),
        onChange: formHandler.handleChange(key),
        onBlur: formHandler.handleBlur,
        setFieldValue: (fieldId: string, value: any) => formHandler.setFieldValue(fieldId, value),
        value: TextFieldUtil.mapKeyToValue(
            key,
            formHandler.values,
            addressHandler.addresses,
            entity
        ),
    }));
}

type Props = {
    formData: UserDTO | BusinessDTO;
    entity: HasAddress;
    formHandler: FormHandler<any>;
    addressHandler: AddressHandler;
    buttonAction?: () => void;
    title: string;
    buttonText?: string;
    primaryAction?: () => void;
    getIndex?: (key: string) => number | undefined;
    primaryActionText?: string;
    primaryDisabled?: boolean;
    selectOptions?: string[];
};

export const FormCard: React.FC<Props> = ({
    buttonAction,
    primaryDisabled,
    buttonText,
    title,
    formData,
    formHandler,
    addressHandler,
    entity,
    getIndex,
    selectOptions,
}: Props) => {
    const styles = useStyles();

    let formCardData = convertToFormData(formData, formHandler, addressHandler, entity, getIndex);

    if (formCardData[0].index) formCardData = formCardData.sort((a, b) => a.index! - b.index!);
    return (
        <Card
            className={styles.card}
            buttonAction={buttonAction}
            buttonColor="primary"
            buttonText={buttonText}
            buttonSize="medium"
            disableButton={primaryDisabled}
            title={title}
            variant="outlined"
        >
            <Row>
                <Col sm={12} md={6} lg={formCardData.length < 5 ? 12 : 5}>
                    {formCardData.slice(0, 4).map((x) => (
                        <TextFieldCardRow data={x} selectOptions={selectOptions} />
                    ))}
                </Col>
                {formCardData.length > 4 && (
                    <>
                        <Divider className={styles.divider} orientation="vertical" flexItem />

                        <Col className={styles.divider} sm={12} md={6} lg={5}>
                            {formCardData.slice(4).map((x) => (
                                <TextFieldCardRow data={x} selectOptions={selectOptions} />
                            ))}
                        </Col>
                    </>
                )}
                <Divider className={styles.divider} orientation="horizontal" />
            </Row>
        </Card>
    );
};
