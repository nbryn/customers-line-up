import React, {ReactText} from 'react';
import Autocomplete, {createFilterOptions} from '@material-ui/lab/Autocomplete';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';

//import {TextField} from './TextField'

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

export type Props = {
   id: string;
   label: string;
   options: ReactText[];
   type?: string;
   setFieldValue: (id: string, value?: string) => void;
   onBlur?: (event: React.FocusEvent) => void;
   value?: ReactText;
   error?: boolean;
   helperText?: string | boolean;
   style?: any;
   partOfForm?: boolean;
};

export const ComboBox: React.FC<Props> = ({
   id,
   label,
   options,
   style,
   value,
   error,
   helperText,
   type,
   partOfForm = true,
   onBlur,
   setFieldValue,
}: Props) => {
   const styles = useStyles();

   const filterOptions = createFilterOptions({
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
               value={value}
               onChange={(event: any, newValue: any | null) => {
                  if (partOfForm) setFieldValue(id, newValue || '');
                  else setFieldValue(newValue || '');
               }}
               options={options}
               getOptionLabel={(option: any) => option || ''}
               style={style}
               renderInput={(params) => (
                  <TextField
                     className={styles.textField}
                     {...params}
                     value={value}
                     error={error}
                     helperText={helperText}
                     label={label}
                     type={type}
                     required
                     variant="outlined"
                     fullWidth
                  />
               )}
            />
         )}
      </>
   );
};
