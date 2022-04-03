import {createAsyncThunk} from '@reduxjs/toolkit';

import ApiCaller from '../../shared/api/ApiCaller';
import {RootState} from '../../app/Store';
import {selectCurrentUser} from '../user/UserState';
import {ThunkParam} from '../../app/AppTypes';
import {callApiAndFetchAggregate} from '../user/UserState';

const DEFAULT_BOOKING_COMMAND_ROUTE = 'api';

export const createBooking = createAsyncThunk(
    'booking/create',
    async ({id, data}: ThunkParam<string>, thunkAPI) => {
        callApiAndFetchAggregate(
            thunkAPI,
            async () =>
                await ApiCaller.post(
                    `${DEFAULT_BOOKING_COMMAND_ROUTE}/user/booking/${id}?userId=${
                        selectCurrentUser(thunkAPI.getState() as RootState)?.id
                    }&businessId=${data}`
                )
        );
    }
);

export const deleteBookingForBusiness = createAsyncThunk(
    'booking/deleteForBusiness',
    async ({id, data}: ThunkParam<string>, thunkAPI) => {
        callApiAndFetchAggregate(
            thunkAPI,
            async () =>
                await ApiCaller.remove(
                    `${DEFAULT_BOOKING_COMMAND_ROUTE}/business/${id}/booking?bookingId=${data}`
                )
        );

        return {businessId: id, bookingId: data};
    }
);

export const deleteBookingForUser = createAsyncThunk<any, any, {state: RootState}>(
    'booking/deleteForUser',
    async (bookingId: string, thunkAPI) => {
        callApiAndFetchAggregate(
            thunkAPI,
            async () =>
                await ApiCaller.remove(`${DEFAULT_BOOKING_COMMAND_ROUTE}/user/booking/${bookingId}`)
        );

        return {bookingId, userEmail: selectCurrentUser(thunkAPI.getState() as RootState)?.id};
    }
);

const getBookings = (state: RootState) => Object.values(state.entities.bookings);

export const selectBookingsByBusiness = (state: RootState) =>
    getBookings(state).filter((booking) => booking.businessId === state.business.current?.id);

export const selectBookingsByUser = (state: RootState) =>
    getBookings(state).filter((booking) => booking.userId === state.user.current?.id);
