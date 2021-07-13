import {configureStore} from '@reduxjs/toolkit';
import {TypedUseSelectorHook, useDispatch, useSelector} from 'react-redux';

import bookingReducer from '../features/booking/bookingSlice';
import businessReducer from '../features/business/businessSlice';
import employeeReducer from '../features/employee/employeeSlice';
import insightsReducer from '../features/insights/insightsSlice';
import timeSlotReducer from '../features/timeslot/timeSlotSlice';
import userReducer from '../features/user/userSlice';

export enum State {
    Bookings = 'Bookings',
    Businesses = 'Businesses',
    Employees = 'Employees',
    Insights = 'Insights',
    TimeSlots = 'timeSlots',
    Users = 'users',
}

export interface NormalizedEntityState<T> {
    byId: {[id: string]: T};
    allIds: string[];
    isLoading: boolean;
    apiMessage: string;
}

export type ThunkParam<T1> = {
    id: string;
    data: T1;
};

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
            return state.bookings.isLoading;

        case State.Businesses:
            return state.businesses.isLoading;

        case State.Employees:
            return state.employees.isLoading;

        case State.Insights:
            return state.insights.isLoading;

        case State.TimeSlots:
            return state.timeSlots.isLoading;

        case State.Users:
            return state.users.isLoading;

        default:
            return false;
    }
};

export const selectApiMessage = (entity: State) => (state: RootState) => {
    switch (entity) {
        case State.Bookings:
            return state.bookings.apiMessage;

        case State.Businesses:
            return state.businesses.apiMessage;

        case State.Employees:
            return state.employees.apiMessage;

        case State.Insights:
            return state.insights.apiMessage;

        case State.TimeSlots:
            return state.timeSlots.apiMessage;

        case State.Users:
            return state.users.apiMessage;

        default:
            return '';
    }
};

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
