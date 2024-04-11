import React from 'react';
import {Container} from 'react-bootstrap';

import {MessageContainer} from '../../shared/containers/MessageContainer';
import {useSendUserMessageMutation} from '../message/MessageApi';
import {useGetUserQuery} from './UserApi';

export const UserMessageView: React.FC = () => {
    const {data: user} = useGetUserQuery();
    const [sendUserMessage] = useSendUserMessageMutation();

    return (
        <Container>
            <MessageContainer
                receivedMessages={user?.receivedMessages ?? []}
                sentMessages={user?.sentMessages ?? []}
                sendMessage={async (message: any) => await sendUserMessage(message)}
            />
        </Container>
    );
};
