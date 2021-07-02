import React from 'react';
import {Route, Switch} from 'react-router-dom';

import {AllBusinessesView} from './business/AllBusinessesView';
import {BusinessBookingView} from './booking/BusinessBookingView';
import {BusinessEmployeeView} from './employee/BusinessEmployeeView';
import {BusinessOverview} from './business/BusinessOverview';
import {BusinessProfileView} from './business/BusinessProfileView';
import {BusinessTimeSlotView} from './timeslot/BusinessTimeSlotView';
import {ErrorView} from './ErrorView';
import {CreateBookingView} from './booking/CreateBookingView';
import {CreateBusinessView} from './business/CreateBusinessView';
import {CreateEmployeeView} from './employee/CreateEmployeeView';
import {GenerateTimeSlotsView} from './timeslot/GenerateTimeSlotsView';
import {NotFoundView} from './NotFoundView';
//import {ProfileView} from './user/ProfileView';
import {HomeView} from './user/HomeView';

import {UserBookingView} from './booking/UserBookingView';

export const Routes: React.FC = () => {
    return (
        <Switch>
            <Route exact path="/" component={HomeView} />
            <Route exact path="/user/bookings" component={UserBookingView} />
            <Route exact path="/user/business" component={AllBusinessesView} />

            <Route exact path="/booking/new" component={CreateBookingView} />
            <Route exact path="/business/new" component={CreateBusinessView} />

            <Route exact path="/business" component={BusinessOverview} />
            <Route exact path="/business/bookings" component={BusinessOverview} />
            <Route exact path="/business/timeslots" component={BusinessOverview} />
            <Route exact path="/business/employees" component={BusinessOverview} />
            <Route exact path="/business/employees/new" component={CreateEmployeeView} />
            <Route exact path="/business/employees/manage" component={BusinessEmployeeView} />
            <Route exact path="/business/bookings/manage" component={BusinessBookingView} />
            <Route exact path="/business/timeslots/manage" component={BusinessTimeSlotView} />
            <Route exact path="/business/timeslots/new" component={GenerateTimeSlotsView} />
            <Route exact path="/business/manage" component={BusinessProfileView} />

            <Route exact path="/error" component={ErrorView} />
            <Route path="*" component={NotFoundView} />
        </Switch>
    );
};
