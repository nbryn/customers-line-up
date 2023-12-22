import {createAsyncThunk} from '@reduxjs/toolkit';

import ApiCaller from '../../../shared/api/ApiCaller';
import {RootState} from '../../../app/Store';
import {callApiAndFetchUser} from '../../user/UserState';

const DEFAULT_TIMESLOT_COMMAND_ROUTE = 'api/business/timeslot';

export const deleteTimeSlot = createAsyncThunk(
    'timeSlot/delete',
    async (data: {businessId: string; timeSlotId: string}, thunkAPI) => {
        callApiAndFetchUser(
            thunkAPI,
            async () =>
                await ApiCaller.remove(`${DEFAULT_TIMESLOT_COMMAND_ROUTE}/${data.timeSlotId}`)
        );

        return {businessId: data.businessId, timeSlotId: data.timeSlotId};
    }
);

export const generateTimeSlots = createAsyncThunk(
    'timeSlot/generate',
    async (data: {businessId: string; start: string}, thunkAPI) => {
        callApiAndFetchUser(
            thunkAPI,
            async () => await ApiCaller.post(`${DEFAULT_TIMESLOT_COMMAND_ROUTE}/generate`, data)
        );
    }
);

const getTimeSlots = (state: RootState) => Object.values(state.entities.timeSlots);

export const selectTimeSlotsByBusiness = (state: RootState) =>
    getTimeSlots(state).filter((timeSlot) => timeSlot.businessId === state.business.current?.id);

export const selectAvailableTimeSlotsForCurrentBusiness = (state: RootState) =>
    selectTimeSlotsByBusiness(state).filter((timeSlot) => timeSlot.available);
