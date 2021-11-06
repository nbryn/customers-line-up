import {createAsyncThunk, createSlice} from '@reduxjs/toolkit';

import ApiCaller from '../../shared/api/ApiCaller';
import {RootState} from '../../app/Store';
import {UserInsights} from './Insights';

const DEFAULT_INSIGHTS_ROUTE = 'insights';
interface InsightsState {
    userInsights: UserInsights | null;
}

const initialState: InsightsState = {
    userInsights: null,
};

export const fetchUserInsights = createAsyncThunk('insights/userBooking', async () => {
    const bookingInsights = await ApiCaller.get<UserInsights>(`user/${DEFAULT_INSIGHTS_ROUTE}`);

    const businessInsights = await ApiCaller.get<UserInsights>(`business/${DEFAULT_INSIGHTS_ROUTE}`);

    return {userInsights: {...bookingInsights, ...businessInsights}};
});

export const insightsSlice = createSlice({
    name: 'insights',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchUserInsights.fulfilled, (state, {payload}) => {
            state.userInsights = payload.userInsights;
        });
    },
});

export const selectUserInsights = (state: RootState) => state.insights.userInsights;

export default insightsSlice.reducer;
