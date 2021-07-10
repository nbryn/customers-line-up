import {createAsyncThunk, createSlice, PayloadAction} from '@reduxjs/toolkit';

import {NormalizedEntity, RootState, useLoading} from '../../app/Store';
import {EmployeeDTO} from './Employee';
import EmployeeService from './EmployeeService';

const initialState: NormalizedEntity<EmployeeDTO> = {
    byId: {},
    allIds: [],
};

export const createEmployee = createAsyncThunk(
    'employee/createEmployee',
    async (data: EmployeeDTO) => {
        useLoading(async () => EmployeeService.createEmployee(data));
    }
);

type Request<T1 = void> = {
    id: string;
    data?: T1;
};

export const deleteEmployee = createAsyncThunk(
    'employee/deleteEmployee',
    async ({id, data}: Request<string>) => {
        useLoading(async () => EmployeeService.removeEmployee(data!, id));

        return id;
    }
);

export const fetchEmployeesByBusiness = createAsyncThunk(
    'employee/fetchEmployeesByBusiness',
    async (businessId: string) => {
        const response = await useLoading(
            async () => await EmployeeService.fetchEmployeesByBusiness(businessId)
        );


        return response!;
    }
);

export const employeeSlice = createSlice({
    name: 'employee',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(
            fetchEmployeesByBusiness.fulfilled,
            (state, action: PayloadAction<EmployeeDTO[]>) => {
                const newState = {...state.byId};
                action.payload.forEach((employee) => (newState[employee.id] = employee));

                state.byId = newState;
            }
        );
        builder.addCase(deleteEmployee.fulfilled, (state, action) => {
            delete state.byId[action.payload];
        });
    },
});

export const selectEmployeesByBusiness = (state: RootState, businessId: string) =>
    Object.values(state.employees.byId).filter((employee) => employee.businessId === businessId);

export default employeeSlice.reducer;
