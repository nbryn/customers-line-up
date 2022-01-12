import {createAsyncThunk} from '@reduxjs/toolkit';

import ApiCaller from '../../../shared/api/ApiCaller';
import {EmployeeDTO} from './Employee';
import {NormalizedEntityState, ThunkParam} from '../../../app/AppTypes';
import {RootState} from '../../../app/Store';

const DEFAULT_EMPLOYEE_QUERY_ROUTE = 'api/query/business';
const DEFAULT_EMPLOYEE_COMMAND_ROUTE = 'api/business/employee';

export const initialEmployeeState: NormalizedEntityState<EmployeeDTO> = {
    byId: {},
    allIds: [],
};

export const createEmployee = createAsyncThunk('employee/create', async (data: EmployeeDTO) => {
    await ApiCaller.post(`${DEFAULT_EMPLOYEE_COMMAND_ROUTE}`, data);
});

export const removeEmployee = createAsyncThunk(
    'employee/delete',
    async ({id, data}: ThunkParam<string>) => {
        await ApiCaller.remove(`${DEFAULT_EMPLOYEE_COMMAND_ROUTE}/${data}?businessId=${id}`);

        return id;
    }
);

export const fetchEmployeesByBusiness = createAsyncThunk(
    'employee/fetchByBusiness',
    async (businessId: string) => {
        const response = await ApiCaller.get<EmployeeDTO[]>(
            `${DEFAULT_EMPLOYEE_QUERY_ROUTE}/${businessId}/employee`
        );

        return response;
    }
);

export const selectEmployeesByBusiness = (state: RootState, businessId: string) =>
    Object.values(state.businesses.employees.byId).filter((employee) => employee.businessId === businessId);

