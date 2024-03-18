import {createAsyncThunk, createSlice} from '@reduxjs/toolkit';

import ApiCaller from '../../shared/api/ApiCaller';
import type {BusinessDTO} from './Business';
import {callApiAndFetchUser, selectCurrentUser} from '../user/UserState';
import type {RootState} from '../../app/Store';

const DEFAULT_BUSINESS_QUERY_ROUTE = 'api/query/business';
const DEFAULT_BUSINESS_COMMAND_ROUTE = 'api/business';

interface BusinessState {
    types: string[];
    current: BusinessDTO | null;
}

const initialBusinessState: BusinessState = {
    types: [],
    current: null,
};

export const createBusiness = createAsyncThunk(
    'business/create',
    async (data: BusinessDTO, thunkAPI) => {
        callApiAndFetchUser(
            thunkAPI,
            async () => await ApiCaller.post(`${DEFAULT_BUSINESS_COMMAND_ROUTE}`, data)
        );
    }
);

export const fetchAllBusinesses = createAsyncThunk('business/fetchAll', async () => {
    const businesses = await ApiCaller.get<BusinessDTO[]>(`${DEFAULT_BUSINESS_QUERY_ROUTE}/all`);

    return businesses;
});

export const fetchBusinessesTypes = createAsyncThunk('business/fetchBusinessTypes', async () => {
    const businessTypes = ApiCaller.get<string[]>(`${DEFAULT_BUSINESS_QUERY_ROUTE}/types`);

    return businessTypes;
});

export const updateBusinessInfo = createAsyncThunk(
    'business/update',
    async (data: {businessId: string; ownerEmail: string; business: BusinessDTO}, thunkAPI) => {
        callApiAndFetchUser(
            thunkAPI,
            async () =>
                await ApiCaller.put(`${DEFAULT_BUSINESS_COMMAND_ROUTE}/${data.businessId}`, {
                    ...data.business,
                    id: data.businessId,
                    ownerEmail: data.ownerEmail,
                })
        );
    }
);

export const businessSlice = createSlice({
    name: 'business',
    initialState: initialBusinessState,
    reducers: {
        setCurrentBusiness: (state, {payload}) => {
            state.current = payload;
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchBusinessesTypes.fulfilled, (state, {payload}) => {
            state.types = payload;
        });
    },
});

export const selectAllBusinesses = (state: RootState) => Object.values(state.entities.businesses);

export const selectCurrentBusiness = (state: RootState) => state.business.current;

export const selectBusinessesByOwner = (state: RootState) =>
    selectAllBusinesses(state).filter((b) => b.ownerEmail === selectCurrentUser(state)?.email);

export const selectBusinessTypes = (state: RootState) => state.business.types;

export const {setCurrentBusiness} = businessSlice.actions;

export default businessSlice.reducer;
