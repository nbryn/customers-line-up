import React from 'react';
import Autocomplete, {createFilterOptions} from '@mui/material/Autocomplete';
import CircularProgress from '@mui/material/CircularProgress';
import TextField from '@mui/material/TextField';
import type moment from 'moment';

import type {TextFieldType} from './TextField';

export type ComboBoxOption = {
    label: string;
    value?: string
    date?: moment.Moment;
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

export const ComboBox = React.forwardRef<HTMLInputElement, Props>((props) => {
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
                    onChange={(_: any, newValue: ComboBoxOption | null) => {
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
