import React from 'react';
import {useParams} from 'react-router-dom';
import {Container} from 'react-bootstrap';

import {MessageContainer} from '../../shared/containers/MessageContainer';
import {useGetBusinessAggregateByIdQuery} from './BusinessApi';

export const BusinessMessageView: React.FC = () => {
    const {businessId} = useParams();
    const {data: business} = useGetBusinessAggregateByIdQuery(businessId!);
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
