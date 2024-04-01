import React from 'react';
import {Route, Router, Switch} from 'react-router-dom';
import {createBrowserHistory} from 'history';

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
    MANAGE_BUSINESS_ROUTE,
    ERROR_ROUTE,
    BUSINESS_ROUTE,
    MANAGE_EMPLOYEES_ROUTE,
    BUSINESS_OVERVIEW_ROUTE,
    USER_BUSINESS_ROUTE,
} from './RouteConstants';

const history = createBrowserHistory();

export const LoginAndRegisterRoutes: React.FC = () => {
    return (
        <Router history={history}>
            <Switch>
                <Route exact path={LOGIN_ROUTE} component={LoginView} />
                <Route exact path={REGISTER_ROUTE} component={RegisterView} />
            </Switch>
        </Router>
    );
};

export const AuthRoutes: React.FC = () => {
    return (
        <Router history={history}>
            <Switch>
                <Route exact path={HOME_ROUTE} component={HomeView} />
                <Route exact path={USER_PROFILE_ROUTE} component={ProfileView} />
                <Route exact path={USER_BOOKINGS_ROUTE} component={UserBookingView} />
                <Route exact path={USER_BUSINESS_ROUTE} component={AllBusinessesView} />
                <Route exact path={USER_MESSAGES_ROUTE} component={UserMessageView} />
                <Route exact path={CREATE_BOOKING_ROUTE} component={CreateBookingView} />
                <Route exact path={BUSINESS_ROUTE} component={CreateBusinessView} />
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
                <Route
                    exact
                    path={MANAGE_BUSINESS_MESSAGES_ROUTE}
                    component={BusinessMessageView}
                />
                <Route exact path={GENERATE_TIMESLOTS_ROUTE} component={GenerateTimeSlotsView} />
                <Route exact path={MANAGE_BUSINESS_ROUTE} component={BusinessProfileView} />
                <Route exact path={ERROR_ROUTE} component={ErrorView} />
                <Route path="*" component={NotFoundView} />
            </Switch>
        </Router>
    );
};
