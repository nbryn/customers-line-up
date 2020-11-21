import {Route, Switch} from 'react-router-dom';
import React from 'react';

import {AllBusinessesView} from './AllBusinessesView';
import {HomeView} from './HomeView'
import {OwnerBusinessesView} from './OwnerBusinessesView';

export const Routes: React.FC = () => {
   return (
      <Switch>
         <Route exact path="/" component={HomeView} />
         <Route exact path="/businesses" component={AllBusinessesView} />
         <Route exact path="/mybusinesses" component={OwnerBusinessesView} />
      </Switch>
   );
};
