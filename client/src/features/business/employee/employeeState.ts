import {createAsyncThunk} from '@reduxjs/toolkit';

import ApiCaller from '../../../shared/api/ApiCaller';
import {EmployeeDTO} from './Employee';
import {NormalizedEntityState, ThunkParam} from '../../../app/AppTypes';
import {RootState} from '../../../app/Store';

const DEFAULT_EMPLOYEE_ROUTE = 'business';

export const initialEmployeeState: NormalizedEntityState<EmployeeDTO> = {
    byId: {},
    allIds: [],
};

export const createEmployee = createAsyncThunk('employee/create', async (data: EmployeeDTO) => {
    await ApiCaller.post(`${DEFAULT_EMPLOYEE_ROUTE}/employee/`, data);
});

export const deleteEmployee = createAsyncThunk(
    'employee/delete',
    async ({id, data}: ThunkParam<string>) => {
        await ApiCaller.remove(`${DEFAULT_EMPLOYEE_ROUTE}/employee/${data}?businessId=${id}`);

        return id;
    }
);

export const fetchEmployeesByBusiness = createAsyncThunk(
    'employee/fetchByBusiness',
    async (businessId: string) => {
        const response = await ApiCaller.get<EmployeeDTO[]>(
            `query/${DEFAULT_EMPLOYEE_ROUTE}/employee/${businessId}`
        );

        return response;
    }
);

export const selectEmployeesByBusiness = (state: RootState, businessId: string) =>
    Object.values(state.businesses.employees.byId).filter((employee) => employee.businessId === businessId);

