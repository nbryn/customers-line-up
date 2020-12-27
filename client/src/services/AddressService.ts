import { fetch } from '../api/Fetch'

export const DAWA_ZIP_URL = 'https://dawa.aws.dk/postnumre?landpostnumre';

const getAddressUrl = (zip: string) => `https://dawa.aws.dk/adgangsadresser?struktur=mini&postnr=${zip}`

type Zip = {
    nr: string;
    navn: string;
}

type Address = {
    vejnavn: string;
    husnr: string;
}

export async function fetchZips(): Promise<string[]> {
    const dawaZips: Zip[] = await fetch(DAWA_ZIP_URL, 'GET');

    const zips = dawaZips.map((x) => x.nr);

    return zips;
}

export async function fetchAddresses(zip: string): Promise<string[]> {
    const dawaAddresses: Address[] = await fetch(getAddressUrl(zip), 'GET');

    const addresses = dawaAddresses.map((x) => `${x.vejnavn} ${x.husnr}`);

    return addresses;
}

export default {
    fetchAddresses,
    fetchZips
}