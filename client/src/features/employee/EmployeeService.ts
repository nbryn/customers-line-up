import {BaseService} from '../../common/services/BaseService';
import {EmployeeDTO} from './Employee';
import {useApi} from '../../common/api/useApi';

const defaultRoute = 'employee';

export interface EmployeeService extends BaseService {
    createEmployee: (data: EmployeeDTO) => Promise<void>;
    fetchEmployeesByBusiness: (businessId: string) => Promise<EmployeeDTO[]>;
    removeEmployee: (employeeEmail: string, businessId: string) => void;
}

export function useEmployeeService(succesMessage?: string): EmployeeService {
    const apiCaller = useApi(succesMessage);

    const createEmployee = (data: EmployeeDTO): Promise<void> => {
        return apiCaller.post(`${defaultRoute}`, data);
    };

    const fetchEmployeesByBusiness = (businessId: string): Promise<EmployeeDTO[]> => {
        return apiCaller.get(`${defaultRoute}/business/${businessId}`);
    };

    const removeEmployee = (email: string, businessId: string): void => {
        apiCaller.remove(`${defaultRoute}/${email}?businessId=${businessId}`);
    };

    const setRequestInfo = (info: string) => apiCaller.setRequestInfo(info);

    return {
        createEmployee,
        fetchEmployeesByBusiness,
        removeEmployee,
        setRequestInfo,
        requestInfo: apiCaller.requestInfo,
        working: apiCaller.working,
    };
}
