import React from 'react';
import Autocomplete from '@material-ui/lab/Autocomplete';
import CircularProgress from '@material-ui/core/CircularProgress';
import {makeStyles} from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';

const useStyles = makeStyles((theme) => ({ 
   wrapper: {
      marginLeft: 110,
   },
}));

export type Props = {
   label: string;
   options: Array<any>;
   setChosenValue: (value: any) => void;
};

export const ComboBox: React.FC<Props> = ({label, options, setChosenValue}: Props) => {
   const styles = useStyles();
   return (
      <>
         {options.length === 0 ? (
            <CircularProgress />
         ) : (
            <Autocomplete
            className={styles.wrapper}
               id="combo-box"
               onChange={(event: any, newValue: any | null) => setChosenValue(newValue)}
               options={options}
               getOptionLabel={(option: any) => option.email}
               style={{width: 300}}
               renderInput={(params) => <TextField {...params} label={label} variant="outlined" />}
            />
         )}
      </>
   );
};
