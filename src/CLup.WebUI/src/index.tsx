import React from 'react';
import {BrowserRouter} from 'react-router-dom';
import {Provider} from 'react-redux';
import ReactDOM from 'react-dom/client';
import reportWebVitals from './reportWebVitals';
import {createTheme} from '@mui/material';
import {ThemeProvider} from '@mui/styles';

import {AppShell} from './app/AppShell';
import {store} from './app/Store';

const App = () => {
    const theme = createTheme();
    return (
        <ThemeProvider theme={theme}>
            <AppShell />
        </ThemeProvider>
    );
};

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
    <React.StrictMode>
        <Provider store={store}>
            <BrowserRouter>
                <App />
            </BrowserRouter>
        </Provider>
    </React.StrictMode>
);

reportWebVitals();
