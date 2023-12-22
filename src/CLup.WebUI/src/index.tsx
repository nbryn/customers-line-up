import React from 'react';
import {BrowserRouter as Router} from 'react-router-dom';
import {Provider} from 'react-redux';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';

import {MainView} from './app/MainView';
import {store} from './app/Store';
import {UserContextProvider} from './features/user/UserContext';

ReactDOM.render(
    <Provider store={store}>
        <UserContextProvider>
            <Router>
                <MainView />
            </Router>
        </UserContextProvider>
    </Provider>,
    document.getElementById('root')
);

reportWebVitals();
