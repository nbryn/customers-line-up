import React from 'react';
import Autocomplete from '@material-ui/lab/Autocomplete';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';

//import {TextField} from './TextField'

const useStyles = makeStyles((theme) => ({
   textField: {
      width: '75%',
      textAlign: 'center',
   },
}));

export type Props = {
   label: string;
   options: Array<any>;
   setChosenValue: (value: any) => void;
   style: any;
};

export const ComboBox: React.FC<Props> = ({label, options, style, setChosenValue}: Props) => {
   const styles = useStyles();
   return (
      <>
         {options.length === 0 ? (
            <CircularProgress />
         ) : (
            <Autocomplete
               className={style}
               id="combo-box"
               onChange={(event: any, newValue: any | null) => setChosenValue(newValue)}
               options={options}
               getOptionLabel={(option: any) => option.email}
               style={{width: 300}}
               renderInput={(params) => (
                  <TextField
                     className={styles.textField}
                     {...params}
                     label={label}
                     variant="outlined"
                  />
               )}
            />
         )}
      </>
   );
};
