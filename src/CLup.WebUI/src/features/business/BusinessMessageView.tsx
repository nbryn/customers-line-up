import React from 'react';
import {Container} from 'react-bootstrap';

import {MessageContainer} from '../../shared/containers/MessageContainer';
import {useAppSelector} from '../../app/Store';
import {selectCurrentBusiness} from './BusinessState';

export const BusinessMessageView: React.FC = () => {
    const business = useAppSelector(selectCurrentBusiness);
    return (
        <Container>
            <MessageContainer
                receivedMessages={business?.receivedMessages ?? []}
                sentMessages={business?.sentMessages ?? []}
                sendMessage={(message: any) => console.log(message)}
            />
        </Container>
    );
};
