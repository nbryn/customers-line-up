import Cookies from 'js-cookie';
import {createAsyncThunk, createSlice, isAnyOf} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/ApiCaller';
import {LoginDTO, NotEmployedByBusiness, UserDTO} from './User';
import {RootState} from '../../app/Store';

const DEFAULT_USER_ROUTE = 'user';

export interface UserState {
    notEmployedByBusiness: {[businessId: string]: UserDTO[]};
    currentUser: UserDTO | null;
}

const initialState: UserState = {
    notEmployedByBusiness: {},
    currentUser: null,
};

export const login = createAsyncThunk('user/login', async (data: LoginDTO) => {
    const user = await ApiCaller.post<UserDTO, LoginDTO>(`${DEFAULT_USER_ROUTE}/login`, data);

    return user;
});

export const register = createAsyncThunk('user/register', async (data: UserDTO) => {
    const user = await ApiCaller.post<UserDTO, UserDTO>(`${DEFAULT_USER_ROUTE}/register`, data);

    return user;
});

export const fetchUserInfo = createAsyncThunk('user/userInfo', async () => {
    const user = await ApiCaller.get<UserDTO>(`${DEFAULT_USER_ROUTE}`);

    return user;
});

export const fetchUsersNotEmployedByBusiness = createAsyncThunk(
    'user/notEmployedByBusiness',
    async (businessId: string) => {
        const notEmployedByBusiness = await ApiCaller.get<NotEmployedByBusiness>(
            `${DEFAULT_USER_ROUTE}/all/${businessId}`
        );

        return notEmployedByBusiness;
    }
);

export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        clearCurrentUser: (state) => {
            state.currentUser = null;
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchUsersNotEmployedByBusiness.fulfilled, (state, action) => {
            state.notEmployedByBusiness[action.payload.businessId] = action.payload.users;
        });

        builder.addCase(fetchUserInfo.fulfilled, (state, action) => {
            state.currentUser = action.payload;
        });

        builder.addMatcher(isAnyOf(login.fulfilled, register.fulfilled), (state, action) => {
            state.currentUser = action.payload;
            Cookies.set('access_token', action.payload.token!);
        });

    },
});

export const {clearCurrentUser} = userSlice.actions;

export const selectUsersNotEmployedByBusiness = (state: RootState, businessId: string) =>
    state.users.notEmployedByBusiness[businessId] ?? null;

export const selectUsersAsComboBoxOption = (businessId: string) => (state: RootState) => {
    const usersNotEmployed = selectUsersNotEmployedByBusiness(state, businessId);

    if (!usersNotEmployed) return null;

    return Object.values(usersNotEmployed).map((user) => ({
        label: user.email,
        value: user.name,
    }));
};

export const selectCurrentUser = (state: RootState) => state.users.currentUser;

export default userSlice.reducer;
