import {createSlice, isAnyOf} from '@reduxjs/toolkit';

import {
    createBooking,
    deleteBookingForBusiness,
    deleteBookingForUser,
} from '../../features/booking/BookingApi';
import {
    createBusiness,
    fetchAllBusinesses,
    fetchBusinessesTypes,
    updateBusinessInfo,
} from '../../features/business/BusinessApi';
import {createEmployee, removeEmployee} from '../../features/employee/EmployeeApi';
import {sendMessage} from '../../features/message/MessageApi';
import {deleteTimeSlot, generateTimeSlots} from '../../features/timeslot/TimeSlotApi';
import {
    login,
    register,
    fetchUser,
    fetchUsersNotEmployedByBusiness,
    updateUserInfo,
} from '../../features/user/UserApi';
import {useAppSelector, type RootState} from '../../app/Store';
import { QueryStatus } from '@reduxjs/toolkit/query';


const USER_DELETED_BOOKING_MSG = 'Booking Deleted';

const BUSINESS_CREATED_MSG = 'Business Created - Go to my businesses to see your businesses';
const BUSINESS_UPDATED_MSG = 'Business Updated';
const BUSINESS_DELETED_BOOKING_MSG = 'Booking Deleted - User has been notified';

const EMPLOYEE_CREATED_MSG = 'Employee Created - Go to my employees to see your employees';

const TIMESLOT_DELETED_MSG = 'Time slot Deleted';
const TIMESLOTS_GENERATED_MSG = 'Success! Press see time slots to manage time slots.';

const MESSAGE_SEND = 'Message successfully send';

const USER_UPDATED_MSG = 'Info Updated.';
const LOGIN_FAILED_MSG = 'Wrong Email/Password';

export type ToastInfo = {
    buttonText: string;
    navigateTo: string;
};

interface ApiState {
    error: boolean;
    message: string;
    toastInfo?: ToastInfo;
}

const initialState: ApiState = {
    error: false,
    message: '',
    toastInfo: undefined,
};

export const isLoading = useAppSelector((state) => {
    return Object.values(state.api.queries).some((query) => {
        return query && query.status === QueryStatus.pending;
    });
});

export const apiSlice = createSlice({
    name: 'api',
    initialState,
    reducers: {
        setApiState: (state: ApiState) => {
            state = state;
        },
        clearApiState: (state) => {
            state.error = false;
            state.message = '';
            state.toastInfo = undefined;
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(createBusiness.fulfilled, (state) => {
                state.error = false;
                state.message = BUSINESS_CREATED_MSG;
                state.toastInfo = {buttonText: 'My Businesses', navigateTo: '/business'};
            })

            .addCase(createEmployee.fulfilled, (state) => {
                state.error = false;
                state.message = EMPLOYEE_CREATED_MSG;
                state.toastInfo = {
                    buttonText: 'My Employees',
                    navigateTo: '/business/employees/manage',
                };
            })

            .addCase(updateBusinessInfo.fulfilled, (state) => {
                state.error = false;
                state.message = BUSINESS_UPDATED_MSG;
            })

            .addCase(generateTimeSlots.fulfilled, (state) => {
                state.error = false;
                state.message = TIMESLOTS_GENERATED_MSG;
                state.toastInfo = {
                    buttonText: 'See time slots',
                    navigateTo: '/business/timeslots/manage',
                };
            })

            .addCase(login.rejected, (state) => {
                state.error = true;
                state.message = LOGIN_FAILED_MSG;
            })

            .addCase(deleteBookingForUser.fulfilled, (state) => {
                state.error = false;
                state.message = USER_DELETED_BOOKING_MSG;
            })

            .addCase(deleteTimeSlot.fulfilled, (state) => {
                state.error = false;
                state.message = TIMESLOT_DELETED_MSG;
            })

            .addCase(updateUserInfo.fulfilled, (state) => {
                state.error = false;
                state.message = USER_UPDATED_MSG;
            })

            .addCase(deleteBookingForBusiness.fulfilled, (state) => {
                state.error = false;
                state.message = BUSINESS_DELETED_BOOKING_MSG;
            })

            .addCase(sendMessage.fulfilled, (state) => {
                state.error = false;
                state.message = MESSAGE_SEND;
            })

            .addMatcher(
                isAnyOf(
                    fetchAllBusinesses.fulfilled,
                    fetchBusinessesTypes.fulfilled,
                    removeEmployee.fulfilled,
                    login.fulfilled,
                    register.fulfilled,
                    fetchUser.fulfilled
                ),
                (state) => {
                    state.error = false;
                    state.message = '';
                }
            )

            .addMatcher(
                isAnyOf(
                    createBooking.pending,
                    deleteBookingForBusiness.pending,
                    deleteBookingForUser.pending,
                    createBusiness.pending,
                    fetchAllBusinesses.pending,
                    fetchBusinessesTypes.pending,
                    updateBusinessInfo.pending,
                    createEmployee.pending,
                    removeEmployee.pending,
                    deleteTimeSlot.pending,
                    generateTimeSlots.pending,
                    fetchUsersNotEmployedByBusiness.pending,
                    login.pending,
                    register.pending,
                    updateUserInfo.pending,
                    sendMessage.pending,
                    fetchUser.pending
                ),
                (state) => {
                     = true;
                }
            )

            .addMatcher(
                isAnyOf(
                    deleteBookingForBusiness.rejected,
                    deleteBookingForUser.rejected,
                    fetchAllBusinesses.rejected,
                    createBusiness.rejected,
                    fetchAllBusinesses.rejected,
                    fetchBusinessesTypes.rejected,
                    updateBusinessInfo.rejected,
                    createEmployee.rejected,
                    removeEmployee.rejected,
                    deleteTimeSlot.rejected,
                    generateTimeSlots.rejected,
                    fetchUsersNotEmployedByBusiness.rejected,
                    register.rejected,
                    updateUserInfo.rejected,
                    sendMessage.rejected,
                    fetchUser.rejected
                ),
                (state, action) => {
                     = false;
                    state.error = true;
                    state.message = action.error.message!;
                }
            );
    },
});

export const {clearApiState, setApiState} = apiSlice.actions;

export const selectApiState = (state: RootState) => state.api;

export default apiSlice.reducer;
