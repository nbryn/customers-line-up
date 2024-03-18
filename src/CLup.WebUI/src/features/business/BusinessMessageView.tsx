import React from 'react';
import {Container} from 'react-bootstrap';

import {MessageContainer} from '../../shared/containers/MessageContainer';
import {selectReceivedBusinessMessages, selectSentBusinessMessages} from '../../features/message/MessageState';
import type {SendMessage} from '../../features/message/Message';
import {useAppSelector} from '../../app/Store';

export const BusinessMessageView: React.FC = () => {
    return (
        <Container>
            <MessageContainer
                receivedMessages={useAppSelector(selectReceivedBusinessMessages)}
                sentMessages={useAppSelector(selectSentBusinessMessages)}
                sendMessage={(message: SendMessage) => console.log(message)}
            />
        </Container>
    );
};
