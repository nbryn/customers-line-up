import {EmployeeDTO} from './Employee';
import {get, post, remove} from '../../common/api/useApi';

const defaultRoute = 'employee';

const EMPLOYEE_CREATED = 'Employee Created - Go to my employees to see your employees';

const createEmployee = (data: EmployeeDTO): Promise<void> => {
    return post(`${defaultRoute}`, data, EMPLOYEE_CREATED);
};

const fetchEmployeesByBusiness = (businessId: string): Promise<EmployeeDTO[]> => {
    return get(`${defaultRoute}/business/${businessId}`);
};

const removeEmployee = (email: string, businessId: string): void => {
    remove(`${defaultRoute}/${email}?businessId=${businessId}`);
};

export default {
    createEmployee,
    fetchEmployeesByBusiness,
    removeEmployee,
};
