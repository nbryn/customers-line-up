import React, {useState} from 'react';
import Autocomplete from '@material-ui/lab/Autocomplete';
import CircularProgress from '@material-ui/core/CircularProgress';
import TextField from '@material-ui/core/TextField';

export type Props = {
   label: string;
   options: Array<any>;
   setChosenValue: (value: any) => void;
};

export const ComboBox: React.FC<Props> = ({label, options, setChosenValue}: Props) => {
   const [value, setValue] = useState<string[]>();

   return (
      <>
         {options.length === 0 ? (
            <CircularProgress />
         ) : (
            <Autocomplete
               id="combo-box"
               value={value}
               onChange={(event: any, newValue: any | null) => {
                  console.log(newValue);
                  setValue(newValue);

                  setChosenValue(newValue);
                }}
               options={options}
               getOptionLabel={(option: any) => option.email}
               style={{width: 300}}
               renderInput={(params) => <TextField {...params} label={label} variant="outlined" />}
            />
         )}
      </>
   );
};
