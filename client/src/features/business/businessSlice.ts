import {normalize, schema} from 'normalizr';
import {createSlice, PayloadAction} from '@reduxjs/toolkit';

import {BusinessDTO} from './Business';
import {NormalizedEntity, RootState} from '../../app/Store';

export interface BusinessState {
    businesses: NormalizedEntity<BusinessDTO> | null;
}

const initialState: BusinessState = {
    businesses: null,
};

export const userSlice = createSlice({
    name: 'business',
    initialState,
    reducers: {},
});

//export const {setUser} = userSlice.actions;

//export const selectUser = (state: RootState) => state.user.currentUser;

export default userSlice.reducer;
