import React from 'react';
import {Route, Switch} from 'react-router-dom';

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

const HOME_ROUTE = '/';

const USER_PROFILE_ROUTE = '/user/profile';
const USER_BOOKINGS_ROUTE = '/user/bookings';
const USER_BUSINESS_ROUTE = '/user/business';
const USER_MESSAGES_ROUTE = '/user/messages';

const CREATE_BOOKING_ROUTE = '/booking/new';
const CREATE_BUSINESS_ROUTE = '/business/new';

export const BUSINESS_ROUTE = '/business';
const BUSINESS_OVERVIEW_ROUTE = '/business/overview';
const BUSINESS_BOOKINGS_ROUTE = '/business/bookings';
const BUSINESS_TIMESLOTS_ROUTE = '/business/timeslots';
const BUSINESS_EMPLOYEES_ROUTE = '/business/employees';
const BUSINESS_MESSAGES_ROUTE = '/business/messages';

const CREATE_EMPLOYEE_ROUTE = '/business/employees/new';
export const MANAGE_EMPLOYEES_ROUTE = '/business/employees/manage';

const MANAGE_BOOKINGS_ROUTE = '/business/bookings/manage';
export const MANAGE_TIMESLOTS_ROUTE = '/business/timeslots/manage';
const MANAGE_BUSINESS_MESSAGES_ROUTE = '/business/messages/manage';
const GENERATE_TIMESLOTS_ROUTE = '/business/timeslots/new';
const MANAGE_BUSINESS_ROUTE = '/business/manage';

const ERROR_ROUTE = '/error';

export const Routes: React.FC = () => {
    return (
        <Switch>
            <Route exact path={HOME_ROUTE} component={HomeView} />
            <Route exact path={USER_PROFILE_ROUTE} component={ProfileView} />
            <Route exact path={USER_BOOKINGS_ROUTE} component={UserBookingView} />
            <Route exact path={USER_BUSINESS_ROUTE} component={AllBusinessesView} />
            <Route exact path={USER_MESSAGES_ROUTE} component={UserMessageView} />

            <Route exact path={CREATE_BOOKING_ROUTE} component={CreateBookingView} />
            <Route exact path={CREATE_BUSINESS_ROUTE} component={CreateBusinessView} />

            <Route exact path={BUSINESS_ROUTE} component={SelectBusinessView} />
            <Route exact path={BUSINESS_OVERVIEW_ROUTE} component={BusinessOverview} />
            <Route exact path={BUSINESS_BOOKINGS_ROUTE} component={BusinessBookingView} />
            <Route exact path={BUSINESS_TIMESLOTS_ROUTE} component={TimeSlotView} />
            <Route exact path={BUSINESS_EMPLOYEES_ROUTE} component={EmployeeView} />
            <Route exact path={BUSINESS_MESSAGES_ROUTE} component={BusinessMessageView} />
            <Route exact path={CREATE_EMPLOYEE_ROUTE} component={CreateEmployeeView} />
            <Route exact path={MANAGE_EMPLOYEES_ROUTE} component={EmployeeView} />
            <Route exact path={MANAGE_BOOKINGS_ROUTE} component={BusinessBookingView} />
            <Route exact path={MANAGE_TIMESLOTS_ROUTE} component={TimeSlotView} />
            <Route exact path={MANAGE_BUSINESS_MESSAGES_ROUTE} component={BusinessMessageView} />
            <Route exact path={GENERATE_TIMESLOTS_ROUTE} component={GenerateTimeSlotsView} />
            <Route exact path={MANAGE_BUSINESS_ROUTE} component={BusinessProfileView} />

            <Route exact path={ERROR_ROUTE} component={ErrorView} />
            <Route path="*" component={NotFoundView} />
        </Switch>
    );
};