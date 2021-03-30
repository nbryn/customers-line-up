import {BaseService} from './BaseService';
import {UserDTO} from '../models/User';
import {useApi} from '../api/useApi';

const defaultRoute = 'user';

export interface UserService extends BaseService {
    fetchAllUsersNotEmployedByBusiness: (businessId: number) => Promise<UserDTO[]>;
    fetchUserInfo: () => Promise<UserDTO>;
}

export function useUserService(succesMessage?: string): UserService {
    const apiCaller = useApi(succesMessage);

    const fetchAllUsersNotEmployedByBusiness = (businessId: number): Promise<UserDTO[]> => {
        return apiCaller.get(`${defaultRoute}/all/${businessId}`);
    };

    const fetchUserInfo = (): Promise<UserDTO> => {
        return apiCaller.get(`${defaultRoute}`);
    };

    const setRequestInfo = (info: string) => apiCaller.setRequestInfo(info);

    return {
        fetchAllUsersNotEmployedByBusiness,
        fetchUserInfo,
        setRequestInfo,
        requestInfo: apiCaller.requestInfo,
        working: apiCaller.working,
    };
}
