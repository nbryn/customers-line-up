import {BrowserRouter as Router} from 'react-router-dom';
import React from 'react';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';

import {UserContextProvider} from '../src/context/UserContext';
import {MainView} from './views/MainView';

ReactDOM.render(
   <React.StrictMode>
      <UserContextProvider>
         <Router>
            <MainView />
         </Router>
      </UserContextProvider>
   </React.StrictMode>,
   document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
