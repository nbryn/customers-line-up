import Cookies from 'js-cookie';
import {createAsyncThunk, createSlice, isAnyOf} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/ApiCaller';
import {ApiInfo} from '../../common/api/ApiInfo';
import {apiError, defaultApiInfo} from '../../common/api/ApiInfo';
import {LoginDTO, NotEmployedByBusiness, UserDTO} from './User';
import {RootState} from '../../app/Store';
import {State} from '../../app/AppTypes';

const DEFAULT_USER_ROUTE = 'user';
const LOGIN_FAILED_MSG = 'Wrong Email/Password';

export interface UserState {
    notEmployedByBusiness: {[businessId: string]: UserDTO[]};
    currentUser: UserDTO | null;
    apiInfo: ApiInfo;
}

const initialState: UserState = {
    notEmployedByBusiness: {},
    currentUser: null,
    apiInfo: defaultApiInfo(State.Users),
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
        clearUserApiInfo: (state) => {
            state.apiInfo = defaultApiInfo(State.Users);
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchUsersNotEmployedByBusiness.fulfilled, (state, action) => {
            state.notEmployedByBusiness[action.payload.businessId] = action.payload.users;
        });

        builder.addCase(login.rejected, (state) => {
            state.apiInfo = apiError({state: State.Users, message: LOGIN_FAILED_MSG});
        });

        builder.addCase(fetchUserInfo.fulfilled, (state, action) => {
            state.currentUser = action.payload;
        });

        builder.addMatcher(isAnyOf(login.fulfilled, register.fulfilled), (state, action) => {
            state.apiInfo.isLoading = true;
            state.currentUser = action.payload;
            Cookies.set('access_token', action.payload.token!);
        });

        builder.addMatcher(
            isAnyOf(login.pending, register.pending, fetchUsersNotEmployedByBusiness.pending),
            (state) => {
                state.apiInfo.isLoading = true;
            }
        );

        builder.addMatcher(
            isAnyOf(register.rejected, fetchUsersNotEmployedByBusiness.rejected),
            (state, action) => {
                state.apiInfo = apiError({state: State.Users, message: action.error.message!});
            }
        );
    },
});

export const {clearCurrentUser, clearUserApiInfo} = userSlice.actions;

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
