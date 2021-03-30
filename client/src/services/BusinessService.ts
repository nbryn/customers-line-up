import {BaseService} from './BaseService';
import {BusinessDTO} from '../models/Business';
import {useApi} from '../api/useApi';

const defaultRoute = 'business';

export interface BusinessService extends BaseService {
    createBusiness: (data: BusinessDTO) => Promise<BusinessDTO>;
    fetchAllBusinesses: () => Promise<BusinessDTO[]>;
    fetchBusinesssesByOwner: () => Promise<BusinessDTO[]>;
    fetchBusinessTypes: () => Promise<string[]>;
    updateBusinessInfo: (businessId: number) => (data: BusinessDTO) => Promise<BusinessDTO>;
}

export function useBusinessService(successMessage?: string): BusinessService {
    const apiCaller = useApi(successMessage);

    const createBusiness = async (data: BusinessDTO): Promise<BusinessDTO> => {
        return await apiCaller.post(`${defaultRoute}`, data);
    };

    const fetchAllBusinesses = async (): Promise<BusinessDTO[]> => {
        return await apiCaller.get(`${defaultRoute}/all`);
    };

    const fetchBusinesssesByOwner = async (): Promise<BusinessDTO[]> => {
        return await apiCaller.get(`${defaultRoute}/owner`);
    };

    const fetchBusinessTypes = async (): Promise<string[]> => {
        return await apiCaller.get(`${defaultRoute}/types`);
    };

    const updateBusinessInfo = (businessId: number) => async (
        data: BusinessDTO
    ): Promise<BusinessDTO> => {
        return await apiCaller.put(`${defaultRoute}/${businessId}`, data);
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
