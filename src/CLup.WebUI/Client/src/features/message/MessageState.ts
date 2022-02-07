import {createAsyncThunk} from '@reduxjs/toolkit';

import ApiCaller from '../../shared/api/ApiCaller';
import {MarkMessageAsDeleted, SendMessage} from './Message';
import {RootState} from '../../app/Store';

const DEFAULT_MESSAGE_COMMAND_ROUTE = 'api/message';

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

const getMessages = (state: RootState) => Object.values(state.entities.messages);

const selectReceivedMessagesById = (state: RootState, id?: string) =>
    getMessages(state).filter((message) => message.receiverId === id);

const selectSentMessagesById = (state: RootState, id?: string) =>
    getMessages(state).filter((message) => message.senderId === id);

export const selectReceivedBusinessMessages = (state: RootState) =>
    selectReceivedMessagesById(state, state.business.current?.id);

export const selectSentBusinessMessages = (state: RootState) =>
    selectSentMessagesById(state, state.business.current?.id);

export const selectReceivedUserMessages = (state: RootState) =>
    selectReceivedMessagesById(state, state.user.current?.id);

export const selectSentUserMessages = (state: RootState) =>
    selectSentMessagesById(state, state.user.current?.id);
