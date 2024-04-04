import React from 'react';
import {Map as PMap, Marker} from 'pigeon-maps';

export const COPENHAGEN_COORDS = [55.676098, 12.568337];
const MAP_ID = 'basic';
const MAPTILER_ACCESS_TOKEN = '3PmudqkY6MeEQb9jxr9h';

export const Map = ({markers, zoom, center}) => {
    const mapTilerProvider = (x, y, z, dpr) => {
        return `https://api.maptiler.com/maps/${MAP_ID}/256/${z}/${x}/${y}${
            dpr >= 2 ? '@2x' : ''
        }.png?key=${MAPTILER_ACCESS_TOKEN}`;
    };

    return (
        <PMap provider={mapTilerProvider} center={center} zoom={zoom} width={450} height={400}>
            {Object.keys(markers).map((key, index) => (
                <Marker key={key} anchor={markers[key][0]} payload={key} width={29 + 10 * index} />
            ))}
        </PMap>
    );
};
