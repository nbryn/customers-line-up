import {createSlice} from '@reduxjs/toolkit';
import {QueryStatus} from '@reduxjs/toolkit/query';

import {useAppSelector, type RootState} from '../../app/Store';

export type ToastInfo = {
    buttonText: string;
    navigateTo: string;
};

export interface ApiState {
    error: boolean;
    message: string;
    toastInfo?: ToastInfo;
}

const initialState: ApiState = {
    error: false,
    message: '',
    toastInfo: undefined,
};

export const apiSlice = createSlice({
    name: 'api',
    initialState,
    reducers: {
        setApiState: (state, {payload}) => {
            state.error = payload.error;
            state.message = payload.message;
            state.toastInfo = payload.toastInfo;
        },
        clearApiState: (state) => {
            state.error = false;
            state.message = '';
            state.toastInfo = undefined;
        },
    },
});

export const isLoading = useAppSelector((state) => {
    return Object.values(state.api.queries).some((query) => {
        return query && query.status === QueryStatus.pending;
    });
});

export const {clearApiState, setApiState} = apiSlice.actions;

export const selectApiState = (state: RootState) => state.api;

export default apiSlice.reducer;
