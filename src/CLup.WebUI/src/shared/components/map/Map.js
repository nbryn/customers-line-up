import React from 'react';
import {Map as PMap, Marker} from 'pigeon-maps';

const MAP_ID = 'basic';
const MAPTILER_ACCESS_TOKEN = 'DrQH8oH4Ah9vh5iA9B3Z';

export const Map = ({markers, zoom, center}) => {
  const mapTilerProvider = (x, y, z, dpr) => {
    return `https://api.maptiler.com/maps/${MAP_ID}/256/${z}/${x}/${y}${
      dpr >= 2 ? '@2x' : ''
    }.png?key=${MAPTILER_ACCESS_TOKEN}`;
  };

  const handleMarkerClick = () => {
    // console.log(`Marker #${payload} clicked at: `, anchor)
    // console.log(event)
  };

  return (
    <>
      <PMap provider={mapTilerProvider} center={center} zoom={zoom} width={450} height={400}>
        {Object.keys(markers).map((key, index) => (
          <Marker
            key={key}
            anchor={markers[key][0]}
            payload={key}
            onClick={handleMarkerClick}
            width={29 + 10 * index}
          />
        ))}
      </PMap>
    </>
  );
};
