import {Route, Switch} from 'react-router-dom';
import React from 'react';

import {AllBusinessesView} from './business/AllBusinessesView';
import {BusinessBookingView} from './business/BusinessBookingView';
import {BusinessOverview} from './business/BusinessOverview';
import {BusinessTimeSlotView} from './business/BusinessTimeSlotView';
import {CreateBookingView} from './user/CreateBookingView';
import {CreateBusinessView} from './business/CreateBusinessView';
import {HomeView} from './user/HomeView';
import {BusinessProfileView} from './business/BusinessProfileView';
import {UserBookingView} from './user/UserBookingView';

export const Routes: React.FC = () => {
   return (
      <Switch>
         <Route exact path="/" component={HomeView} />
         <Route exact path="/user/bookings" component={UserBookingView} />
         <Route exact path="/user/business" component={AllBusinessesView} />

         <Route exact path="/new/booking" component={CreateBookingView} />
         <Route exact path="/new/business" component={CreateBusinessView} />

         <Route exact path="/business/bookings" component={BusinessOverview} />
         <Route exact path="/business/timeslots" component={BusinessOverview} />
         <Route exact path="/business/employees" component={BusinessOverview} />
         <Route exact path="/business" component={BusinessOverview} />
         <Route exact path="/business/bookings/manage" component={BusinessBookingView} />
         <Route exact path="/business/timeslots/manage" component={BusinessTimeSlotView} />
         <Route exact path="/business/manage" component={BusinessProfileView} />
      </Switch>
   );
};
