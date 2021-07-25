import {createAsyncThunk, createSlice, isAnyOf} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/ApiCaller';
import {BookingDTO} from './Booking';
import {RootState, ThunkParam} from '../../app/Store';
import {selectCurrentUser} from '../user/userSlice';

const DEFAULT_BOOKING_ROUTE = 'booking';
const BOOKING_CREATED_MSG = 'Booking Made - Go to my bookings to see your bookings';

// Generic slice: https://redux-toolkit.js.org/usage/usage-with-typescript
export interface BookingState {
    byBusiness: {[businessId: string]: BookingDTO[]};
    byUser: {[email: string]: BookingDTO[]};
    isLoading: boolean;
    apiMessage: string;
}
const initialState: BookingState = {
    byBusiness: {},
    byUser: {},
    isLoading: false,
    apiMessage: '',
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
        clearApiMessage: (state) => {
            state.apiMessage = '';
        },
    },
    extraReducers: (builder) => {
        builder.addCase(createBooking.fulfilled, (state) => {
            state.isLoading = false;
            state.apiMessage = BOOKING_CREATED_MSG;
        });

        builder.addCase(deleteBookingForBusiness.fulfilled, (state, {payload}) => {
            state.isLoading = false;
            state.byBusiness[payload.businessId] = Object.values(
                state.byBusiness[payload.businessId]
            ).filter((b) => b.id !== payload.bookingId);
        });

        builder.addCase(deleteBookingForUser.fulfilled, (state, {payload}) => {
            state.isLoading = false;
            state.byUser[payload.userEmail] = Object.values(state.byUser[payload.userEmail]).filter(
                (b) => b.id !== payload.bookingId
            );
        });

        builder.addCase(fetchBookingsByBusiness.fulfilled, (state, {payload}) => {
            state.isLoading = false;
            state.byBusiness[payload.businessId] = payload.bookings;
        });

        builder.addCase(fetchBookingsByUser.fulfilled, (state, {payload}) => {
            state.isLoading = false;
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
                state.isLoading = true;
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
                state.isLoading = false;
                state.apiMessage = action.error.message!;
            }
        );
    },
});

export const {clearApiMessage} = bookingSlice.actions;

export const selectBookingsByBusiness = (businessId: string) => (state: RootState) =>
    state.bookings.byBusiness[businessId];

export const selectBookingsByUser = (state: RootState) =>
    state.bookings.byUser[state.users.currentUser!.email];

export default bookingSlice.reducer;
