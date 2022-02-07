import {createAsyncThunk} from '@reduxjs/toolkit';

import ApiCaller from '../../../shared/api/ApiCaller';
import {EmployeeDTO} from './Employee';
import {ThunkParam} from '../../../app/AppTypes';
import {RootState} from '../../../app/Store';

const DEFAULT_EMPLOYEE_COMMAND_ROUTE = 'api/business/employee';

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

const getEmployees = (state: RootState) => Object.values(state.entities.employees);

export const selectEmployeesByBusiness = (state: RootState) =>
    getEmployees(state).filter((employee) => employee.businessId === state.business.current?.id);
