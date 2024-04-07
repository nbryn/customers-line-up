import React from 'react';

import {COPENHAGEN_COORDS, Map} from '../map/Map.js';
import {Dialog} from './Dialog';

export type Marker = Array<number[] | number>;

export type MapModalProps = {
    center: number[];
    markers: Marker;
    zoom: number;
    open: boolean;
    title?: string;
    setVisible?: () => void;
};

export const defaultMapProps: MapModalProps = {
    open: false,
    center: COPENHAGEN_COORDS,
    zoom: 10,
    markers: [COPENHAGEN_COORDS, 0],
};

export const MapModal: React.FC<MapModalProps> = ({
    open,
    zoom,
    center,
    markers,
    title,
    setVisible,
}: MapModalProps) => {
    return (
        <Dialog open={open} secondaryAction={() => setVisible!()} title={title ?? 'Map'}>
            <Map center={center} markers={markers} zoom={zoom} />
        </Dialog>
    );
};
