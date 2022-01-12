import React from 'react';
import {Container} from 'react-bootstrap';

import {ErrorView} from '../../shared/views/ErrorView';
import {fetchBusinessMessages, selectBusinessMessages, selectCurrentBusiness} from './businessSlice';
import {MessageContainer} from '../../shared/containers/MessageContainer';
import {SendMessage} from '../../shared/models/General';
import {useAppDispatch, useAppSelector} from '../../app/Store';

export const BusinessMessageView: React.FC = () => {
    const dispatch = useAppDispatch();

    const business = useAppSelector(selectCurrentBusiness);
    const messageResponse = useAppSelector(selectBusinessMessages);

    if (!business) {
        return <ErrorView />;
    }

    return (
        <Container>
            <MessageContainer
                messageResponse={messageResponse}
                fetchData={() => dispatch(fetchBusinessMessages(business.id!))}
                sendMessage={(message: SendMessage) => console.log(message)}
            />
        </Container>
    );
};
