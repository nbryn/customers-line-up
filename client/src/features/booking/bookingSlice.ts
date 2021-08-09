import {createAsyncThunk, createSlice, isAnyOf} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/ApiCaller';
import {ApiInfo} from '../../common/api/ApiInfo';
import {apiError, apiSuccess, defaultApiInfo} from '../../common/api/ApiInfo';
import {BookingDTO} from './Booking';
import {RootState} from '../../app/Store';
import {selectCurrentUser} from '../user/userSlice';
import {State, ThunkParam} from '../../app/AppTypes';

const DEFAULT_BOOKING_ROUTE = 'booking';
const BOOKING_CREATED_MSG = 'Success - Go to my bookings to see your bookings';

// Generic slice: https://redux-toolkit.js.org/usage/usage-with-typescript
export interface BookingState {
    byBusiness: {[businessId: string]: BookingDTO[]};
    byUser: {[email: string]: BookingDTO[]};
    apiInfo: ApiInfo;
}
const initialState: BookingState = {
    byBusiness: {},
    byUser: {},
    apiInfo: defaultApiInfo(State.Bookings),
};

export const createBooking = createAsyncThunk('booking/create', async (timeSlotId: string) => {
    await ApiCaller.post(`${DEFAULT_BOOKING_ROUTE}/${timeSlotId}`);
});

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
        const bookings = await ApiCaller.get<BookingDTO[]>(`${DEFAULT_BOOKING_ROUTE}/user`);

        return {userEmail: selectCurrentUser(getState())?.email, bookings};
    }
);

export const bookingSlice = createSlice({
    name: 'booking',
    initialState,
    reducers: {
        clearBookingApiInfo: (state) => {
            state.apiInfo = defaultApiInfo(State.Bookings);
        },
    },
    extraReducers: (builder) => {
        builder.addCase(createBooking.fulfilled, (state) => {
            state.apiInfo = apiSuccess({state: State.Bookings, message: BOOKING_CREATED_MSG, buttonText: "My Bookings"});
        });

        builder.addCase(deleteBookingForBusiness.fulfilled, (state, {payload}) => {
            state.apiInfo.isLoading = false;
            state.byBusiness[payload.businessId] = Object.values(
                state.byBusiness[payload.businessId]
            ).filter((b) => b.id !== payload.bookingId);
        });

        builder.addCase(deleteBookingForUser.fulfilled, (state, {payload}) => {
            state.apiInfo.isLoading = false;
            state.byUser[payload.userEmail] = Object.values(state.byUser[payload.userEmail]).filter(
                (b) => b.id !== payload.bookingId
            );
        });

        builder.addCase(fetchBookingsByBusiness.fulfilled, (state, {payload}) => {
            state.apiInfo.isLoading = false;
            state.byBusiness[payload.businessId] = payload.bookings;
        });

        builder.addCase(fetchBookingsByUser.fulfilled, (state, {payload}) => {
            state.apiInfo.isLoading = false;
            state.byUser[payload.userEmail] = payload.bookings;
        });

        builder.addMatcher(
            isAnyOf(
                createBooking.pending,
                deleteBookingForBusiness.pending,
                deleteBookingForUser.pending,
                fetchBookingsByBusiness.pending,
                fetchBookingsByUser.pending
            ),
            (state) => {
                state.apiInfo.isLoading = true;
            }
        );

        builder.addMatcher(
            isAnyOf(
                createBooking.rejected,
                deleteBookingForBusiness.rejected,
                deleteBookingForUser.rejected,
                fetchBookingsByBusiness.rejected,
                fetchBookingsByUser.rejected
            ),
            (state, action) => {
                state.apiInfo = apiError({state: State.Bookings, message: action.error.message!});
            }
        );
    },
});

export const {clearBookingApiInfo} = bookingSlice.actions;

export const selectBookingsByBusiness = (businessId: string) => (state: RootState) =>
    state.bookings.byBusiness[businessId];

export const selectBookingsByUser = (state: RootState) =>
    state.bookings.byUser[state.users.currentUser!.email];

export default bookingSlice.reducer;
