import {createAsyncThunk, createSlice} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/ApiCaller';
import {BusinessDTO} from './Business';
import {NormalizedEntityState} from '../../app/AppTypes';
import {RootState} from '../../app/Store';
import {selectCurrentUser} from '../user/userSlice';

const DEFAULT_BUSINESS_ROUTE = 'business';
interface BusinessState extends NormalizedEntityState<BusinessDTO> {
    businessTypes: string[];
}

const initialState: BusinessState = {
    byId: {},
    allIds: [],
    businessTypes: [],
};

export const createBusiness = createAsyncThunk('business/create', async (data: BusinessDTO) => {
    await ApiCaller.post(`${DEFAULT_BUSINESS_ROUTE}`, data);
});

export const fetchAllBusinesses = createAsyncThunk('business/fetchAll', async () => {
    const businesses = await ApiCaller.get<BusinessDTO[]>(`${DEFAULT_BUSINESS_ROUTE}/all`);

    return businesses;
});

export const fetchBusinessesByOwner = createAsyncThunk('business/fetchByOwner', async () => {
    const businesses = ApiCaller.get<BusinessDTO[]>(`${DEFAULT_BUSINESS_ROUTE}/owner`);

    return businesses;
});

export const fetchBusinessesTypes = createAsyncThunk('business/fetchBusinessTypes', async () => {
    const businessTypes = ApiCaller.get<string[]>(`${DEFAULT_BUSINESS_ROUTE}/types`);

    return businessTypes;
});

export const updateBusinessInfo = createAsyncThunk(
    'business/update',
    async (data: {businessId: string; ownerEmail: string; business: BusinessDTO}) => {
        await ApiCaller.put(`${DEFAULT_BUSINESS_ROUTE}/${data.businessId}`, {
            ...data.business,
            id: data.businessId,
            ownerEmail: data.ownerEmail,
        });
    }
);

export const bookingSlice = createSlice({
    name: 'business',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchAllBusinesses.fulfilled, (state, {payload}) => {
            const newState = {...state.byId};
            payload.forEach((business) => (newState[business.id] = business));

            state.byId = newState;
        });

        builder.addCase(fetchBusinessesByOwner.fulfilled, (state, {payload}) => {
            const newState = {...state.byId};
            payload.forEach((business) => (newState[business.id] = business));

            state.byId = newState;
        });

        builder.addCase(fetchBusinessesTypes.fulfilled, (state, {payload}) => {
            state.businessTypes = payload;
        });
    },
});

export const selectAllBusinesses = (state: RootState) => Object.values(state.businesses.byId);

export const selectBusinessesByOwner = (state: RootState) =>
    selectAllBusinesses(state).filter((b) => b.ownerEmail === selectCurrentUser(state)?.email);

export const selectBusinessTypes = (state: RootState) => state.businesses.businessTypes;

export default bookingSlice.reducer;
