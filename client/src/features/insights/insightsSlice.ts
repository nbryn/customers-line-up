import {createAsyncThunk, createSlice, isAnyOf} from '@reduxjs/toolkit';

import ApiCaller from '../../common/api/useApi';
import {UserInsights} from './Insights';
import {RootState} from '../../app/Store';

const DEFAULT_INSIGHTS_ROUTE = 'insights';

interface InsightsState {
    userInsights: UserInsights | null;
    isLoading: boolean;
    apiMessage: string;
}

const initialState: InsightsState = {
    userInsights: null,
    isLoading: false,
    apiMessage: '',
};

export const fetchUserInsights = createAsyncThunk('insights/user', async () => {
    const userInsights = await ApiCaller.get<UserInsights>(`${DEFAULT_INSIGHTS_ROUTE}/user`);

    return userInsights;
});

export const insightsSlice = createSlice({
    name: 'insights',
    initialState,
    reducers: {
        clearApiMessage: (state) => {
            state.apiMessage = '';
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchUserInsights.fulfilled, (state, action) => {
            state.isLoading = false;
            state.userInsights = action.payload;
        });

        builder.addMatcher(isAnyOf(fetchUserInsights.pending), (state) => {
            state.isLoading = true;
        });

        builder.addMatcher(isAnyOf(fetchUserInsights.rejected), (state, action) => {
            state.isLoading = false;
            state.apiMessage = action.error.message!;
        });
    },
});

export const {clearApiMessage} = insightsSlice.actions;

export const selectUserInsights = (state: RootState) => state.insights.userInsights;

export default insightsSlice.reducer;
