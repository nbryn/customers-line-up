import {createAsyncThunk} from '@reduxjs/toolkit';

import ApiCaller from '../../../shared/api/ApiCaller';
import {callApiAndFetchUser} from '../../user/UserState';
import type {EmployeeDTO} from './Employee';
import type {RootState} from '../../../app/Store';
import {selectBusinessesByOwner} from '../BusinessState';
import type {ThunkParam} from '../../../app/AppTypes';

const DEFAULT_EMPLOYEE_COMMAND_ROUTE = 'api/business/employee';

export const createEmployee = createAsyncThunk(
    'employee/create',
    async (data: EmployeeDTO, thunkAPI) => {
        callApiAndFetchUser(
            thunkAPI,
            async () => await ApiCaller.post(`${DEFAULT_EMPLOYEE_COMMAND_ROUTE}`, data)
        );
    }
);

export const removeEmployee = createAsyncThunk(
    'employee/delete',
    async ({id, data}: ThunkParam<string>, thunkAPI) => {
        callApiAndFetchUser(
            thunkAPI,
            async () =>
                await ApiCaller.remove(`${DEFAULT_EMPLOYEE_COMMAND_ROUTE}/${data}?businessId=${id}`)
        );

        return id;
    }
);

const selectAllEmployees = (state: RootState) => Object.values(state.entities.employees);

export const selectEmployeesByBusiness = (state: RootState) =>
    selectAllEmployees(state).filter(
        (employee) => employee.businessId === state.business.current?.id
    );

export const selectEmployeesByOwner = (state: RootState) =>
    selectBusinessesByOwner(state).flatMap((business) => business.employees);
