import { BASE_URL } from './Url';
import ApiService from './ApiService';
import { BusinessDTO } from '../models/dto/Business';

async function fetchAllBusinesses(): Promise<BusinessDTO[]> {
    const businesses: BusinessDTO[] = await ApiService.fetch<BusinessDTO[]>(BASE_URL + 'business/all', 'get');

    return businesses;
}

async function fetchBusinessesForOwner(ownerEmail: string): Promise<BusinessDTO[]> {
    const businesses: BusinessDTO[] = await ApiService.fetch<BusinessDTO[]>(BASE_URL + `business/owner?email=${ownerEmail}`, 'get')

    return businesses;
}

async function createBusiness(business: BusinessDTO): Promise<void> {
    business.opens = business.opens.replace(':', '.');
    business.closes = business.closes.replace(':', '.');

    await ApiService.fetch(BASE_URL + 'business/create', 'post', business);
}



export default {
    createBusiness,
    fetchAllBusinesses,
    fetchBusinessesForOwner,
};