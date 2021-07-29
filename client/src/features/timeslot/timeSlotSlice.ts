import {createAsyncThunk, createSlice, isAnyOf} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/ApiCaller';
import {TimeSlotDTO} from './TimeSlot';
import {NormalizedEntityState, RootState} from '../../app/Store';

const DEFAULT_TIMESLOT_ROUTE = 'timeslot';

export const TIMESLOTS_GENERATED_MSG = 'Success! Press see time slots to manage time slots.';

interface TimeSlotState extends NormalizedEntityState<TimeSlotDTO> {
    availableByBusiness: {[businessId: string]: TimeSlotDTO[]};
}

const initialState: TimeSlotState = {
    byId: {},
    availableByBusiness: {},
    allIds: [],
    isLoading: false,
    apiMessage: '',
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
            `${DEFAULT_TIMESLOT_ROUTE}/business/${businessId}`
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
    reducers: {
        clearApiMessage: (state) => {
            state.apiMessage = '';
        },
    },
    extraReducers: (builder) => {
        builder.addCase(deleteTimeSlot.fulfilled, (state, {payload}) => {
            state.isLoading = false;
            state.availableByBusiness[payload.businessId] = state.availableByBusiness[
                payload.businessId
            ].filter((t) => t.id !== payload.timeSlotId);
        });

        builder.addCase(fetchAvailableTimeSlotsByBusiness.fulfilled, (state, {payload}) => {
            state.isLoading = false;
            state.availableByBusiness[payload.businessId] = payload.timeSlots;

        });

        builder.addCase(fetchTimeSlotsByBusiness.fulfilled, (state, {payload}) => {
            state.isLoading = false;
            const newState = {...state.byId};
            payload.forEach((timeSlot) => (newState[timeSlot.id] = timeSlot));

            state.byId = newState;
        });

        builder.addCase(generateTimeSlots.fulfilled, (state) => {
            state.isLoading = false;
            state.apiMessage = TIMESLOTS_GENERATED_MSG;
        });

        builder.addMatcher(
            isAnyOf(
                deleteTimeSlot.pending,
                fetchAvailableTimeSlotsByBusiness.pending,
                fetchTimeSlotsByBusiness.pending,
                generateTimeSlots.pending
            ),
            (state) => {
                state.isLoading = true;
            }
        );

        builder.addMatcher(
            isAnyOf(
                deleteTimeSlot.rejected,
                fetchAvailableTimeSlotsByBusiness.rejected,
                fetchTimeSlotsByBusiness.rejected,
                generateTimeSlots.rejected
            ),
            (state, action) => {
                state.isLoading = false;
                state.apiMessage = action.error.message!;
            }
        );
    },
});

export const {clearApiMessage} = timeSlotSlice.actions;

export const selectTimeSlotsByBusiness = (businessId: string) => (state: RootState) =>
    Object.values(state.timeSlots.byId).filter((t) => t.businessId === businessId);

export const selectAvailableTimeSlotsByBusiness = (businessId: string) => (state: RootState) =>
    state.timeSlots.availableByBusiness[businessId];

export default timeSlotSlice.reducer;
