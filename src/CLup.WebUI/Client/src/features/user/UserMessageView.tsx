import React from 'react';
import {Container} from 'react-bootstrap';

import {MessageContainer} from '../../shared/containers/MessageContainer';
import {
    selectReceivedUserMessages,
    selectSentUserMessages,
} from '../../features/message/MessageState';
import {SendMessage} from '../../features/message/Message';
import {sendMessage} from '../../features/message/MessageState';
import {useAppDispatch, useAppSelector} from '../../app/Store';

export const UserMessageView: React.FC = () => {
    const dispatch = useAppDispatch();

    return (
        <Container>
            <MessageContainer
                receivedMessages={useAppSelector(selectReceivedUserMessages)}
                sentMessages={useAppSelector(selectSentUserMessages)}
                sendMessage={(message: SendMessage) => dispatch(sendMessage(message))}
            />
        </Container>
    );
};
