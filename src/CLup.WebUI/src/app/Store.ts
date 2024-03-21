import {configureStore} from '@reduxjs/toolkit';
import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import {setupListeners} from '@reduxjs/toolkit/query';

export const USER_TAG = 'User';
// TODO: Business_Tag needs an id to know which business to invalidate
// https://redux-toolkit.js.org/rtk-query/usage/automated-refetching
export const BUSINESS_TAG = 'Business';
export const BUSINESS_BY_OWNER_TAG = 'BusinessByOwner';
export const BASE_URL = '/api';
export const emptySplitApi = createApi({
    baseQuery: fetchBaseQuery({baseUrl: BASE_URL}),
    tagTypes: [BUSINESS_TAG, BUSINESS_BY_OWNER_TAG, USER_TAG],
    endpoints: () => ({}),
});

export const store = configureStore({
    reducer: {
        [emptySplitApi.reducerPath]: emptySplitApi.reducer,
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(emptySplitApi.middleware),
});

setupListeners(store.dispatch);
