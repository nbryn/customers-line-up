import {createSlice} from '@reduxjs/toolkit';

import {NormalizedEntityState} from './AppTypes';
import {BookingDTO} from '../features/booking/Booking';
import {BusinessDTO} from '../features/business/Business';
import {EmployeeDTO} from '../features/business/employee/Employee';
import {fetchUserAggregate} from '../features/user/UserState';
import {MessageDTO} from '../features/message/Message';
import {normalizeUserAggregate} from './Normalize';
import {TimeSlotDTO} from '../features/business/timeslot/TimeSlot';
import {UserDTO} from '../features/user/User';

export interface EntityState {
    bookings: NormalizedEntityState<BookingDTO>;
    businesses: NormalizedEntityState<BusinessDTO>;
    employees: NormalizedEntityState<EmployeeDTO>;
    messages: NormalizedEntityState<MessageDTO>;
    timeSlots: NormalizedEntityState<TimeSlotDTO>;
    users: NormalizedEntityState<UserDTO>;
}

const initialState: EntityState = {
    bookings: {},
    businesses: {},
    employees: {},
    messages: {},
    timeSlots: {},
    users: {},
};

export const entitySlice = createSlice({
    name: 'entity',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchUserAggregate.fulfilled, (state, {payload}) => {
            const normalizedState = normalizeUserAggregate(payload);
            state.bookings = normalizedState.bookings;
            state.businesses = normalizedState.businesses;
            state.employees = normalizedState.employees;
            state.messages = normalizedState.messages;
            state.timeSlots = normalizedState.timeSlots;
        });
    },
});

export default entitySlice.reducer;
