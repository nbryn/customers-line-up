import ApiCaller from '../api/ApiCaller';

// TODO: Move to RTK Query?

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

export type Address = {
    city: string;
    zip: string;
    zipCity: string;
    street?: string;
    longitude?: number;
    latitude?: number;
};

export async function fetchZips(): Promise<Address[]> {
    const dawaZips = await ApiCaller.get<DawaZip[]>(DAWA_ZIP_URL);

    const zips = dawaZips.map((x) => ({
        city: x.navn,
        zip: x.nr,
        zipCity: `${x.nr} - ${x.navn}`,
    }));

    return zips;
}

export async function fetchAddresses(address: Address | undefined): Promise<Address[]> {
    if (address) {
        const dawaAddresses = await ApiCaller.get<DawaAddress[]>(getAddressUrl(address.zip));

        const addresses: Address[] = dawaAddresses.map((x) => ({
            ...address,
            street: `${x.vejnavn} ${x.husnr}`,
            longitude: x.y,
            latitude: x.x,
        }));

        return addresses;
    }
    
    return [];
}

export default {
    fetchAddresses,
    fetchZips,
};
