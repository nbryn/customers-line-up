import React from 'react';
import {Container} from 'react-bootstrap';

import {ErrorView} from '../../shared/views/ErrorView';
import {
    fetchUserMessages,
    selectCurrentUser,
    selectUserMessages,
    sendUserMessage,
} from './userSlice';
import {MessageContainer} from '../../shared/containers/MessageContainer';
import {SendMessage} from '../../shared/models/General';
import {useAppDispatch, useAppSelector} from '../../app/Store';

export const UserMessageView: React.FC = () => {
    const dispatch = useAppDispatch();

    const user = useAppSelector(selectCurrentUser);
    const messageResponse = useAppSelector(selectUserMessages);

    if (!user) {
        return <ErrorView />;
    }

    return (
        <Container>
            <MessageContainer
                messageResponse={messageResponse}
                fetchData={() => dispatch(fetchUserMessages(user.id!))}
                sendMessage={(message: SendMessage) => dispatch(sendUserMessage(message))}
            />
        </Container>
    );
};
