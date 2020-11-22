import {makeStyles} from '@material-ui/core/styles';
import MaterialUITextField from '@material-ui/core/TextField';
import React, {useState} from 'react';

import {ValidationResult} from '../validation/ValidationRunner';

const useStyles = makeStyles((theme) => ({
   helperText: {
      color: 'red',
   },
}));

type Props = {
   id: string;
   label: string | undefined;
   value?: string;
   setValue?: (input: string) => void;
   onBlur?: (event: React.FocusEvent) => void;
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
   defaultValue?: string;
   inputLabelProps?: any;
   validateInput?: (input: string) => ValidationResult;
};

export const TextField: React.FC<Props> = (props) => {
   const styles = useStyles();

   const [errorMessage, setErrorMessage] = useState<string>('');

   const validateInput = (input: string) => {
      props.setValue!(input);
      const validation: ValidationResult = props.validateInput!(input);

      if (validation) {
         setErrorMessage(validation[props.id]);
      } else {
         setErrorMessage('');
      }
   };
   return (
      <MaterialUITextField
         className={props.className}
         variant={props.variant}
         margin={props.margin}
         size={props.size}
         helperText={errorMessage}
         FormHelperTextProps={{className: styles.helperText}}
         inputProps={props.inputProps}
         fullWidth
         required
         id={props.id}
         onBlur={props.onBlur}
         onChange={(e) => validateInput(e.target.value)}
         label={props.label}
         value={props.value}
         type={props.type}
         autoFocus={props.autoFocus}
         select={props.select}
         defaultValue={props.defaultValue}
         InputLabelProps={props.inputLabelProps}
      >
         {props.children}
      </MaterialUITextField>
   );
};
