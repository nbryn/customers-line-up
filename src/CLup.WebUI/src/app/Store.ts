import {configureStore} from '@reduxjs/toolkit';
import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import {setupListeners} from '@reduxjs/toolkit/query';

import apiReducer from '../shared/api/ApiState';
import {type TypedUseSelectorHook, useDispatch, useSelector} from 'react-redux';

export const USER_TAG = 'User';
// TODO: Business_Tag needs an id to know which business to invalidate
// https://redux-toolkit.js.org/rtk-query/usage/automated-refetching
export const BUSINESS_AGGREGATE_TAG = 'BusinessAggregate';
export const BUSINESS_TAG = 'Business';
export const BUSINESS_ALL_TAG = 'AllBusinesses';
export const BUSINESS_BY_OWNER_TAG = 'BusinessByOwner';
export const BASE_URL = '/api';

export const baseApi = createApi({
    baseQuery: fetchBaseQuery({baseUrl: BASE_URL}),
    tagTypes: [
        BUSINESS_TAG,
        BUSINESS_AGGREGATE_TAG,
        BUSINESS_ALL_TAG,
        BUSINESS_BY_OWNER_TAG,
        USER_TAG,
    ],
    endpoints: () => ({}),
});

export const store = configureStore({
    reducer: {
        apiInfo: apiReducer,
        [baseApi.reducerPath]: baseApi.reducer,
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(baseApi.middleware),
});

setupListeners(store.dispatch);

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
