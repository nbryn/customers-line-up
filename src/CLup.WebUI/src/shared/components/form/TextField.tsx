import React, {ReactText} from 'react';
import {makeStyles} from '@material-ui/core/styles';
import MaterialUITextField from '@material-ui/core/TextField';
import { DTO } from '../../models/General';

const useStyles = makeStyles((theme) => ({
    helperText: {
        color: 'red',
    },
}));

export type TextFieldType = 'text' | 'number' | 'time' | 'password' | 'email' | undefined;

type Props = {
    id: string;
    label: string | undefined;
    value?: ReactText | boolean | DTO | DTO[] | undefined;
    helperText?: string | boolean;
    inputProps?: any;
    size?: 'small' | 'medium';
    margin?: 'none' | 'dense' | 'normal';
    type?: TextFieldType;
    autoFocus?: boolean;
    select?: boolean;
    className?: any;
    variant?: 'filled' | 'outlined' | 'standard';
    defaultValue?: ReactText;
    inputLabelProps?: any;
    error?: boolean;
    children?: React.ReactNode;
    required?: boolean;
    disabled?: boolean;
    step?: number;
    maxLength?: number;
    multiline?: boolean;
    style?: any
    setInputRef?: (element: HTMLInputElement) => void;
    onBlur?: (event: React.FocusEvent) => void;
    onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
    onClick?: (event?: React.MouseEvent<HTMLParagraphElement>) => void;
};

export const TextField = React.forwardRef<HTMLInputElement, Props>((props, ref) => {
    const styles = useStyles();

    return (
        <MaterialUITextField
            className={props.className}
            variant={props.variant || 'outlined'}
            margin={props.margin}
            size={props.size}
            helperText={props.helperText}
            FormHelperTextProps={{className: styles.helperText}}
            fullWidth
            id={props.id}
            multiline={props.multiline}
            onClick={props.onClick}
            onBlur={props.onBlur}
            onChange={props.onChange}
            label={props.label}
            value={props.value}
            type={props.type}
            autoFocus={props.autoFocus}
            select={props.select}
            defaultValue={props.defaultValue}
            InputLabelProps={props.inputLabelProps}
            error={props.error}
            required={props.required || true}
            disabled={props.disabled || false}
            inputRef={props.setInputRef}
            style={props.style}
            autoComplete="off"
            inputProps={{
                maxLength: props.maxLength ?? 500,
                autoComplete: 'new-password',
                step: props.step,
                form: {
                    autoComplete: 'off',
                },
            }}
        >
            {props.children}
        </MaterialUITextField>
    );
});
