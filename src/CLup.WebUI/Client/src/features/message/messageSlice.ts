import {createAsyncThunk, createSlice} from '@reduxjs/toolkit';

import ApiCaller from '../../shared/api/ApiCaller';
import {NormalizedEntityState} from '../../app/AppTypes';
import {MarkMessageAsDeleted, MessageDTO, SendMessage} from './Message';

const DEFAULT_MESSAGE_COMMAND_ROUTE = 'api/message';

const initialState: NormalizedEntityState<MessageDTO> = {
    byId: {},
    allIds: [],
};

export const fetchBusinessMessages = createAsyncThunk(
    'business/messages',
    async (businessId: string) => {
        const messages = await ApiCaller.get<MessageDTO[]>(`business/${businessId}/messages`);

        return messages;
    }
);

export const fetchUserMessages = createAsyncThunk('user/messages', async (userId: string) => {
    const messages = await ApiCaller.get<MessageDTO[]>(`user/${userId}/messages`);

    return messages;
});

export const markMessageAsDeleted = createAsyncThunk(
    'message/delete',
    async (message: MarkMessageAsDeleted) => {
        await ApiCaller.post(`${DEFAULT_MESSAGE_COMMAND_ROUTE}/delete`, message);

        return message;
    }
);

export const sendMessage = createAsyncThunk('message/send', async (message: SendMessage) => {
    await ApiCaller.put(`${DEFAULT_MESSAGE_COMMAND_ROUTE}/send`, message);
});

export const messageSlice = createSlice({
    name: 'message',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchUserMessages.fulfilled, (state, {payload}) => {
                const newState = {...state.byId};
                payload.forEach((business) => (newState[business.id] = business));

                state.byId = newState;
            })

            .addCase(fetchBusinessMessages.fulfilled, (state, {payload}) => {
                const newState = {...state.byId};
                payload.forEach((business) => (newState[business.id] = business));

                state.byId = newState;
            })

            .addCase(markMessageAsDeleted.fulfilled, (state, {payload}) => {
                const message = state.byId[payload.messageId];

                if (payload.forSender) message.deletedBySender = true;
                else message.deletedByReceiver = true;
            });
    },
});
