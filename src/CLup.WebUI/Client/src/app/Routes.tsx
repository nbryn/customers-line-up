import React from 'react';
import {Route, Switch} from 'react-router-dom';

import {CreateBookingView} from '../features/booking/CreateBookingView';
import {BusinessBookingView} from '../features/booking/BusinessBookingView';
import {UserBookingView} from '../features/booking/UserBookingView';
import {AllBusinessesView} from '../features/business/AllBusinessesView';
import {CreateBusinessView} from '../features/business/CreateBusinessView';
import {BusinessOverview} from '../features/business/BusinessOverview';
import {BusinessProfileView} from '../features/business/BusinessProfileView';
import {EmployeeView} from '../features/business/employee/EmployeeView';
import {CreateEmployeeView} from '../features/business/employee/CreateEmployeeView';
import {TimeSlotView} from '../features/business/timeslot/TimeSlotView';
import {GenerateTimeSlotsView} from '../features/business/timeslot/GenerateTimeSlotsView';
import {ErrorView} from '../shared/views/ErrorView';
import {NotFoundView} from '../shared/views/NotFoundView';
import {HomeView} from '../features/user/HomeView';
import {ProfileView} from '../features/user/ProfileView';

export const Routes: React.FC = () => {
    return (
        <Switch>
            <Route exact path="/" component={HomeView} />
            <Route exact path="/user/profile" component={ProfileView} />
            <Route exact path="/user/bookings" component={UserBookingView} />
            <Route exact path="/user/business" component={AllBusinessesView} />

            <Route exact path="/booking/new" component={CreateBookingView} />
            <Route exact path="/business/new" component={CreateBusinessView} />

            <Route exact path="/business" component={BusinessOverview} />
            <Route exact path="/business/bookings" component={BusinessOverview} />
            <Route exact path="/business/timeslots" component={BusinessOverview} />
            <Route exact path="/business/employees" component={BusinessOverview} />
            <Route exact path="/business/employees/new" component={CreateEmployeeView} />
            <Route exact path="/business/employees/manage" component={EmployeeView} />
            <Route exact path="/business/bookings/manage" component={BusinessBookingView} />
            <Route exact path="/business/timeslots/manage" component={TimeSlotView} />
            <Route exact path="/business/timeslots/new" component={GenerateTimeSlotsView} />
            <Route exact path="/business/manage" component={BusinessProfileView} />

            <Route exact path="/error" component={ErrorView} />
            <Route path="*" component={NotFoundView} />
        </Switch>
    );
};
