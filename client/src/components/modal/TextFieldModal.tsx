import React, {useState} from 'react';
import BsModal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import CircularProgress from '@material-ui/core/CircularProgress';
import {MenuItem} from '@material-ui/core';

import {ComboBox, ComboBoxOption} from '../form/ComboBox';
import {FormHandler} from '../../hooks/useForm';
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
      <>
         <BsModal
            show={show}
            onHide={() => {
               formHandler.setFieldValue(textFieldKey, initialValue);
               showModal('');
            }}
         >
            <BsModal.Dialog>
               <BsModal.Header>
                  <BsModal.Title>{`Edit ${TextFieldUtil.mapKeyToLabel(
                     textFieldKey
                  )}`}</BsModal.Title>
               </BsModal.Header>

               <BsModal.Body>
                  {updating && <CircularProgress />}
                  {isComboBox ? (
                     <ComboBox
                        id={textFieldKey}
                        style={{width: '100%', marginLeft: 0}}
                        label={TextFieldUtil.mapKeyToLabel(textFieldKey)}
                        type="text"
                        options={comboBoxOptions || []}
                        onBlur={formHandler.handleBlur}
                        setFieldValue={(option: ComboBoxOption, formFieldId) =>
                           formHandler.setFieldValue(formFieldId, option.label)
                        }
                        error={
                           formHandler.touched[textFieldKey] &&
                           Boolean(formHandler.errors[textFieldKey])
                        }
                        helperText={
                           formHandler.touched[textFieldKey] &&
                           (formHandler.errors[textFieldKey] as any)
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
                           formHandler.touched[textFieldKey] &&
                           Boolean(formHandler.errors[textFieldKey])
                        }
                        helperText={
                           formHandler.touched[textFieldKey] &&
                           (formHandler.errors[textFieldKey] as any)
                        }
                        select={textFieldKey === 'type' ? true : false}
                        inputLabelProps={{
                           shrink:
                              textFieldKey === 'opens' || textFieldKey === 'closes'
                                 ? true
                                 : undefined,
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
               </BsModal.Body>

               <BsModal.Footer>
                  <Button
                     variant="secondary"
                     onClick={() => {
                        formHandler.setFieldValue(textFieldKey, initialValue);
                        showModal('');
                     }}
                  >
                     Close
                  </Button>
                  {primaryAction && (
                     <Button
                        disabled={!formHandler.isValid}
                        variant="primary"
                        onClick={async () => {
                           setUpdating(true);
                           await primaryAction();

                           setUpdating(false);
                        }}
                     >
                        {primaryActionText}
                     </Button>
                  )}
               </BsModal.Footer>
            </BsModal.Dialog>
         </BsModal>
      </>
   );
};
