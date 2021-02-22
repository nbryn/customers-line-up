import {BrowserRouter as Router} from 'react-router-dom';
import React from 'react';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';

import {UserContextProvider} from '../src/context/UserContext';
import {MainView} from './views/MainView';

ReactDOM.render(
    <UserContextProvider>
        <Router>
            <MainView />
        </Router>
    </UserContextProvider>,
    document.getElementById('root')
);

reportWebVitals();
