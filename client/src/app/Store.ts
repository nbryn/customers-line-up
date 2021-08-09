import {configureStore} from '@reduxjs/toolkit';
import {TypedUseSelectorHook, useDispatch, useSelector} from 'react-redux';

import bookingReducer from '../features/booking/bookingSlice';
import businessReducer from '../features/business/businessSlice';
import employeeReducer from '../features/employee/employeeSlice';
import insightsReducer from '../features/insights/insightsSlice';
import timeSlotReducer from '../features/timeslot/timeSlotSlice';
import userReducer from '../features/user/userSlice';

import {State} from './AppTypes';

export const store = configureStore({
    reducer: {
        bookings: bookingReducer,
        businesses: businessReducer,
        employees: employeeReducer,
        insights: insightsReducer,
        timeSlots: timeSlotReducer,
        users: userReducer,
    },
});

export const isLoading = (entity: State) => (state: RootState) => {
    switch (entity) {
        case State.Bookings:
            return state.bookings.apiInfo.isLoading;

        case State.Businesses:
            return state.businesses.apiInfo.isLoading;

        case State.Employees:
            return state.employees.apiInfo.isLoading;

        case State.Insights:
            return state.insights.apiInfo.isLoading;

        case State.TimeSlots:
            return state.timeSlots.apiInfo.isLoading;

        case State.Users:
            return state.users.apiInfo.isLoading;

        default:
            return false;
    }
};

export const selectApiInfo = (entity: State) => (state: RootState) => {
    switch (entity) {
        case State.Bookings:
            return state.bookings.apiInfo;

        case State.Businesses:
            return state.businesses.apiInfo;

        case State.Employees:
            return state.employees.apiInfo;

        case State.Insights:
            return state.insights.apiInfo;

        case State.TimeSlots:
            return state.timeSlots.apiInfo;

        case State.Users:
            return state.users.apiInfo;

        default:
            return {isError: false, isLoading: false, message: '', state: State.Users};
    }
};

export const selectGeneralApiInfo = (state: RootState) =>
    Object.values(State)
        .map((key) => selectApiInfo(key)(state))
        .find((apiInfo) => apiInfo.message);

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
