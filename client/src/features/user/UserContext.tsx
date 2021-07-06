import Cookies from 'js-cookie';
import React, {useContext, useEffect, useState} from 'react';

import {UserDTO} from './User';
import {useUserService} from './UserService';

export type ContextValue = {
    user: UserDTO;
    logout: () => void;
    setUser: (user: UserDTO) => void;
};

export const UserContext = React.createContext<ContextValue>({
    user: (Cookies.get('user') as unknown) as UserDTO,
    setUser: () => null,
    logout: () => null,
});

type Props = {
    children: React.ReactNode;
};

export const UserContextProvider: React.FC<Props> = (props: Props) => {
    const [user, setCurrentUser] = useState<UserDTO>({email: ''});

    const userService = useUserService()

    const setUser = (user: UserDTO) => {
        setCurrentUser(user);

        console.log(user);

        Cookies.set('access_token', user.token!);
    };

    const logout = () => {
        Cookies.remove('access_token');

        setCurrentUser({email: ''});
    };

    useEffect(() => {
        (async () => {
            if (Cookies.get('access_token')) {
                const user = await userService.fetchUserInfo();

                setCurrentUser(user || {email: ''});
            }
        })();
    }, []);

    useEffect(() => {
        setTimeout(() => logout(), 7200000);
    }, []);

    const contextValue: ContextValue = {
        user,
        logout,
        setUser,
    };

    return <UserContext.Provider value={contextValue}>{props.children}</UserContext.Provider>;
};

export const useUserContext = (): ContextValue => {
    const context = useContext(UserContext);

    return context;
};
