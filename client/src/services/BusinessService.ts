import {BaseService} from './BaseService';
import {BusinessDTO} from '../models/Business';
import {useApi} from '../api/useApi';

const defaultRoute = 'business';

export interface BusinessService extends BaseService {
    createBusiness: (data: BusinessDTO) => Promise<BusinessDTO>;
    fetchAllBusinesses: () => Promise<BusinessDTO[]>;
    fetchBusinesssesByOwner: () => Promise<BusinessDTO[]>;
    fetchBusinessTypes: () => Promise<string[]>;
    updateBusinessInfo: (businessId: string, ownerEmail: string) => (data: BusinessDTO) => Promise<BusinessDTO>;
}

export function useBusinessService(successMessage?: string): BusinessService {
    const apiCaller = useApi(successMessage);

    const createBusiness = (data: BusinessDTO): Promise<BusinessDTO> => {
        return apiCaller.post(`${defaultRoute}`, data);
    };

    const fetchAllBusinesses = (): Promise<BusinessDTO[]> => {
        return apiCaller.get(`${defaultRoute}/all`);
    };

    const fetchBusinesssesByOwner = (): Promise<BusinessDTO[]> => {
        return apiCaller.get(`${defaultRoute}/owner`);
    };

    const fetchBusinessTypes = (): Promise<string[]> => {
        return apiCaller.get(`${defaultRoute}/types`);
    };

    const updateBusinessInfo = (businessId: string, ownerEmail: string) => (
        data: BusinessDTO
    ): Promise<BusinessDTO> => {
        return apiCaller.put(`${defaultRoute}/${businessId}`, {...data, ownerEmail});
    };

    const setRequestInfo = (info: string) => apiCaller.setRequestInfo(info);

    return {
        createBusiness,
        fetchAllBusinesses,
        fetchBusinesssesByOwner,
        fetchBusinessTypes,
        updateBusinessInfo,
        setRequestInfo,
        requestInfo: apiCaller.requestInfo,
        working: apiCaller.working,
    };
}
