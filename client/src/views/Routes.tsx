import {Route, Switch} from 'react-router-dom';
import React from 'react';

import {AllBusinessesView} from './user/AllBusinessesView';
import {BusinessOverview} from './business/BusinessOverview';
import {CreateBookingView} from './user/CreateBookingView';
import {CreateBusinessView} from './business/CreateBusinessView';
import {HomeView} from './HomeView';
import {BusinessBookingView} from './business/BusinessBookingView';
import {UserBookingView} from './user/UserBookingView';

export const Routes: React.FC = () => {
   return (
      <Switch>
         <Route exact path="/" component={HomeView} />
         <Route exact path="/businesses" component={AllBusinessesView} />
         <Route exact path="/business/bookings" component={BusinessBookingView} />
         <Route exact path="/booking" component={CreateBookingView} />
         <Route exact path="/create" component={CreateBusinessView} />
         <Route exact path="/mybusinesses" component={BusinessOverview} />
         <Route exact path="/mybookings" component={UserBookingView} />
      </Switch>
   );
};
