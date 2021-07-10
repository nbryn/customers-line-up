import {configureStore, createSlice, PayloadAction} from '@reduxjs/toolkit';
import {TypedUseSelectorHook, useDispatch, useSelector} from 'react-redux';

import employeeReducer from '../features/employee/employeeSlice';
import businessReducer from '../features/business/businessSlice';
import userReducer from '../features/user/userSlice';

export interface NormalizedEntity<T> {
    byId: {[id: string]: T};
    allIds: string[];
}

export type Request<T1, T2, T3 = void> = {
    service: T1;
    data: T2;
    extraParam?: T3;
};

interface AppState {
    isLoading: boolean;
    apiMessage: string;
}

const initialState: AppState = {
    isLoading: false,
    apiMessage: '',
};

export const useLoading = async <T>(action: () => Promise<T>) => {
    const dispatch = useAppDispatch();
    try {
        dispatch(toggleLoading);
        const response = await action();

        return response;
    } catch (e) {
        console.log(e);
    } finally {
        dispatch(toggleLoading);
    }
};

export const appSlice = createSlice({
    name: 'app',
    initialState,
    reducers: {
        toggleLoading: (state) => {
            console.log('loading');
            state.isLoading = !state.isLoading;
        },
        clearApiMessage: (state) => {
            state.apiMessage = '';
        },
        setApiMessage: (state, action: PayloadAction<string>) => {
            state.apiMessage = action.payload;
        },
    },
});

export const store = configureStore({
    reducer: {
        app: appSlice.reducer,
        businesses: businessReducer,
        employees: employeeReducer,
        users: userReducer,
    },
});

export const {clearApiMessage, setApiMessage, toggleLoading} = appSlice.actions;
export const isLoading = (state: RootState) => state.app.isLoading;
export const selectApiMessage = (state: RootState) => state.app.apiMessage;

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
