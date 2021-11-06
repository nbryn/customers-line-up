import {createAsyncThunk, createSlice} from '@reduxjs/toolkit';

import ApiCaller from '../../../shared/api/ApiCaller';
import {NormalizedEntityState} from '../../../app/AppTypes';
import {RootState} from '../../../app/Store';
import {TimeSlotDTO} from './TimeSlot';

const DEFAULT_TIMESLOT_ROUTE = 'business/timeslot';

interface TimeSlotState extends NormalizedEntityState<TimeSlotDTO> {
    availableByBusiness: {[businessId: string]: TimeSlotDTO[]};
}

const initialState: TimeSlotState = {
    byId: {},
    availableByBusiness: {},
    allIds: [],
};

export const deleteTimeSlot = createAsyncThunk(
    'timeSlot/delete',
    async (data: {businessId: string; timeSlotId: string}) => {
        await ApiCaller.remove(`${DEFAULT_TIMESLOT_ROUTE}/${data.timeSlotId}`);

        return {businessId: data.businessId, timeSlotId: data.timeSlotId};
    }
);

export const fetchAvailableTimeSlotsByBusiness = createAsyncThunk(
    'timeSlot/availableByBusiness',
    async (businessId: string) => {
        const today = new Date();
        today.setDate(today.getDate() - 100);
        const tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 30);

        const start = today.toISOString().substring(0, 10);
        const end = tomorrow.toISOString().substring(0, 10);

        const timeSlots = await ApiCaller.get<TimeSlotDTO[]>(
            `${DEFAULT_TIMESLOT_ROUTE}/available?businessid=${businessId}&start=${start}&end=${end}`
        );

        return {businessId, timeSlots};
    }
);

export const fetchTimeSlotsByBusiness = createAsyncThunk(
    'timeSlot/byBusiness',
    async (businessId: string) => {
        const timeSlots = await ApiCaller.get<TimeSlotDTO[]>(
            `${DEFAULT_TIMESLOT_ROUTE}/${businessId}`
        );

        return timeSlots;
    }
);

export const generateTimeSlots = createAsyncThunk(
    'timeSlot/generate',
    async (data: {businessId: string; start: string}) => {
        await ApiCaller.post(`${DEFAULT_TIMESLOT_ROUTE}/generate`, data);
    }
);

export const timeSlotSlice = createSlice({
    name: 'timeSlot',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(deleteTimeSlot.fulfilled, (state, {payload}) => {
            delete state.byId[payload.timeSlotId]
        })

        .addCase(fetchAvailableTimeSlotsByBusiness.fulfilled, (state, {payload}) => {
            state.availableByBusiness[payload.businessId] = payload.timeSlots;
        })

        .addCase(fetchTimeSlotsByBusiness.fulfilled, (state, {payload}) => {
            const newState = {...state.byId};
            payload.forEach((timeSlot) => (newState[timeSlot.id] = timeSlot));

            state.byId = newState;
        });
    },
});

export const selectTimeSlotsByBusiness = (businessId: string) => (state: RootState) =>
    Object.values(state.timeSlots.byId).filter((t) => t.businessId === businessId);

export const selectAvailableTimeSlotsByBusiness = (businessId: string) => (state: RootState) =>
    state.timeSlots.availableByBusiness[businessId];

export default timeSlotSlice.reducer;
