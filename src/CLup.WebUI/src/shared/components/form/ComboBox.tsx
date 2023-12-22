import React from 'react';
import Autocomplete, {createFilterOptions} from '@material-ui/lab/Autocomplete';
import CircularProgress from '@material-ui/core/CircularProgress';
import TextField from '@material-ui/core/TextField';

import {TextFieldType} from './TextField';

export type ComboBoxOption = {
    label: string;
    value?: string;
};

export type Props = {
    id: string;
    label: string;
    options: ComboBoxOption[];
    type?: TextFieldType;
    error?: boolean;
    helperText?: string | boolean;
    style?: React.CSSProperties;
    partOfForm?: boolean;
    defaultLabel?: string;
    setFieldValue: (option: ComboBoxOption, formFieldId: string) => void;
    onBlur?: (event: React.FocusEvent) => void;
    setInputRef?: (element: HTMLInputElement) => void;
};

export const ComboBox = React.forwardRef<HTMLInputElement, Props>((props, ref) => {
    const filterOptions = createFilterOptions<ComboBoxOption>({
        limit: props.options[0]?.label === 'Choose zip first' ? 1 : 10,
        matchFrom: 'start',
    });

    return (
        <>
            {!props.partOfForm && props.options.length === 0 ? (
                <CircularProgress />
            ) : (
                <Autocomplete
                    id={props.id}
                    filterOptions={filterOptions}
                    onBlur={props.onBlur}
                    onChange={(event: any, newValue: ComboBoxOption | null) => {
                        props.setFieldValue(newValue || {label: ''}, props.id);
                    }}
                    options={props.options}
                    getOptionLabel={(option: ComboBoxOption) => option.label}
                    style={props.style}
                    renderInput={(params) => (
                        <TextField
                            {...params}
                            inputRef={props.setInputRef}
                            error={props.error}
                            helperText={props.helperText}
                            label={props.options.length === 0 ? props.defaultLabel : props.label}
                            type={props.type}
                            required
                            variant="outlined"
                        />
                    )}
                />
            )}
        </>
    );
});
