import {createAsyncThunk} from '@reduxjs/toolkit';

import ApiCaller from '../../shared/api/ApiCaller';
import {callApiAndFetchUser, selectCurrentUser} from '../user/UserState';
import {RootState} from '../../app/Store';
import {selectBusinessesByOwner} from '../business/BusinessState';
import {ThunkParam} from '../../app/AppTypes';

const DEFAULT_BOOKING_COMMAND_ROUTE = 'api';

export const createBooking = createAsyncThunk(
    'booking/create',
    async ({id, data}: ThunkParam<string>, thunkAPI) => {
        callApiAndFetchUser(
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
        callApiAndFetchUser(
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
        callApiAndFetchUser(
            thunkAPI,
            async () =>
                await ApiCaller.remove(`${DEFAULT_BOOKING_COMMAND_ROUTE}/user/booking/${bookingId}`)
        );

        return {bookingId, userEmail: selectCurrentUser(thunkAPI.getState() as RootState)?.id};
    }
);

const selectAllBookings = (state: RootState) => Object.values(state.entities.bookings);

export const selectBookingsByBusiness = (state: RootState) =>
    selectAllBookings(state).filter((booking) => booking.businessId === state.business.current?.id);

export const selectBookingsByUser = (state: RootState) =>
    selectAllBookings(state).filter((booking) => booking.userId === selectCurrentUser(state).id);

export const selectBookingsByOwner = (state: RootState) =>
    selectBusinessesByOwner(state).flatMap((business) => business.bookings);

export const selectNextBookingByUser = (state: RootState) =>
    selectBookingsByUser(state)
        .map((booking) => {
            const minutes = booking.interval.substring(
                booking.interval.indexOf(':') + 1,
                booking.interval.indexOf(' ')
            );

            const hours = booking.interval.substring(0, booking.interval.indexOf(':'));
            const day = booking.date.substring(0, booking.date.indexOf('/'));

            const month = booking.date.substring(
                booking.date.indexOf('/') + 1,
                booking.date.lastIndexOf('/')
            );

            const year = booking.date.substring(
                booking.date.lastIndexOf('/') + 1,
                booking.date.length
            );

            const date = new Date(
                parseInt(year),
                parseInt(month) - 1,
                parseInt(day) - 1,
                parseInt(hours),
                parseInt(minutes)
            );

            return {booking, date, dateString: booking.date + ' - ' + hours + ':' + minutes};
        })
        .sort((entry1, entry2) => +entry1.date - +entry2.date)[0];
