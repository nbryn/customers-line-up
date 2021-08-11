import {configureStore} from '@reduxjs/toolkit';
import {TypedUseSelectorHook, useDispatch, useSelector} from 'react-redux';

import apiReducer from '../common/api/apiSlice';
import bookingReducer from '../features/booking/bookingSlice';
import businessReducer from '../features/business/businessSlice';
import employeeReducer from '../features/employee/employeeSlice';
import insightsReducer from '../features/insights/insightsSlice';
import timeSlotReducer from '../features/timeslot/timeSlotSlice';
import userReducer from '../features/user/userSlice';

export const store = configureStore({
    reducer: {
        api: apiReducer,
        bookings: bookingReducer,
        businesses: businessReducer,
        employees: employeeReducer,
        insights: insightsReducer,
        timeSlots: timeSlotReducer,
        users: userReducer,
    },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
