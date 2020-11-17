import React from 'react';
import MaterialUITextField from '@material-ui/core/TextField';

type Props = {
   id: string;
   label: string | undefined;
   value?: string;
   onBlur?: () => void;
   helperText?: string;
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
};

export const TextField: React.FC<Props> = (props) => {
   return (
      <MaterialUITextField
         className={props.className}
         variant={props.variant}
         margin={props.margin}
         size={props.size}
         helperText={props.helperText}
         FormHelperTextProps={props.formHelperTextProps}
         inputProps={props.inputProps}
         fullWidth
         required
         id={props.id}
         onBlur={props.onBlur}
         onChange={props.onChange}
         label={props.label}
         value={props.value}
         type={props.type}
         autoFocus={props.autoFocus}
         select={props.select}
      >
         {props.children}
      </MaterialUITextField>
   );
};
