import BsModal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import CircularProgress from '@material-ui/core/CircularProgress';
import React, {useState} from 'react';

import {TextField} from './TextField';

export type TextFieldType = 'text' | 'number' | 'time' | undefined;

type Props = {
   show: boolean;
   title: string;
   value: string | number;
   valueLabel: string;
   textFieldType: TextFieldType;
   setValue: (value: string) => void;
   primaryAction?: () => Promise<void>;
   primaryActionText?: string;
   secondaryAction: () => void;
};

export const TextFieldModal: React.FC<Props> = ({
   show,
   title,
   valueLabel,
   value,
   setValue,
   primaryAction,
   primaryActionText,
   secondaryAction,
   textFieldType,
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
                     id={valueLabel}
                     label={valueLabel}
                     type={textFieldType}
                     value={value}
                     onChange={(e) => setValue(e.target.value)}
                  />
               </BsModal.Body>

               <BsModal.Footer>
                  <Button variant="secondary" onClick={secondaryAction}>
                     Close
                  </Button>
                  {primaryAction && (
                     <Button
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
