import React from 'react';
import {Route, Routes} from 'react-router-dom';

import {CreateBookingView} from '../features/booking/CreateBookingView';
import {BusinessBookingView} from '../features/booking/BusinessBookingView';
import {UserBookingView} from '../features/booking/UserBookingView';
import {AllBusinessesView} from '../features/business/AllBusinessesView';
import {CreateBusinessView} from '../features/business/CreateBusinessView';
import {BusinessOverview} from '../features/business/BusinessOverview';
import {SelectBusinessView} from '../features/business/SelectBusinessView';
import {BusinessProfileView} from '../features/business/BusinessProfileView';
import {EmployeeView} from '../features/employee/EmployeeView';
import {CreateEmployeeView} from '../features/employee/CreateEmployeeView';
import {TimeSlotView} from '../features/timeslot/TimeSlotView';
import {GenerateTimeSlotsView} from '../features/timeslot/GenerateTimeSlotsView';
import {ErrorView} from '../shared/views/ErrorView';
import {NotFoundView} from '../shared/views/NotFoundView';
import {HomeView} from '../features/user/HomeView';
import {ProfileView} from '../features/user/ProfileView';
import {UserMessageView} from '../features/user/UserMessageView';
import {BusinessMessageView} from '../features/business/BusinessMessageView';
import {MANAGE_TIMESLOTS_ROUTE} from '../features/timeslot/TimeSlotApi';
import {LoginView} from '../features/auth/LoginView';
import {RegisterView} from '../features/auth/RegisterView';
import {
    LOGIN_ROUTE,
    REGISTER_ROUTE,
    HOME_ROUTE,
    USER_PROFILE_ROUTE,
    USER_BOOKINGS_ROUTE,
    USER_MESSAGES_ROUTE,
    CREATE_BOOKING_ROUTE,
    BUSINESS_BOOKINGS_ROUTE,
    BUSINESS_TIMESLOTS_ROUTE,
    BUSINESS_EMPLOYEES_ROUTE,
    BUSINESS_MESSAGES_ROUTE,
    CREATE_EMPLOYEE_ROUTE,
    MANAGE_BOOKINGS_ROUTE,
    MANAGE_BUSINESS_MESSAGES_ROUTE,
    GENERATE_TIMESLOTS_ROUTE,
    BUSINESS_PROFILE_ROUTE,
    ERROR_ROUTE,
    BUSINESS_ROUTE,
    MANAGE_EMPLOYEES_ROUTE,
    BUSINESS_OVERVIEW_ROUTE,
    ALL_BUSINESSES_VIEW,
    CREATE_BUSINESS_ROUTE,
} from './RouteConstants';

export const PublicRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path={LOGIN_ROUTE} element={<LoginView />} />
            <Route path={REGISTER_ROUTE} element={<RegisterView />} />
        </Routes>
    );
};

export const AuthRoutes: React.FC = () => {
    return (
        <Routes>
            <Route path={HOME_ROUTE} element={<HomeView />} />
            <Route path={USER_PROFILE_ROUTE} element={<ProfileView />} />
            <Route path={USER_BOOKINGS_ROUTE} element={<UserBookingView />} />
            <Route path={ALL_BUSINESSES_VIEW} element={<AllBusinessesView />} />
            <Route path={USER_MESSAGES_ROUTE} element={<UserMessageView />} />
            <Route path={`${CREATE_BOOKING_ROUTE}/:businessId`} element={<CreateBookingView />} />
            <Route path={CREATE_BUSINESS_ROUTE} element={<CreateBusinessView />} />
            <Route path={BUSINESS_ROUTE} element={<SelectBusinessView />} />
            <Route path={`${BUSINESS_OVERVIEW_ROUTE}/:businessId`} element={<BusinessOverview />} />
            <Route path={`${BUSINESS_BOOKINGS_ROUTE}/:businessId`} element={<BusinessBookingView />} />
            <Route path={`${BUSINESS_TIMESLOTS_ROUTE}/:businessId`} element={<TimeSlotView />} />
            <Route path={`${BUSINESS_EMPLOYEES_ROUTE}/:businessId`} element={<EmployeeView />} />
            <Route path={`${BUSINESS_MESSAGES_ROUTE}/:businessId`} element={<BusinessMessageView />} />
            <Route path={`${CREATE_EMPLOYEE_ROUTE}/:businessId`} element={<CreateEmployeeView />} />
            <Route path={`${MANAGE_EMPLOYEES_ROUTE}/:businessId`} element={<EmployeeView />} />
            <Route path={`${MANAGE_BOOKINGS_ROUTE}/:businessId`} element={<BusinessBookingView />} />
            <Route path={`${MANAGE_TIMESLOTS_ROUTE}/:businessId`} element={<TimeSlotView />} />
            <Route path={`${MANAGE_BUSINESS_MESSAGES_ROUTE}/:businessId`} element={<BusinessMessageView />} />
            <Route path={`${GENERATE_TIMESLOTS_ROUTE}}/:businessId`} element={<GenerateTimeSlotsView />} />
            <Route path={`${BUSINESS_PROFILE_ROUTE}/:businessId`} element={<BusinessProfileView />} />
            <Route path={ERROR_ROUTE} element={<ErrorView />} />
            <Route path="*" element={<NotFoundView />} />
        </Routes>
    );
};
