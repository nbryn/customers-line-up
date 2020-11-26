import {Route, Switch} from 'react-router-dom';
import React from 'react';

import {AllBusinessesView} from './business/AllBusinessesView';
import {CreateBookingView} from './business/CreateBookingView';
import {CreateBusinessView} from './business/CreateBusinessView';
import {HomeView} from './HomeView';
import {OwnerBusinessesView} from './business/OwnerBusinessesView';
import {UserBookingView} from './user/UserBookingView';

export const Routes: React.FC = () => {
   return (
      <Switch>
         <Route exact path="/" component={HomeView} />
         <Route exact path="/businesses" component={AllBusinessesView} />
         <Route exact path="/booking" component={CreateBookingView} />
         <Route exact path="/create" component={CreateBusinessView} />
         <Route exact path="/mybusinesses" component={OwnerBusinessesView} />
         <Route exact path="/mybookings" component={UserBookingView} />
      </Switch>
   );
};
