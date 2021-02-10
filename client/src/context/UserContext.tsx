import Cookies from 'js-cookie';
import React, {useContext, useEffect, useState} from 'react';

import {UserDTO} from '../models/User';
import {USER_INFO_URL} from '../api/URL';
import {useRequest, RequestHandler} from '../hooks/useRequest';

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

   const requestHandler: RequestHandler<UserDTO> = useRequest();

   const setUser = (user: UserDTO) => {
      setCurrentUser(user);

      Cookies.set('token', user.token!);
   };

   const logout = () => {
      Cookies.remove('token');

      setCurrentUser({email: ''});
   };

   useEffect(() => {
      (async () => {
         if (Cookies.get('token')) {
            const user = await requestHandler.query(USER_INFO_URL);

            setCurrentUser(user || {email: ''});
         }
      })();
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
