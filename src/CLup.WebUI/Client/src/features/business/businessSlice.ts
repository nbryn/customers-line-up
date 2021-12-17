import {createAsyncThunk, createSlice, PayloadAction} from '@reduxjs/toolkit';

import ApiCaller from '../../shared/api/ApiCaller';
import {BusinessDTO, BusinessMessageResponse} from './Business';
import {EmployeeDTO} from './employee/Employee';
import {NormalizedEntityState} from '../../app/AppTypes';
import {RootState} from '../../app/Store';
import {selectCurrentUser} from '../user/userSlice';
import {deleteEmployee, fetchEmployeesByBusiness, initialEmployeeState} from './employee/employeeState';
import {
    TimeSlotState,
    deleteTimeSlot,
    fetchAvailableTimeSlotsByBusiness,
    fetchTimeSlotsByBusiness,
    initialTimeSlotState,
} from './timeslot/timeSlotState';

const DEFAULT_BUSINESS_ROUTE = 'business';

interface BusinessState extends NormalizedEntityState<BusinessDTO> {
    businessTypes: string[];
    currentBusiness: BusinessDTO | null;
    employees: typeof initialEmployeeState
    timeSlots: TimeSlotState;
    messages: BusinessMessageResponse | null;
}

const initialState: BusinessState = {
    byId: {},
    allIds: [],
    businessTypes: [],
    currentBusiness: null,
    messages: null,
    employees: initialEmployeeState,
    timeSlots: initialTimeSlotState,
};

export const createBusiness = createAsyncThunk('business/create', async (data: BusinessDTO) => {
    await ApiCaller.post(`${DEFAULT_BUSINESS_ROUTE}`, data);
});

export const fetchBusinessMessages = createAsyncThunk('business/messages', async (businessId: string) => {
    const messages = await ApiCaller.get<BusinessMessageResponse>(`query/${DEFAULT_BUSINESS_ROUTE}/${businessId}/messages`);

    return messages;
});

export const fetchAllBusinesses = createAsyncThunk('business/fetchAll', async () => {
    const businesses = await ApiCaller.get<BusinessDTO[]>(`query/${DEFAULT_BUSINESS_ROUTE}/all`);

    return businesses;
});

export const fetchBusinessesByOwner = createAsyncThunk('business/fetchByOwner', async () => {
    const businesses = ApiCaller.get<BusinessDTO[]>(`query/${DEFAULT_BUSINESS_ROUTE}/owner`);

    return businesses;
});

export const fetchBusinessesTypes = createAsyncThunk('business/fetchBusinessTypes', async () => {
    const businessTypes = ApiCaller.get<string[]>(`query/${DEFAULT_BUSINESS_ROUTE}/types`);

    return businessTypes;
});

export const updateBusinessInfo = createAsyncThunk(
    'business/update',
    async (data: {businessId: string; ownerEmail: string; business: BusinessDTO}) => {
        await ApiCaller.put(`${DEFAULT_BUSINESS_ROUTE}/${data.businessId}`, {
            ...data.business,
            id: data.businessId,
            ownerEmail: data.ownerEmail,
        });
    }
);

export const businessSlice = createSlice({
    name: 'business',
    initialState,
    reducers: {
        setCurrentBusiness: (state, action: PayloadAction<BusinessDTO>) => {
            state.currentBusiness = action.payload;
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchAllBusinesses.fulfilled, (state, {payload}) => {
                const newState = {...state.byId};
                payload.forEach((business) => (newState[business.id] = business));

                state.byId = newState;
            })

            .addCase(fetchBusinessesByOwner.fulfilled, (state, {payload}) => {
                const newState = {...state.byId};
                payload.forEach((business) => (newState[business.id] = business));

                state.byId = newState;
            })

            .addCase(fetchBusinessesTypes.fulfilled, (state, {payload}) => {
                state.businessTypes = payload;
            })

            .addCase(deleteTimeSlot.fulfilled, (state, {payload}) => {
                delete state.timeSlots.byId[payload.timeSlotId];
            })

            .addCase(fetchAvailableTimeSlotsByBusiness.fulfilled, (state, {payload}) => {
                state.timeSlots.availableByBusiness[payload.businessId] = payload.timeSlots;
            })

            .addCase(fetchTimeSlotsByBusiness.fulfilled, (state, {payload}) => {
                const newState = {...state.timeSlots.byId};
                payload.forEach((timeSlot) => (newState[timeSlot.id] = timeSlot));

                state.timeSlots.byId = newState;
            })

            .addCase(deleteEmployee.fulfilled, (state, action) => {
                delete state.employees.byId[action.payload];
            })

            .addCase(fetchBusinessMessages.fulfilled, (state, action) => {
                state.messages = action.payload;
            })
    
            .addCase(
                fetchEmployeesByBusiness.fulfilled,
                (state, action: PayloadAction<EmployeeDTO[]>) => {
                    const newState = {...state.employees.byId};
                    action.payload.forEach((employee) => (newState[employee.id] = employee));
                    state.employees.byId = newState;
                }
            );
    },
});

export const {setCurrentBusiness} = businessSlice.actions;

export const selectAllBusinesses = (state: RootState) => Object.values(state.businesses.byId);
export const selectCurrentBusiness = (state: RootState) => state.businesses.currentBusiness;

export const selectBusinessesByOwner = (state: RootState) =>
    selectAllBusinesses(state).filter((b) => b.ownerEmail === selectCurrentUser(state)?.email);

export const selectBusinessTypes = (state: RootState) => state.businesses.businessTypes;

export const selectBusinessMessages = (state: RootState) => state.businesses.messages;

export default businessSlice.reducer;
