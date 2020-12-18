import BsModal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import CircularProgress from '@material-ui/core/CircularProgress';
import {MenuItem} from '@material-ui/core';
import React, {useState} from 'react';

import {BusinessDTO} from '../dto/Business';
import {Form} from '../util/useForm';
import {TextField} from './TextField';

export type TextFieldType = 'text' | 'number' | 'time' | undefined;

type Props = {
   show: boolean;
   title: string;
   id: string;
   valueLabel: string;
   form: Form<BusinessDTO>;
   textFieldType: TextFieldType;
   primaryActionText?: string;
   selectOptions?: string[];
   primaryAction?: () => Promise<void>;
   secondaryAction: () => void;
};

export const TextFieldModal: React.FC<Props> = ({
   show,
   title,
   valueLabel,
   id,
   primaryAction,
   primaryActionText,
   secondaryAction,
   textFieldType,
   form,
   selectOptions,
}: Props) => {
   const [updating, setUpdating] = useState(false);

   return (
      <>
         <BsModal show={show}>
            <BsModal.Dialog>
               <BsModal.Header>
                  <BsModal.Title>{title}</BsModal.Title>
               </BsModal.Header>

               <BsModal.Body>
                  {updating && <CircularProgress />}
                  <TextField
                     id={id}
                     label={valueLabel}
                     type={textFieldType}
                     value={form.values[id]}
                     onChange={form.handleChange(id)}
                     onBlur={form.handleBlur}
                     error={form.touched[id] && Boolean(form.errors[id])}
                     helperText={form.touched[id] && form.errors[id]}
                     select={id === 'type' ? true : false}
                     inputProps={{
                        step: 1800,
                     }}
                  >
                     {id === 'type' &&
                        selectOptions!.map((x) => (
                           <MenuItem key={x} value={x}>
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
