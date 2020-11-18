import { BASE_URL } from './Url';
import { fetchFromServer, setTokenInHeader } from './Fetch';
import { BusinessDTO } from './dto/Business';

async function fetchAllBusinesses(token: string): Promise<BusinessDTO[]> {
    setTokenInHeader(token);

    const businesses: BusinessDTO[] = await fetchFromServer<BusinessDTO[]>(BASE_URL + 'business/all', 'get');

    return businesses;
}

export default {
    fetchAllBusinesses
}