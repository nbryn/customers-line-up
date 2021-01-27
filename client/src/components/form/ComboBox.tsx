import React from 'react';
import Autocomplete, {createFilterOptions} from '@material-ui/lab/Autocomplete';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';

import {TextFieldType} from './TextField';

const useStyles = makeStyles((theme) => ({
   inputRoot: {
      '&&[class*="MuiOutlinedInput-root"] $input': {
         margin: 0,
      },
   },
   textField: {
      width: '60%',
      textAlign: 'center',
   },
}));

export type ComboBoxOption = {
   label: string;
   value?: string;
};

export type Props = {
   id: string;
   label: string;
   options: ComboBoxOption[];
   type?: TextFieldType;
   setFieldValue: (option: ComboBoxOption, formFieldId: string) => void;
   onBlur?: (event: React.FocusEvent) => void;
   error?: boolean;
   helperText?: string | boolean;
   style?: any;
   partOfForm?: boolean;
   defaultLabel?: string;
};

export const ComboBox: React.FC<Props> = ({
   id,
   label,
   options,
   style,
   error,
   helperText,
   type,
   defaultLabel,
   partOfForm = true,
   onBlur,
   setFieldValue,
}: Props) => {
   const styles = useStyles();

   const filterOptions = createFilterOptions<ComboBoxOption>({
      limit: 10,
      matchFrom: 'start',
   });

   return (
      <>
         {!partOfForm && options.length === 0 ? (
            <CircularProgress />
         ) : (
            <Autocomplete
               id={id}
               filterOptions={filterOptions}
               onBlur={onBlur}
               onChange={(event: any, newValue: ComboBoxOption | null) => {
                  setFieldValue(newValue || {label: ''}, id);
               }}
               options={options}
               getOptionLabel={(option: ComboBoxOption) => option.label}
               style={style}
               renderInput={(params) => (
                  <TextField
                     {...params}
                     error={error}
                     helperText={helperText}
                     label={options.length === 0 ? defaultLabel : label}
                     type={type}
                     required
                     variant="outlined"
                  />
               )}
            />
         )}
      </>
   );
};
