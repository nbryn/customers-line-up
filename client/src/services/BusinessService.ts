import ApiService from './ApiService';
import { BusinessDTO } from '../models/dto/Business';

async function createBusiness(business: BusinessDTO): Promise<void> {
    business.opens = business.opens.replace(':', '.');
    business.closes = business.closes.replace(':', '.');

    //await ApiService.fetch(BASE_URL + 'business/create', 'post', business);
}

export default {
    createBusiness,
};