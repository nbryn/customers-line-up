import React from 'react';
import {Col, Row} from 'react-bootstrap';
import Divider from '@mui/material/Divider';
import makeStyles from '@mui/styles/makeStyles';

import type {AddressHandler} from '../../hooks/useAddress';
import {Card} from './Card';
import type {FormHandler, Index} from '../../hooks/useForm';
import {TextFieldCardRow} from './TextFieldCardRow';
import type {FormCardData} from './TextFieldCardRow';
import TextFieldUtil, {type HasAddress} from '../../util/TextFieldUtil';

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
    data: Index,
    formHandler: FormHandler<any>,
    addressHandler: AddressHandler,
    entity: HasAddress | undefined,
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
    formData: Index;
    entity: HasAddress | undefined;
    formHandler: FormHandler<any>;
    addressHandler: AddressHandler;
    title: string;
    buttonText?: string;
    primaryDisabled?: boolean;
    selectOptions?: string[];
    buttonAction?: () => void;
    getIndex?: (key: string) => number | undefined;
};

export const FormCard: React.FC<Props> = ({
    primaryDisabled,
    buttonText,
    title,
    formData: data,
    formHandler,
    addressHandler,
    entity,
    buttonAction,
    getIndex,
    selectOptions,
}: Props) => {
    const styles = useStyles();

    let formCardData = convertToFormData(data, formHandler, addressHandler, entity, getIndex);
    if (formCardData[0].index) {
        formCardData = formCardData.sort((a, b) => a.index! - b.index!);
    }
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
                    {formCardData.slice(0, 4).map((cardData) => (
                        <TextFieldCardRow
                            key={cardData.key}
                            data={cardData}
                            selectOptions={selectOptions}
                        />
                    ))}
                </Col>
                {formCardData.length > 4 && (
                    <>
                        <Divider className={styles.divider} orientation="vertical" flexItem />
                        <Col className={styles.divider} sm={12} md={6} lg={5}>
                            {formCardData.slice(4).map((cardData) => (
                                <TextFieldCardRow
                                    key={cardData.key}
                                    data={cardData}
                                    selectOptions={selectOptions}
                                />
                            ))}
                        </Col>
                    </>
                )}
                <Divider className={styles.divider} orientation="horizontal" />
            </Row>
        </Card>
    );
};
