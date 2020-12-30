import BsModal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import CircularProgress from '@material-ui/core/CircularProgress';
import {MenuItem} from '@material-ui/core';
import React, {useState} from 'react';

import {BusinessDTO} from '../../models/Business';
import {FormHandler} from '../../hooks/useForm';
import {TextField, TextFieldType} from '../form/TextField';
import TextFieldUtil from '../../util/TextFieldUtil';

type Props = {
   show: boolean;
   textFieldKey: string;
   formHandler: FormHandler<BusinessDTO>;
   textFieldType: TextFieldType;
   primaryActionText?: string;
   selectOptions?: string[];
   primaryAction?: () => Promise<void>;
   secondaryAction: () => void;
   showModal: (value: string) => void;
};

export const TextFieldModal: React.FC<Props> = ({
   show,
   textFieldKey,
   primaryAction,
   primaryActionText,
   secondaryAction,
   textFieldType,
   formHandler,
   selectOptions,
   showModal,
}: Props) => {
   const [updating, setUpdating] = useState(false);

   return (
      <>
         <BsModal show={show} onHide={() => showModal('')}>
            <BsModal.Dialog>
               <BsModal.Header>
                  <BsModal.Title>{`Edit ${TextFieldUtil.mapKeyToLabel(
                     textFieldKey
                  )}`}</BsModal.Title>
               </BsModal.Header>

               <BsModal.Body>
                  {updating && <CircularProgress />}
                  <TextField
                     id={textFieldKey}
                     label={TextFieldUtil.mapKeyToLabel(textFieldKey)}
                     type={textFieldType}
                     value={formHandler.values[textFieldKey]}
                     onChange={formHandler.handleChange(textFieldKey)}
                     onBlur={formHandler.handleBlur}
                     error={formHandler.touched[textFieldKey] && Boolean(formHandler.errors[textFieldKey])}
                     helperText={formHandler.touched[textFieldKey] && formHandler.errors[textFieldKey]}
                     select={textFieldKey === 'type' ? true : false}
                     inputProps={{
                        step: 1800,
                     }}
                  >
                     {textFieldKey === 'type' &&
                        selectOptions!.map((x, index) => (
                           <MenuItem key={index} value={x}>
                              {x}
                           </MenuItem>
                        ))}
                  </TextField>
               </BsModal.Body>

               <BsModal.Footer>
                  <Button variant="secondary" onClick={secondaryAction}>
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
