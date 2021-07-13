import Cookies from 'js-cookie';
import React, {useContext, useEffect, useState} from 'react';

import {UserDTO} from './User';

export type ContextValue = {
    user: UserDTO;
    logout: () => void;
};

export const UserContext = React.createContext<ContextValue>({
    user: (Cookies.get('user') as unknown) as UserDTO,
    logout: () => null,
});

type Props = {
    children: React.ReactNode;
};

export const UserContextProvider: React.FC<Props> = (props: Props) => {
    const [user, setCurrentUser] = useState<UserDTO>({email: ''});

    const logout = () => {
        Cookies.remove('access_token');

        setCurrentUser({email: ''});
    };


    useEffect(() => {
        setTimeout(() => logout(), 7200000);
    }, []);

    const contextValue: ContextValue = {
        user,
        logout,
    };

    return <UserContext.Provider value={contextValue}>{props.children}</UserContext.Provider>;
};

export const useUserContext = (): ContextValue => {
    const context = useContext(UserContext);

    return context;
};
