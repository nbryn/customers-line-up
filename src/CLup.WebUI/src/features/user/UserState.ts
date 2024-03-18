import Cookies from 'js-cookie';
import {createAsyncThunk, createSlice} from '@reduxjs/toolkit';

import ApiCaller from '../../shared/api/ApiCaller';
import type {LoginDTO, NotEmployedByBusiness, TokenResponse, UserDTO} from './User';
import type {RootState} from '../../app/Store';

const DEFAULT_USER_QUERY_ROUTE = 'api/query/user';
const DEFAULT_USER_COMMAND_ROUTE = 'api/user';

export interface UserState {
    notEmployedByBusiness: {[businessId: string]: UserDTO[]};
}

const initialState: UserState = {
    notEmployedByBusiness: {},
};

export const callApiAndFetchUser = async (thunkAPI: any, apiCall: () => Promise<void>) => {
    await apiCall();

    thunkAPI.dispatch(fetchUser());
};

export const fetchUser = createAsyncThunk('user', async () => {
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
    callApiAndFetchUser(thunkAPI, async () => {
        const response = await ApiCaller.post<LoginDTO, TokenResponse>(`auth/login`, data);
        Cookies.set('access_token', response.token);
    });
});

export const register = createAsyncThunk('auth/register', async (data: UserDTO, thunkAPI) => {
    callApiAndFetchUser(thunkAPI, async () => {
        const response = await ApiCaller.post<UserDTO, TokenResponse>(`auth/register`, data);
        Cookies.set('access_token', response.token);
    });
});

export const updateUserInfo = createAsyncThunk('user/update', async (data: UserDTO, thunkAPI) => {
    callApiAndFetchUser(
        thunkAPI,
        async () => await ApiCaller.put<UserDTO>(`${DEFAULT_USER_COMMAND_ROUTE}/update`, data)
    );
});

export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchUsersNotEmployedByBusiness.fulfilled, (state, {payload}) => {
            state.notEmployedByBusiness[payload.businessId] = payload.users;
        });
    },
});

export const selectUsersNotEmployedByBusiness = (businessId: string) => (state: RootState) =>
    state.user.notEmployedByBusiness[businessId] ?? null;

export const selectCurrentUser = (state: RootState) => Object.values(state.entities.user)[0];

export default userSlice.reducer;
