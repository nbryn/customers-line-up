import BsModal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import CircularProgress from '@material-ui/core/CircularProgress';
import {MenuItem} from '@material-ui/core';
import React, {useState} from 'react';

import {BusinessDTO} from '../dto/Business';
import {Form} from '../validation/useForm';
import {TextField} from './TextField';
import TextFieldUtil from '../util/TextFieldUtil';

export type TextFieldType = 'text' | 'number' | 'time' | 'password' | undefined;

type Props = {
   show: boolean;
   textFieldKey: string;
   form: Form<BusinessDTO>;
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
   form,
   selectOptions,
   showModal,
}: Props) => {
   const [updating, setUpdating] = useState(false);

   console.log(textFieldKey);

   return (
      <>
         <BsModal show={show} onHide={() => showModal('')}>
            <BsModal.Dialog>
               <BsModal.Header>
                  <BsModal.Title>{`Edit ${TextFieldUtil.getLabelFromDTOKey(
                     textFieldKey
                  )}`}</BsModal.Title>
               </BsModal.Header>

               <BsModal.Body>
                  {updating && <CircularProgress />}
                  <TextField
                     id={textFieldKey}
                     label={TextFieldUtil.getLabelFromDTOKey(textFieldKey)}
                     type={textFieldType}
                     value={form.values[textFieldKey]}
                     onChange={form.handleChange(textFieldKey)}
                     onBlur={form.handleBlur}
                     error={form.touched[textFieldKey] && Boolean(form.errors[textFieldKey])}
                     helperText={form.touched[textFieldKey] && form.errors[textFieldKey]}
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
                        disabled={!form.isValid}
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
