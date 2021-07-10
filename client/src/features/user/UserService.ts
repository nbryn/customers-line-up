import {BaseService} from '../../common/services/BaseService';
import {UserDTO, NotEmployedByBusiness, UserInsights} from './User';
import {useApi} from '../../common/api/useApi';

const defaultRoute = 'user';

export interface UserService extends BaseService {
    fetchAllUsersNotEmployedByBusiness: (businessId: string) => Promise<NotEmployedByBusiness>;
    fetchUserInfo: () => Promise<UserDTO>;
    fetchUserInsights: () => Promise<UserInsights>;
}

export function useUserService(succesMessage?: string): UserService {
    const apiCaller = useApi(succesMessage);

    const fetchAllUsersNotEmployedByBusiness = (
        businessId: string
    ): Promise<NotEmployedByBusiness> => {
        return apiCaller.get(`${defaultRoute}/all/${businessId}`);
    };

    const fetchUserInfo = (): Promise<UserDTO> => {
        return apiCaller.get(`${defaultRoute}`);
    };

    const fetchUserInsights = (): Promise<UserInsights> => {
        return apiCaller.get(`${defaultRoute}/insights`);
    };

    const setRequestInfo = (info: string) => apiCaller.setRequestInfo(info);

    return {
        fetchAllUsersNotEmployedByBusiness,
        fetchUserInfo,
        fetchUserInsights,
        setRequestInfo,
        requestInfo: apiCaller.requestInfo,
        working: apiCaller.working,
    };
}
