import {BaseService} from './BaseService';
import {UserInsights} from '../../features/user/User';
import {useApi} from '../api/useApi';

const defaultRoute = 'insights';

export interface InsightsService extends BaseService {
    fetchUserInsights: () => Promise<UserInsights>;
}

export function useInsightsService(succesMessage?: string): InsightsService {
    const apiCaller = useApi(succesMessage);

    const fetchUserInsights = (): Promise<UserInsights> => {
        return apiCaller.get(`${defaultRoute}/user`);
    };

    const setRequestInfo = (info: string) => apiCaller.setRequestInfo(info);

    return {
        fetchUserInsights,
        setRequestInfo,
        requestInfo: apiCaller.requestInfo,
        working: apiCaller.working,
    };
}
