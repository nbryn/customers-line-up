
import { BASE_URL } from './Url';
import { fetchFromServer } from './Fetch';
import { BusinessDTO } from './dto/Business';

async function fetchAllBusinesses(): Promise<BusinessDTO[]> {
    const businesses: BusinessDTO[] = await fetchFromServer<BusinessDTO[]>(BASE_URL + 'business/all', 'get');

    return businesses;
}

async function fetchBusinessesForOwner(ownerEmail: string): Promise<BusinessDTO[]> {
    const businesses: BusinessDTO[] = await fetchFromServer<BusinessDTO[]>(BASE_URL + `business/owner?email=${ownerEmail}`, 'get')

    return businesses;
}

async function createBusiness(business: BusinessDTO): Promise<void> {
    console.log(business);
    await fetchFromServer(BASE_URL + 'business/create', 'post', business);
}

export default {
    createBusiness,
    fetchAllBusinesses,
    fetchBusinessesForOwner
};