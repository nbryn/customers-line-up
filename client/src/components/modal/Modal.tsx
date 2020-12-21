import BsModal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import React from 'react';

type Props = {
   show: boolean;
   title: string;
   text: string;
   primaryAction?: () => void;
   primaryActionText?: string;
   secondaryAction: () => void;
};

export const Modal: React.FC<Props> = ({
   show,
   title,
   text,
   primaryAction,
   primaryActionText,
   secondaryAction,
}: Props) => {
   return (
      <>
         <BsModal show={show}>
            <BsModal.Dialog>
               <BsModal.Header >
                  <BsModal.Title>{title}</BsModal.Title>
               </BsModal.Header>

               <BsModal.Body>
                  <p>{text}</p>
               </BsModal.Body>

               <BsModal.Footer>
                  <Button variant="secondary" onClick={secondaryAction}>
                     Close
                  </Button>
                  {primaryAction && (
                     <Button variant="primary" onClick={primaryAction}>
                        {primaryActionText}
                     </Button>
                  )}
               </BsModal.Footer>
            </BsModal.Dialog>
         </BsModal>
      </>
   );
};
