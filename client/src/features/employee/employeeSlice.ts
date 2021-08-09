import {createAsyncThunk, createSlice, isAnyOf, PayloadAction} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/ApiCaller';
import {apiError, apiSuccess, defaultApiInfo} from '../../common/api/ApiInfo';
import {EmployeeDTO} from './Employee';
import {NormalizedEntityState, State, ThunkParam} from '../../app/AppTypes';
import {RootState} from '../../app/Store';

const DEFAULT_EMPLOYEE_ROUTE = 'employee';
const EMPLOYEE_CREATED_MSG = 'Employee Created - Go to my employees to see your employees';

const initialState: NormalizedEntityState<EmployeeDTO> = {
    byId: {},
    allIds: [],
    apiInfo: defaultApiInfo(State.Employees),
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
        clearEmployeeApiInfo: (state) => {
            state.apiInfo = defaultApiInfo(State.Employees);
        },
    },
    extraReducers: (builder) => {
        builder.addCase(createEmployee.fulfilled, (state) => {
            state.apiInfo = apiSuccess({state: State.Employees, message: EMPLOYEE_CREATED_MSG});
        });

        builder.addCase(deleteEmployee.fulfilled, (state, action) => {
            state.apiInfo.isLoading = false;
            delete state.byId[action.payload];
        });

        builder.addCase(
            fetchEmployeesByBusiness.fulfilled,
            (state, action: PayloadAction<EmployeeDTO[]>) => {
                const newState = {...state.byId};
                action.payload.forEach((employee) => (newState[employee.id] = employee));

                state.byId = newState;
                state.apiInfo.isLoading = false;
            }
        );
        builder.addMatcher(
            isAnyOf(
                createEmployee.pending,
                deleteEmployee.pending,
                fetchEmployeesByBusiness.pending
            ),
            (state) => {
                state.apiInfo.isLoading = true;
            }
        );
        builder.addMatcher(
            isAnyOf(
                createEmployee.rejected,
                deleteEmployee.rejected,
                fetchEmployeesByBusiness.rejected
            ),
            (state, action) => {
                state.apiInfo = apiError({state: State.Employees, message: action.error.message!});
            }
        );
    },
});

export const {clearEmployeeApiInfo} = employeeSlice.actions;

export const selectEmployeesByBusiness = (state: RootState, businessId: string) =>
    Object.values(state.employees.byId).filter((employee) => employee.businessId === businessId);

export default employeeSlice.reducer;
