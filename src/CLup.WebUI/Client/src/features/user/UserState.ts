import Cookies from 'js-cookie';
import {createAsyncThunk, createSlice} from '@reduxjs/toolkit';
import {omit} from 'lodash-es';

import ApiCaller from '../../shared/api/ApiCaller';
import {LoginDTO, NotEmployedByBusiness, TokenResponse, UserDTO} from './User';
import {RootState} from '../../app/Store';

const DEFAULT_USER_QUERY_ROUTE = 'api/query/user';
const DEFAULT_USER_COMMAND_ROUTE = 'api/user';

export interface UserState {
    notEmployedByBusiness: {[businessId: string]: UserDTO[]};
    current: UserDTO | null;
}

const initialState: UserState = {
    notEmployedByBusiness: {},
    current: null,
};

export const fetchUserAggregate = createAsyncThunk('user/aggregate', async () => {
    const user = await ApiCaller.get<UserDTO>(`${DEFAULT_USER_QUERY_ROUTE}`);

    return user;
});

export const fetchUsersNotEmployedByBusiness = createAsyncThunk(
    'user/notEmployedByBusiness',
    async (businessId: string) => {
        const notEmployedByBusiness = await ApiCaller.get<NotEmployedByBusiness>(
            `${DEFAULT_USER_QUERY_ROUTE}/notEmployedByBusiness/${businessId}`
        );

        return notEmployedByBusiness;
    }
);

export const login = createAsyncThunk('auth/login', async (data: LoginDTO, thunkAPI) => {
    const response = await ApiCaller.post<TokenResponse, LoginDTO>(`auth/login`, data);
    Cookies.set('access_token', response.token);

    thunkAPI.dispatch(fetchUserAggregate());
});

export const register = createAsyncThunk('auth/register', async (data: UserDTO, thunkAPI) => {
    const response = await ApiCaller.post<TokenResponse, UserDTO>(`auth/register`, data);
    Cookies.set('access_token', response.token);
    
    thunkAPI.dispatch(fetchUserAggregate());
});

export const updateUserInfo = createAsyncThunk('user/update', async (data: UserDTO) => {
    await ApiCaller.put<UserDTO, UserDTO>(`${DEFAULT_USER_COMMAND_ROUTE}/update`, data);

    return data;
});

export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        clearCurrentUser: (state) => {
            state.current = null;
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchUsersNotEmployedByBusiness.fulfilled, (state, {payload}) => {
                state.notEmployedByBusiness[payload.businessId] = payload.users;
            })

            .addCase(fetchUserAggregate.fulfilled, (state, {payload}) => {
                state.current = omit(payload, ['bookings', 'businesses', 'messages']) as UserDTO;
            })

            .addCase(updateUserInfo.fulfilled, (state, {payload}) => {
                state.current = payload;
            })
    },
});

export const selectUsersNotEmployedByBusiness = (businessId: string) => (state: RootState) =>
    state.user.notEmployedByBusiness[businessId] ?? null;

export const selectCurrentUser = (state: RootState) => state.user.current;

export const {clearCurrentUser} = userSlice.actions;

export default userSlice.reducer;
