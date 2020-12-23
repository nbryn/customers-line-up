import Cookies from 'js-cookie';
import React, {useContext, useEffect, useState} from 'react';

import {UserDTO} from '../models/User';

export type ContextValue = {
   user: UserDTO;
   logout: () => void;
   setUser: (user: UserDTO) => void;
   userLoggedIn: boolean;
};

export const UserContext = React.createContext<ContextValue>({
   user: (Cookies.get('user') as unknown) as UserDTO,
   setUser: () => null,
   logout: () => null,
   userLoggedIn: false,
});

type Props = {
   children: React.ReactNode;
};

export const UserContextProvider: React.FC<Props> = (props: Props) => {
   const [user, setCurrentUser] = useState<UserDTO>({name: '', email: '', zip: '', token: ''});
   const [userLoggedIn, setUserLoggedIn] = useState<boolean>(false);

   console.log(user);

   const setUser = (user: UserDTO) => {
      setUserLoggedIn(true);
      setCurrentUser(user);

      Cookies.set('token', user.token!);
      Cookies.set('user', user);
   };

   const logout = () => {
      Cookies.remove('user');
      Cookies.remove('token');
      
      setUserLoggedIn(false);
   };

   useEffect(() => {
      if (Cookies.get('user')) {
         const user = (Cookies.get('user') as unknown) as UserDTO;

         setCurrentUser(user);
         setUserLoggedIn(true);
      }
   }, []);

   const contextValue: ContextValue = {
      user,
      logout,
      setUser,
      userLoggedIn,
   };

   return <UserContext.Provider value={contextValue}>{props.children}</UserContext.Provider>;
};

export const useUserContext = (): ContextValue => {
   const context = useContext(UserContext);

   return context;
};
