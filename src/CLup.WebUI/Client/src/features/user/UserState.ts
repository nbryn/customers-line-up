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

export const callApiAndFetchAggregate = async (thunkAPI: any, apiCall: () => Promise<void>) => {
    await apiCall();

    thunkAPI.dispatch(fetchUserAggregate());
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
    callApiAndFetchAggregate(thunkAPI, async () => {
        const response = await ApiCaller.post<LoginDTO, TokenResponse>(`auth/login`, data);
        Cookies.set('access_token', response.token);
    });
});

export const register = createAsyncThunk('auth/register', async (data: UserDTO, thunkAPI) => {
    callApiAndFetchAggregate(thunkAPI, async () => {
        const response = await ApiCaller.post<UserDTO, TokenResponse>(`auth/register`, data);
        Cookies.set('access_token', response.token);
    });
});

export const updateUserInfo = createAsyncThunk('user/update', async (data: UserDTO, thunkAPI) => {
    callApiAndFetchAggregate(
        thunkAPI,
        async () => await ApiCaller.put<UserDTO>(`${DEFAULT_USER_COMMAND_ROUTE}/update`, data)
    );
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
    },
});

export const selectUsersNotEmployedByBusiness = (businessId: string) => (state: RootState) =>
    state.user.notEmployedByBusiness[businessId] ?? null;

export const selectCurrentUser = (state: RootState) => state.user.current;

export const {clearCurrentUser} = userSlice.actions;

export default userSlice.reducer;
