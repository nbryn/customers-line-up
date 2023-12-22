import {configureStore} from '@reduxjs/toolkit';
import {TypedUseSelectorHook, useDispatch, useSelector} from 'react-redux';

import apiReducer from '../shared/api/ApiState';
import businessReducer from '../features/business/BusinessState';
import entityReducer from './EntityState';
import userReducer from '../features/user/UserState';

export const store = configureStore({
    reducer: {
        api: apiReducer,
        entities: entityReducer,
        user: userReducer,
        business: businessReducer,
    },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
