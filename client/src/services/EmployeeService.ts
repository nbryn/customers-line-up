import {BaseService} from './BaseService';
import {EmployeeDTO} from '../models/Employee';
import {useApi} from '../api/useApi';

const defaultRoute = 'employee';

export interface EmployeeService extends BaseService {
    createEmployee: (data: EmployeeDTO) => Promise<void>;
    fetchEmployeesByBusiness: (businessId: number) => Promise<EmployeeDTO[]>;
    removeEmployee: (employeeEmail: string, businessId: number) => Promise<void>;
}

export function useEmployeeService(succesMessage?: string): EmployeeService {
    const apiCaller = useApi(succesMessage);

    const createEmployee = async (data: EmployeeDTO): Promise<void> => {
        return await apiCaller.post(`${defaultRoute}`, data);
    };

    const fetchEmployeesByBusiness = async (businessId: number): Promise<EmployeeDTO[]> => {
        return await apiCaller.get(`${defaultRoute}/business/${businessId}`);
    };

    const removeEmployee = async (email: string, businessId: number): Promise<void> => {
        await apiCaller.remove(`${defaultRoute}/${email}?businessId=${businessId}`);
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
