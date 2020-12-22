import React from 'react';
import {Route, Switch} from 'react-router-dom';

import {AllBusinessesView} from './business/AllBusinessesView';
import {BusinessBookingView} from './booking/BusinessBookingView';
import {BusinessEmployeeView} from './employee/BusinessEmployeeView';
import {BusinessOverview} from './business/BusinessOverview';
import {BusinessTimeSlotView} from './business/BusinessTimeSlotView';
import {NewBookingView} from './booking/NewBookingView';
import {NewBusinessView} from './business/NewBusinessView';
import {NewEmployeeView} from './employee/NewEmployeeView';
import {HomeView} from './user/HomeView';
import {BusinessProfileView} from './business/BusinessProfileView';
import {UserBookingView} from './booking/UserBookingView';

export const Routes: React.FC = () => {
   return (
      <Switch>
         <Route exact path="/" component={HomeView} />
         <Route exact path="/user/bookings" component={UserBookingView} />
         <Route exact path="/user/business" component={AllBusinessesView} />

         <Route exact path="/booking/new" component={NewBookingView} />
         <Route exact path="/business/new" component={NewBusinessView} />

         <Route exact path="/business/bookings" component={BusinessOverview} />
         <Route exact path="/business/timeslots" component={BusinessOverview} />
         <Route exact path="/business/employees" component={BusinessOverview} />
         <Route exact path="/business/employees/new" component={NewEmployeeView} />
         <Route exact path="/business/employees/manage" component={BusinessEmployeeView} />
         <Route exact path="/business" component={BusinessOverview} />
         <Route exact path="/business/bookings/manage" component={BusinessBookingView} />
         <Route exact path="/business/timeslots/manage" component={BusinessTimeSlotView} />
         <Route exact path="/business/manage" component={BusinessProfileView} />
      </Switch>
   );
};
