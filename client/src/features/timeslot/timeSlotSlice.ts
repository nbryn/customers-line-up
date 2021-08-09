import {createAsyncThunk, createSlice, isAnyOf} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/ApiCaller';
import {apiError, defaultApiInfo} from '../../common/api/ApiInfo';
import {NormalizedEntityState, State} from '../../app/AppTypes';
import {RootState} from '../../app/Store';
import {TimeSlotDTO} from './TimeSlot';

const DEFAULT_TIMESLOT_ROUTE = 'timeslot';

export const TIMESLOTS_GENERATED_MSG = 'Success! Press see time slots to manage time slots.';

interface TimeSlotState extends NormalizedEntityState<TimeSlotDTO> {
    availableByBusiness: {[businessId: string]: TimeSlotDTO[]};
}

const initialState: TimeSlotState = {
    byId: {},
    availableByBusiness: {},
    allIds: [],
    apiInfo: defaultApiInfo(State.TimeSlots),
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
        clearTimeSlotsApiInfo: (state) => {
            state.apiInfo = defaultApiInfo(State.TimeSlots);
        },
    },
    extraReducers: (builder) => {
        builder.addCase(deleteTimeSlot.fulfilled, (state, {payload}) => {
            state.apiInfo.isLoading = false;

            state.availableByBusiness[payload.businessId] = state.availableByBusiness[
                payload.businessId
            ].filter((t) => t.id !== payload.timeSlotId);
        });

        builder.addCase(fetchAvailableTimeSlotsByBusiness.fulfilled, (state, {payload}) => {
            state.apiInfo.isLoading = false;
            state.availableByBusiness[payload.businessId] = payload.timeSlots;
        });

        builder.addCase(fetchTimeSlotsByBusiness.fulfilled, (state, {payload}) => {
            state.apiInfo.isLoading = false;

            const newState = {...state.byId};
            payload.forEach((timeSlot) => (newState[timeSlot.id] = timeSlot));

            state.byId = newState;
        });

        builder.addCase(generateTimeSlots.fulfilled, (state) => {
            state.apiInfo.isLoading = false;
            state.apiInfo.message = TIMESLOTS_GENERATED_MSG;
        });

        builder.addMatcher(
            isAnyOf(
                deleteTimeSlot.pending,
                fetchAvailableTimeSlotsByBusiness.pending,
                fetchTimeSlotsByBusiness.pending,
                generateTimeSlots.pending
            ),
            (state) => {
                state.apiInfo.isLoading = true;
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
                state.apiInfo = apiError({state: State.TimeSlots, message: action.error.message!});
            }
        );
    },
});

export const {clearTimeSlotsApiInfo} = timeSlotSlice.actions;

export const selectTimeSlotsByBusiness = (businessId: string) => (state: RootState) =>
    Object.values(state.timeSlots.byId).filter((t) => t.businessId === businessId);

export const selectAvailableTimeSlotsByBusiness = (businessId: string) => (state: RootState) =>
    state.timeSlots.availableByBusiness[businessId];

export default timeSlotSlice.reducer;
