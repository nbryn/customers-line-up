import React from 'react';
import {BrowserRouter as Router} from 'react-router-dom';
import {Provider} from 'react-redux';
import ReactDOM from 'react-dom/client';
import reportWebVitals from './reportWebVitals';
import {createTheme} from '@mui/material';
import {ThemeProvider} from '@mui/styles';

import {MainView} from './app/MainView';
import {store} from './app/Store';
import {LoginAndRegisterRoutes} from './app/Routes';

const App = () => {
    const theme = createTheme();
    return (
        <ThemeProvider theme={theme}>
            <LoginAndRegisterRoutes />
            <MainView />
        </ThemeProvider>
    );
};

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
    <React.StrictMode>
        <Provider store={store}>
            <Router>
                <App />
            </Router>
        </Provider>
    </React.StrictMode>
);

reportWebVitals();
