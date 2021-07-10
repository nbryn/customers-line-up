import {createAsyncThunk, createSlice} from '@reduxjs/toolkit';

import {Request, RootState} from '../../app/Store';
import {UserDTO} from './User';
import {UserService} from './UserService';

export interface UserState {
    notEmployedByBusiness: {[businessId: string]: UserDTO[]};
}

const initialState: UserState = {
    notEmployedByBusiness: {},
};

export const fetchUsersNotEmployedByBusiness = createAsyncThunk(
    'user/notEmployedByBusiness',
    async ({service, data}: Request<UserService, string>) => {
        const response = await service.fetchAllUsersNotEmployedByBusiness(data);

        return response;
    }
);

export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchUsersNotEmployedByBusiness.fulfilled, (state, action) => {
            state.notEmployedByBusiness![action.payload.businessId] = action.payload.users;
        });
    },
});

export const selectUsersNotEmployedByBusiness = (state: RootState) =>
    state.users.notEmployedByBusiness;

export const selectUsersAsComboBoxOption = (state: RootState, businessId: string) => {
    const usersNotEmployed = selectUsersNotEmployedByBusiness(state);

    if (!usersNotEmployed[businessId]) return null;

    return Object.values(usersNotEmployed[businessId]).map((user) => ({
        label: user.email,
        value: user.name,
    }));
};

export default userSlice.reducer;
