import {createAsyncThunk, createSlice, isAnyOf} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/ApiCaller';
import {ApiInfo} from '../../common/api/ApiInfo';
import {apiError, defaultApiInfo} from '../../common/api/ApiInfo';
import {RootState} from '../../app/Store';
import {State} from '../../app/AppTypes';
import {UserInsights} from './Insights';

const DEFAULT_INSIGHTS_ROUTE = 'insights';

interface InsightsState {
    userInsights: UserInsights | null;
    apiInfo: ApiInfo;
}

const initialState: InsightsState = {
    userInsights: null,
    apiInfo: defaultApiInfo(State.Insights),
};

export const fetchUserInsights = createAsyncThunk('insights/userBooking', async () => {
    const bookingInsights = await ApiCaller.get<UserInsights>(
        `${DEFAULT_INSIGHTS_ROUTE}/user/booking`
    );

    const businessInsights = await ApiCaller.get<UserInsights>(
        `${DEFAULT_INSIGHTS_ROUTE}/user/business`
    );

    return {userInsights: {...bookingInsights, ...businessInsights}};
});

export const insightsSlice = createSlice({
    name: 'insights',
    initialState,
    reducers: {
        clearInsightsApiInfo: (state) => {
            state.apiInfo = defaultApiInfo(State.Insights);
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchUserInsights.fulfilled, (state, {payload}) => {
            state.apiInfo.isLoading = false;
            state.userInsights = payload.userInsights;
        });

        builder.addMatcher(isAnyOf(fetchUserInsights.pending), (state) => {
            state.apiInfo.isLoading = true;
        });

        builder.addMatcher(isAnyOf(fetchUserInsights.rejected), (state, action) => {
            state.apiInfo = apiError({state: State.Insights, message: action.error.message!});
        });
    },
});

export const {clearInsightsApiInfo} = insightsSlice.actions;

export const selectUserInsights = (state: RootState) => state.insights.userInsights;

export default insightsSlice.reducer;
