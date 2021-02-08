import React from 'react';

import {Map} from '../map/Map.js';
import {Modal} from './Modal';

export type Props = {
   visible: boolean;
   setVisible: (visible: boolean) => void;
};

export const MapModal: React.FC<Props> = ({visible, setVisible}: Props) => {
   return (
      <Modal show={visible} secondaryAction={() => setVisible(false)} title="Map">
         <Map />
      </Modal>
   );
};
