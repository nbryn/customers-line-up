import {createAsyncThunk, createSlice, PayloadAction} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/ApiCaller';
import {EmployeeDTO} from './Employee';
import {NormalizedEntityState, ThunkParam} from '../../app/AppTypes';
import {RootState} from '../../app/Store';

const DEFAULT_EMPLOYEE_ROUTE = 'employee';

const initialState: NormalizedEntityState<EmployeeDTO> = {
    byId: {},
    allIds: [],
};

export const createEmployee = createAsyncThunk('employee/create', async (data: EmployeeDTO) => {
    await ApiCaller.post(`${DEFAULT_EMPLOYEE_ROUTE}`, data);
});

export const deleteEmployee = createAsyncThunk(
    'employee/delete',
    async ({id, data}: ThunkParam<string>) => {
        await ApiCaller.remove(`${DEFAULT_EMPLOYEE_ROUTE}/${data}?businessId=${id}`);

        return id;
    }
);

export const fetchEmployeesByBusiness = createAsyncThunk(
    'employee/fetchByBusiness',
    async (businessId: string) => {
        const response = await ApiCaller.get<EmployeeDTO[]>(
            `${DEFAULT_EMPLOYEE_ROUTE}/business/${businessId}`
        );

        return response;
    }
);

export const employeeSlice = createSlice({
    name: 'employee',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(deleteEmployee.fulfilled, (state, action) => {
            delete state.byId[action.payload];
        });

        builder.addCase(
            fetchEmployeesByBusiness.fulfilled,
            (state, action: PayloadAction<EmployeeDTO[]>) => {
                const newState = {...state.byId};
                action.payload.forEach((employee) => (newState[employee.id] = employee));
                state.byId = newState;
            }
        );
    },
});

export const selectEmployeesByBusiness = (state: RootState, businessId: string) =>
    Object.values(state.employees.byId).filter((employee) => employee.businessId === businessId);

export default employeeSlice.reducer;
