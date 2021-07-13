import {createAsyncThunk, createSlice, isAnyOf, PayloadAction} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/useApi';
import {NormalizedEntityState, RootState, ThunkParam} from '../../app/Store';
import {EmployeeDTO} from './Employee';

const DEFAULT_EMPLOYEE_ROUTE = 'employee';
const EMPLOYEE_CREATED_MSG = 'Employee Created - Go to my employees to see your employees';

// Generic slice: https://redux-toolkit.js.org/usage/usage-with-typescript
const initialState: NormalizedEntityState<EmployeeDTO> = {
    byId: {},
    allIds: [],
    isLoading: false,
    apiMessage: '',
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
    reducers: {
        clearApiMessage: (state) => {
            state.apiMessage = '';
        },
    },
    extraReducers: (builder) => {
        builder.addCase(createEmployee.fulfilled, (state) => {
            state.isLoading = false;
            state.apiMessage = EMPLOYEE_CREATED_MSG;
        });

        builder.addCase(deleteEmployee.fulfilled, (state, action) => {
            state.isLoading = false;
            delete state.byId[action.payload];
        });

        builder.addCase(
            fetchEmployeesByBusiness.fulfilled,
            (state, action: PayloadAction<EmployeeDTO[]>) => {
                const newState = {...state.byId};
                action.payload.forEach((employee) => (newState[employee.id] = employee));

                state.byId = newState;
                state.isLoading = false;
            }
        );
        builder.addMatcher(
            isAnyOf(
                createEmployee.pending,
                deleteEmployee.pending,
                fetchEmployeesByBusiness.pending
            ),
            (state) => {
                state.isLoading = true;
            }
        );
        builder.addMatcher(
            isAnyOf(
                createEmployee.rejected,
                deleteEmployee.rejected,
                fetchEmployeesByBusiness.rejected
            ),
            (state, action) => {
                state.isLoading = false;
                state.apiMessage = action.error.message!;
            }
        );
    },
});

export const {clearApiMessage} = employeeSlice.actions;

export const selectEmployeesByBusiness = (state: RootState, businessId: string) =>
    Object.values(state.employees.byId).filter((employee) => employee.businessId === businessId);

export default employeeSlice.reducer;
