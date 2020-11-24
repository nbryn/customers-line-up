import {makeStyles} from '@material-ui/core/styles';
import MaterialUITextField from '@material-ui/core/TextField';
import React from 'react';

const useStyles = makeStyles((theme) => ({
   helperText: {
      color: 'red',
   },
}));

type Props = {
   id: string;
   label: string | undefined;
   value: string | number;
   onBlur?: (event: React.FocusEvent) => void;
   helperText?: string | boolean;
   formHelperTextProps?: any;
   inputProps?: any;
   size?: 'small' | 'medium';
   margin?: 'none' | 'dense' | 'normal';
   type?: string;
   onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
   autoFocus?: boolean;
   select?: boolean;
   className?: any;
   variant?: 'filled' | 'outlined';
   defaultValue?: string;
   inputLabelProps?: any;
   error?: boolean;
   children?: React.ReactNode;
};

export const TextField: React.FC<Props> = (props: Props) => {
   const styles = useStyles();

   return (
      <MaterialUITextField
         className={props.className}
         variant={props.variant}
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
      >
         {props.children}
      </MaterialUITextField>
   );
};
