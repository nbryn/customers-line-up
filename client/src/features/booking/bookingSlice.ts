import {createAsyncThunk, createSlice} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/ApiCaller';
import {BookingDTO} from './Booking';
import {RootState} from '../../app/Store';
import {selectCurrentUser} from '../user/userSlice';
import {ThunkParam} from '../../app/AppTypes';

const DEFAULT_BOOKING_ROUTE = 'booking';

// Generic slice: https://redux-toolkit.js.org/usage/usage-with-typescript
export interface BookingState {
    byBusiness: {[businessId: string]: BookingDTO[]};
    byUser: {[email: string]: BookingDTO[]};
}

const initialState: BookingState = {
    byBusiness: {},
    byUser: {},
};

export const createBooking = createAsyncThunk<any, any, {state: RootState}>(
    'booking/create',
    async ({id, data}: ThunkParam<string>, {getState}) => {
        await ApiCaller.post(`${DEFAULT_BOOKING_ROUTE}/${id}?userId=${selectCurrentUser(getState())?.id}&businessId=${data}`);
    }
);

export const deleteBookingForBusiness = createAsyncThunk(
    'booking/deleteForBusiness',
    async ({id, data}: ThunkParam<string>) => {
        await ApiCaller.remove(`${DEFAULT_BOOKING_ROUTE}/business/${id}?bookingId=${data}`);

        return {businessId: id, bookingId: data};
    }
);

export const deleteBookingForUser = createAsyncThunk<any, any, {state: RootState}>(
    'booking/deleteForUser',
    async (bookingId: string, {getState}) => {
        await ApiCaller.remove(`${DEFAULT_BOOKING_ROUTE}/user/${bookingId}`);

        return {bookingId, userEmail: selectCurrentUser(getState())?.email};
    }
);

export const fetchBookingsByBusiness = createAsyncThunk(
    'booking/byBusiness',
    async (businessId: string) => {
        const bookings = await ApiCaller.get<BookingDTO[]>(
            `${DEFAULT_BOOKING_ROUTE}/business/${businessId}`
        );

        return {businessId, bookings};
    }
);

export const fetchBookingsByUser = createAsyncThunk<any, any, {state: RootState}>(
    'booking/byUser',
    async (_: void, {getState}) => {
        const currentUser = selectCurrentUser(getState());
        const bookings = await ApiCaller.get<BookingDTO[]>(`${DEFAULT_BOOKING_ROUTE}/user/${currentUser?.id}`);

        return {userEmail: currentUser?.email, bookings};
    }
);

export const bookingSlice = createSlice({
    name: 'booking',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(deleteBookingForBusiness.fulfilled, (state, {payload}) => {
            state.byBusiness[payload.businessId] = Object.values(
                state.byBusiness[payload.businessId]
            ).filter((b) => b.id !== payload.bookingId);
        });

        builder.addCase(deleteBookingForUser.fulfilled, (state, {payload}) => {
            state.byUser[payload.userEmail] = Object.values(state.byUser[payload.userEmail]).filter(
                (b) => b.id !== payload.bookingId
            );
        });

        builder.addCase(fetchBookingsByBusiness.fulfilled, (state, {payload}) => {
            state.byBusiness[payload.businessId] = payload.bookings;
        });

        builder.addCase(fetchBookingsByUser.fulfilled, (state, {payload}) => {
            state.byUser[payload.userEmail] = payload.bookings;
        });
    },
});

export const selectBookingsByBusiness = (businessId: string) => (state: RootState) =>
    state.bookings.byBusiness[businessId];

export const selectBookingsByUser = (state: RootState) =>
    state.bookings.byUser[state.users.currentUser!.email];

export default bookingSlice.reducer;
