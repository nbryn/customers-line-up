import {Route, Switch} from 'react-router-dom';
import React from 'react';

import {BusinessOverview} from './BusinessOverview';
import {HomeView} from './HomeView';

export const Routes = () => {
   return (
      <Switch>
         <Route exact path="/" component={HomeView} />
         <Route exact path="/businessoverview" component={BusinessOverview} />
      </Switch>
   );
};
