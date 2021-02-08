import React, {useState} from 'react';
import CircularProgress from '@material-ui/core/CircularProgress';
import {MenuItem} from '@material-ui/core';

import {ComboBox, ComboBoxOption} from '../form/ComboBox';
import {FormHandler} from '../../hooks/useForm';
import {Modal} from './Modal';
import {TextField, TextFieldType} from '../form/TextField';
import TextFieldUtil from '../../util/TextFieldUtil';

type Props = {
   show: boolean;
   isComboBox?: boolean;
   comboBoxOptions?: ComboBoxOption[];
   textFieldKey: string;
   formHandler: FormHandler<any>;
   textFieldType: TextFieldType;
   initialValue: string | undefined;
   primaryActionText?: string;
   selectOptions?: string[];
   primaryAction?: () => Promise<void>;
   showModal: (value: string) => void;
};

export const TextFieldModal: React.FC<Props> = ({
   show,
   isComboBox = false,
   comboBoxOptions,
   textFieldKey,
   initialValue,
   primaryAction,
   primaryActionText,
   textFieldType,
   formHandler,
   selectOptions,
   showModal,
}: Props) => {
   const [updating, setUpdating] = useState(false);

   return (
      <Modal
         show={show}
         title={`Edit ${TextFieldUtil.mapKeyToLabel(textFieldKey)}`}
         primaryActionText={primaryActionText}
         primaryDisabled={!formHandler.isValid}
         primaryAction={async () => {
            setUpdating(true);
            await primaryAction!();

            setUpdating(false);
         }}
         secondaryAction={() => {
            formHandler.setFieldValue(textFieldKey, initialValue);
            showModal('');
         }}
      >
         {updating && <CircularProgress />}
         {isComboBox ? (
            <ComboBox
               id={textFieldKey}
               style={{width: '100%', marginLeft: 0}}
               label={TextFieldUtil.mapKeyToLabel(textFieldKey)}
               type="text"
               options={comboBoxOptions || []}
               onBlur={formHandler.handleBlur}
               setFieldValue={(option: ComboBoxOption, formFieldId: string) =>
                  formHandler.setFieldValue(formFieldId, option.label)
               }
               error={
                  formHandler.touched[textFieldKey] && Boolean(formHandler.errors[textFieldKey])
               }
               helperText={
                  formHandler.touched[textFieldKey] && (formHandler.errors[textFieldKey] as any)
               }
               defaultLabel={textFieldKey === 'address' ? 'Address - After Zip' : ''}
            />
         ) : (
            <TextField
               id={textFieldKey}
               label={TextFieldUtil.mapKeyToLabel(textFieldKey)}
               type={textFieldType}
               value={formHandler.values[textFieldKey] || ''}
               onChange={formHandler.handleChange(textFieldKey)}
               onBlur={formHandler.handleBlur}
               error={
                  formHandler.touched[textFieldKey] && Boolean(formHandler.errors[textFieldKey])
               }
               helperText={
                  formHandler.touched[textFieldKey] && (formHandler.errors[textFieldKey] as any)
               }
               select={textFieldKey === 'type' ? true : false}
               inputLabelProps={{
                  shrink: textFieldKey === 'opens' || textFieldKey === 'closes' ? true : undefined,
               }}
               inputProps={{
                  step: 1800,
               }}
            >
               {selectOptions &&
                  selectOptions.map((x, index) => (
                     <MenuItem key={index} value={x}>
                        {x}
                     </MenuItem>
                  ))}
            </TextField>
         )}
      </Modal>
   );
};
