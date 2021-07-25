import Cookies from 'js-cookie';
import React, {useContext, useEffect} from 'react';

import {clearCurrentUser, fetchUserInfo} from './userSlice';
import { useAppDispatch } from '../../app/Store';

export type ContextValue = {
    logout: () => void;
};

export const UserContext = React.createContext<ContextValue>({
    logout: () => null,
});

type Props = {
    children: React.ReactNode;
};

export const UserContextProvider: React.FC<Props> = (props: Props) => {
    const dispatch = useAppDispatch();

    const logout = () => {
        Cookies.remove('access_token');

        dispatch(clearCurrentUser());
    };


    useEffect(() => {
        if (Cookies.get('access_token')) dispatch(fetchUserInfo());
        setTimeout(() => logout(), 7200000);
    }, []);

    const contextValue: ContextValue = {
        logout,
    };

    return <UserContext.Provider value={contextValue}>{props.children}</UserContext.Provider>;
};

export const useUserContext = (): ContextValue => {
    const context = useContext(UserContext);

    return context;
};
