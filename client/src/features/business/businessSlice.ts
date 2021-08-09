import {createAsyncThunk, createSlice, isAnyOf} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/ApiCaller';
import {apiError, apiSuccess, defaultApiInfo} from '../../common/api/ApiInfo';
import {BusinessDTO} from './Business';
import {NormalizedEntityState, State} from '../../app/AppTypes';
import {RootState} from '../../app/Store';
import {selectCurrentUser} from '../user/userSlice';

const DEFAULT_BUSINESS_ROUTE = 'business';
const BUSINESS_CREATED_MSG = 'Business Created - Go to my businesses to see your businesses';
const BUSINESS_UPDATED_MSG = 'Business Updated';

interface BusinessState extends NormalizedEntityState<BusinessDTO> {
    businessTypes: string[];
}

const initialState: BusinessState = {
    byId: {},
    allIds: [],
    businessTypes: [],
    apiInfo: defaultApiInfo(State.Businesses),
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
    reducers: {
        clearBusinessApiInfo: (state) => {
            state.apiInfo = defaultApiInfo(State.Businesses);
        },
    },
    extraReducers: (builder) => {
        builder.addCase(createBusiness.fulfilled, (state) => {
            state.apiInfo = apiSuccess({state: State.Users, message: BUSINESS_CREATED_MSG});
        });

        builder.addCase(fetchAllBusinesses.fulfilled, (state, {payload}) => {
            state.apiInfo.isLoading = false;
            const newState = {...state.byId};
            payload.forEach((business) => (newState[business.id] = business));

            state.byId = newState;
        });

        builder.addCase(fetchBusinessesByOwner.fulfilled, (state, {payload}) => {
            state.apiInfo.isLoading = false;
            const newState = {...state.byId};
            payload.forEach((business) => (newState[business.id] = business));

            state.byId = newState;
        });

        builder.addCase(fetchBusinessesTypes.fulfilled, (state, {payload}) => {
            state.apiInfo.isLoading = false;
            state.businessTypes = payload;
        });

        builder.addCase(updateBusinessInfo.fulfilled, (state) => {
            state.apiInfo = apiSuccess({state: State.Businesses, message: BUSINESS_UPDATED_MSG});
        });

        builder.addMatcher(
            isAnyOf(
                createBusiness.pending,
                fetchAllBusinesses.pending,
                fetchBusinessesByOwner.pending,
                fetchBusinessesTypes.pending,
                updateBusinessInfo.pending
            ),
            (state) => {
                state.apiInfo.isLoading = true;
            }
        );

        builder.addMatcher(
            isAnyOf(
                createBusiness.rejected,
                fetchAllBusinesses.rejected,
                fetchBusinessesByOwner.rejected,
                fetchBusinessesTypes.rejected,
                updateBusinessInfo.rejected
            ),
            (state, action) => {
                state.apiInfo = apiError({state: State.Businesses, message: action.error.message!});
            }
        );
    },
});

export const {clearBusinessApiInfo} = bookingSlice.actions;

export const selectAllBusinesses = (state: RootState) => Object.values(state.businesses.byId);

export const selectBusinessesByOwner = (state: RootState) =>
    selectAllBusinesses(state).filter((b) => b.ownerEmail === selectCurrentUser(state)?.email);

export const selectBusinessTypes = (state: RootState) => state.businesses.businessTypes;

export default bookingSlice.reducer;
