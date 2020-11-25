
import { BASE_URL } from './Url';
import { fetchFromServer } from './Fetch';
import { BusinessDTO, BusinessQueueDTO } from '../models/dto/Business';

async function fetchAllBusinesses(): Promise<BusinessDTO[]> {
    const businesses: BusinessDTO[] = await fetchFromServer<BusinessDTO[]>(BASE_URL + 'business/all', 'get');

    return businesses;
}

async function fetchBusinessesForOwner(ownerEmail: string): Promise<BusinessDTO[]> {
    const businesses: BusinessDTO[] = await fetchFromServer<BusinessDTO[]>(BASE_URL + `business/owner?email=${ownerEmail}`, 'get')

    return businesses;
}

async function createBusiness(business: BusinessDTO): Promise<void> {
    business.opens = business.opens.replace(':', '.');
    business.closes = business.closes.replace(':', '.');

    await fetchFromServer(BASE_URL + 'business/create', 'post', business);
}

async function fetchAvailableQueuesForBusiness(businessId: number): Promise<BusinessQueueDTO[]> {
    const today = new Date();
    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 3);

    const start = today.toISOString().substring(0, 10);
    const end = tomorrow.toISOString().substring(0, 10);

    const queues: BusinessQueueDTO[] = await fetchFromServer<BusinessQueueDTO[]>(BASE_URL + `businessqueue/available?businessid=${businessId}&start=${start}&end=${end}`, 'get');

    console.log(queues);

    return queues;
}

export default {
    createBusiness,
    fetchAllBusinesses,
    fetchAvailableQueuesForBusiness,
    fetchBusinessesForOwner
};