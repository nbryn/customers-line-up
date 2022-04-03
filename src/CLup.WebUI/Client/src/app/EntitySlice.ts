import {createSlice} from '@reduxjs/toolkit';

import {BookingDTO} from '../features/booking/Booking';
import {BusinessDTO} from '../features/business/Business';
import {DTO} from '../shared/models/General';
import {fetchAllBusinesses} from '../features/business/BusinessState';
import {EmployeeDTO} from '../features/business/employee/Employee';
import {fetchUserAggregate} from '../features/user/UserState';
import {MessageDTO} from '../features/message/Message';
import {NormalizedEntityState} from './AppTypes';
import {normalizeBusinesses, normalizeUserAggregate} from './Normalize';
import {TimeSlotDTO} from '../features/business/timeslot/TimeSlot';

export interface EntityState {
    [key: string]: NormalizedEntityState<DTO>;
    bookings: NormalizedEntityState<BookingDTO>;
    businesses: NormalizedEntityState<BusinessDTO>;
    employees: NormalizedEntityState<EmployeeDTO>;
    messages: NormalizedEntityState<MessageDTO>;
    timeSlots: NormalizedEntityState<TimeSlotDTO>;
}

const initialState: EntityState = {
    bookings: {},
    businesses: {},
    employees: {},
    messages: {},
    timeSlots: {},
};

export const entitySlice = createSlice({
    name: 'entity',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchUserAggregate.fulfilled, (state, {payload}) => {
                const normalizedState = normalizeUserAggregate(payload);
                state.bookings = normalizedState.bookings;
                state.businesses = normalizedState.businesses;
                state.employees = normalizedState.employees;
                state.messages = normalizedState.messages ?? {'': ''};
                state.timeSlots = normalizedState.timeSlots;
            })

            .addCase(fetchAllBusinesses.fulfilled, (state, {payload}) => {
                const entities = normalizeBusinesses(payload);
                Object.keys(entities).forEach((entityKey) => {
                    Object.values(entities[entityKey]).forEach((entity) => {
                        if (!state[entityKey]?.[entity.id]) {
                            state[entityKey][entity.id] = entity;
                        }
                    });
                });
            });
    },
});

export default entitySlice.reducer;
