import {BrowserRouter as Router} from 'react-router-dom';
import React from 'react';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';

import {UserContextProvider} from './features/user/UserContext';
import {MainView} from './app/MainView';

ReactDOM.render(
    <UserContextProvider>
        <Router>
            <MainView />
        </Router>
    </UserContextProvider>,
    document.getElementById('root')
);

reportWebVitals();
