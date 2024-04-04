import React from 'react';

import {COPENHAGEN_COORDS, Map} from '../map/Map.js';
import {BsModal} from './BsModal';

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
    center: COPENHAGEN_COORDS,
    zoom: 10,
    markers: [COPENHAGEN_COORDS, 0],
};

export const MapModal: React.FC<MapModalProps> = ({
    visible,
    setVisible,
    zoom,
    center,
    markers,
}: MapModalProps) => {
    return (
        <BsModal show={visible} secondaryAction={() => setVisible!()} title="Map">
            <Map center={center} markers={markers} zoom={zoom} />
        </BsModal>
    );
};
