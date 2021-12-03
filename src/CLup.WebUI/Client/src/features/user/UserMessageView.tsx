import React from 'react';
import {Container} from 'react-bootstrap';

import {ErrorView} from '../../shared/views/ErrorView';
import {fetchUserMessages, selectCurrentUser, selectUserMessages} from './userSlice';
import {MessageContainer} from '../../shared/containers/MessageContainer';
import {useAppDispatch, useAppSelector} from '../../app/Store';

export const UserMessageView: React.FC = () => {
    const dispatch = useAppDispatch();

    const user = useAppSelector(selectCurrentUser);
    const messageResponse = useAppSelector(selectUserMessages);

    if (!user) {
        return <ErrorView />;
    }

    console.log(messageResponse);
    return (
        <Container>
            <MessageContainer
                messageResponse={messageResponse}
                fetchData={() => dispatch(fetchUserMessages(user.id!))}
            />
        </Container>
    );
};
