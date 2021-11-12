import React from 'react';

import {Map} from '../map/Map.js';
import {Modal} from './Modal';

export type Marker = Array<number[] | number>;

export type MapModalProps = {
  center: number[];
  markers: Marker;
  zoom: number;
  visible: boolean;
  setVisible?: () => void;
};

export const defaultMapProps: MapModalProps = {
  visible: false,
  center: [0, 0],
  zoom: 10,
  markers: [[0, 0], 0],
};

export const MapModal: React.FC<MapModalProps> = ({
  visible,
  setVisible,
  zoom,
  center,
  markers,
}: MapModalProps) => {
  return (
    <Modal show={visible} secondaryAction={() => setVisible!()} title="Map">
      <Map center={center} markers={markers} zoom={zoom} />
    </Modal>
  );
};
