import ApiCaller from '../api/ApiCaller';

const DAWA_ZIP_URL = 'https://dawa.aws.dk/postnumre?landpostnumre&struktur=mini';

const getAddressUrl = (zip: string) =>
    `https://dawa.aws.dk/adgangsadresser?struktur=mini&postnr=${zip}`;

type DawaZip = {
    nr: string;
    navn: string;
};

type DawaAddress = {
    vejnavn: string;
    husnr: string;
    x: number;
    y: number;
};

type Address = {
    address: string;
    longitude: number;
    latitude: number;
};

export async function fetchZips(): Promise<string[]> {
    const dawaZips = await ApiCaller.get<DawaZip[]>(DAWA_ZIP_URL);

    const zips = dawaZips.map((x) => `${x.nr} - ${x.navn}`);

    return zips;
}

export async function fetchAddresses(zip: string): Promise<Address[]> {
    const dawaAddresses = await ApiCaller.get<DawaAddress[]>(getAddressUrl(zip));

    const addresses: Address[] = dawaAddresses.map((x) => ({
        address: `${x.vejnavn} ${x.husnr}`,
        longitude: x.y,
        latitude: x.x,
    }));

    return addresses;
}

export default {
    fetchAddresses,
    fetchZips,
};
