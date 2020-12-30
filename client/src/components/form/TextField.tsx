import {makeStyles} from '@material-ui/core/styles';
import MaterialUITextField from '@material-ui/core/TextField';
import React, {ReactText} from 'react';

const useStyles = makeStyles((theme) => ({
   helperText: {
      color: 'red',
   },
}));

export type TextFieldType = 'text' | 'number' | 'time' | 'password' | undefined;

type Props = {
   id: string;
   label: string | undefined;
   value?: ReactText | undefined;
   onBlur?: (event: React.FocusEvent) => void;
   helperText?: string | boolean;
   formHelperTextProps?: any;
   inputProps?: any;
   size?: 'small' | 'medium';
   margin?: 'none' | 'dense' | 'normal';
   type?: TextFieldType;
   onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
   autoFocus?: boolean;
   select?: boolean;
   className?: any;
   variant?: 'filled' | 'outlined';
   defaultValue?: ReactText;
   inputLabelProps?: any;
   error?: boolean;
   children?: React.ReactNode;
   required?: boolean;
   disabled?: boolean;
};

export const TextField: React.FC<Props> = (props: Props) => {
   const styles = useStyles();

   return (
      <MaterialUITextField
         className={props.className}
         variant={props.variant || 'outlined'}
         margin={props.margin}
         size={props.size}
         helperText={props.helperText}
         FormHelperTextProps={{className: styles.helperText}}
         inputProps={props.inputProps}
         fullWidth
         id={props.id}
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

      >
         {props.children}
      </MaterialUITextField>
   );
};
