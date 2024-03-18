import React, {useRef, useState} from 'react';
import {Col, Row} from 'react-bootstrap';
import Divider from '@mui/material/Divider';
import makeStyles from '@mui/styles/makeStyles';
import {MenuItem} from '@mui/material';

import {ComboBox} from '../form/ComboBox';
import type {ComboBoxOption} from '../form/ComboBox';
import {TextField} from '../form/TextField';
import type {TextFieldType} from '../form/TextField';
import TextFieldUtil from '../../util/TextFieldUtil';

const useStyles = makeStyles({
    root: {
        minWidth: 275,
    },
    row: {
        marginLeft: 0,
        marginTop: 25,
        marginBottom: 25,
        textAlign: 'center',
    },
    dataCol: {
        textAlign: 'center',
    },
    divider: {
        marginLeft: -5,
        marginRight: -5,
    },
});

export type FormCardData = {
    index?: number | undefined;
    label: string | undefined;
    key: string;
    value: string | number;
    type?: TextFieldType;
    error?: boolean;
    helperText?: string | boolean | undefined;
    isComboBox?: boolean;
    zipOptions?: ComboBoxOption[];
    streetOptions?: ComboBoxOption[];
    onBlur?: (event: React.FocusEvent) => void;
    onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
    setFieldValue?: (field: string, value: any) => void;
};

type Props = {
    data: FormCardData;
    selectOptions?: string[];
};

export const TextFieldCardRow: React.FC<Props> = ({data, selectOptions}: Props) => {
    const styles = useStyles();
    const [disabled, setDisabled] = useState(true);
    const [shouldFocus, setShouldFocus] = useState(false);
    const [showComboBox, setShowComboBox] = useState(false);
    const textFieldRef = useRef<HTMLInputElement | null>(null);

    const setInputRef = (element: HTMLInputElement) => {
        if (shouldFocus) {
            textFieldRef.current = element;
            textFieldRef?.current?.focus();
            setShouldFocus(false);
        }
    };

    return (
        <>
            <Divider className={styles.divider} />
            <Row className={styles.row}>
                <Col>
                    {data.isComboBox && showComboBox ? (
                        <ComboBox
                            id={data.key}
                            label={TextFieldUtil.mapKeyToLabel(data.key)}
                            type="text"
                            style={{width: '50%', marginLeft: 125}}
                            options={
                                (data.key === 'zip' ? data.zipOptions : data.streetOptions) ?? []
                            }
                            setInputRef={setInputRef}
                            onBlur={(event) => {
                                setShowComboBox(false);
                                setDisabled(true);
                                data.onBlur!(event);
                            }}
                            setFieldValue={(option: ComboBoxOption, formFieldId: string) =>
                                data.setFieldValue!(formFieldId, option.label)
                            }
                            defaultLabel={data.key === 'street' ? 'Zip before street' : ''}
                            partOfForm
                        />
                    ) : (
                        <TextField
                            id={data.key}
                            disabled={disabled}
                            label={data.label}
                            type={data.type}
                            value={data.value}
                            onChange={data.onChange}
                            onBlur={data.onBlur}
                            error={data.error}
                            helperText={data.helperText}
                            style={{width: '50%'}}
                            setInputRef={setInputRef}
                            onClick={() => {
                                if (disabled) {
                                    setShouldFocus(true);
                                    setShowComboBox(!showComboBox);
                                    setDisabled(!disabled);
                                }
                            }}
                            inputLabelProps={{
                                shrink: TextFieldUtil.shouldInputLabelShrink(data.key),
                            }}
                            step={TextFieldUtil.mapKeyToStep(data.key)}
                        >
                            {selectOptions &&
                                selectOptions.map((x, index) => (
                                    <MenuItem key={index} value={x}>
                                        {x}
                                    </MenuItem>
                                ))}
                        </TextField>
                    )}
                </Col>
            </Row>
        </>
    );
};
