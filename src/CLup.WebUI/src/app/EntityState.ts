import {createSlice} from '@reduxjs/toolkit';

import type {BookingDTO} from '../features/booking/Booking';
import type {BusinessDTO} from '../features/business/Business';
import type {DTO} from '../shared/models/General';
import type {EmployeeDTO} from '../features/business/employee/Employee';
import {fetchAllBusinesses} from '../features/business/BusinessState';
import {fetchUser} from '../features/user/UserState';
import type {MessageDTO} from '../features/message/Message';
import type {NormalizedEntityState} from './AppTypes';
import {normalizeBusinesses, normalizeUser} from './Normalize';
import type {TimeSlotDTO} from '../features/business/timeslot/TimeSlot';
import type {UserDTO} from '../features/user/User';

export interface EntityState {
    [key: string]: NormalizedEntityState<DTO | UserDTO>;
    bookings: NormalizedEntityState<BookingDTO>;
    businesses: NormalizedEntityState<BusinessDTO>;
    employees: NormalizedEntityState<EmployeeDTO>;
    messages: NormalizedEntityState<MessageDTO>;
    timeSlots: NormalizedEntityState<TimeSlotDTO>;
    user: NormalizedEntityState<UserDTO>;
}

const initialState: EntityState = {
    bookings: {},
    businesses: {},
    employees: {},
    messages: {},
    timeSlots: {},
    user: {},
};

export const entitySlice = createSlice({
    name: 'entity',
    initialState,
    reducers: {
        clearCurrentUser: (state) => {
            state.user = {};
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchUser.fulfilled, (state, {payload}) => {
                const normalizedState = normalizeUser(payload);
                state.bookings = normalizedState.bookings;
                state.businesses = normalizedState.businesses;
                state.employees = normalizedState.employees;
                state.messages = normalizedState.messages ?? {'': ''};
                state.timeSlots = normalizedState.timeSlots;
                state.user = normalizedState.user;
            })

            .addCase(fetchAllBusinesses.fulfilled, (state, {payload}) => {
                const entities = normalizeBusinesses(payload);
                Object.keys(entities).forEach((entityKey) => {
                    Object.values(entities[entityKey]).forEach((entity) => {
                        entity = entity as DTO;
                        if (!state[entityKey]?.[entity.id]) {
                            state[entityKey][entity.id] = entity;
                        }
                    });
                });
            });
    },
});

export const {clearCurrentUser} = entitySlice.actions;

export default entitySlice.reducer;
