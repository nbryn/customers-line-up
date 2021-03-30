import {BaseService} from './BaseService';
import {useApi} from '../api/useApi';
import {UserDTO} from '../models/User';

const defaultRoute = 'user';

export interface AuthService extends BaseService {
    login: (data: UserDTO) => Promise<UserDTO>;
    register: (data: UserDTO) => Promise<UserDTO>;
}

export function useAuthService(): AuthService {
    const apiCaller = useApi();

    const login = async (data: UserDTO): Promise<UserDTO> => {
        return await apiCaller.post(`${defaultRoute}/login`, data);
    };

    const register = async (data: UserDTO): Promise<UserDTO> => {
        return await apiCaller.post(`${defaultRoute}/register`, data);
    };

    const setRequestInfo = (info: string) => apiCaller.setRequestInfo(info);

    return {
        login,
        register,
        setRequestInfo,
        requestInfo: apiCaller.requestInfo,
        working: apiCaller.working,
    };
}
