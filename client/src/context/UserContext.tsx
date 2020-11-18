import React, {useContext, useEffect, useState} from 'react';

import {UserDTO} from '../services/dto/User';

export type ContextValue = {
   user: UserDTO;
   token: string;
   logout: () => void;
   setUser: (user: UserDTO) => void;
   userLoggedIn: boolean;
};

const initialUserState: UserDTO = {email: '', name: '', zip: '', token: ''};

export const UserContext = React.createContext<ContextValue>({
   user: initialUserState,
   token: '',
   setUser: () => null,
   logout: () => null,
   userLoggedIn: false,
});

type Props = {
   children: React.ReactNode;
};

export const UserContextProvider: React.FC<Props> = (props: Props) => {
   const [user, setCurrentUser] = useState<UserDTO>(initialUserState);
   const [token, setToken] = useState<string>('');
   const [userLoggedIn, setUserLoggedIn] = useState<boolean>(false);

   const setUser = (user: UserDTO) => {
      setUserLoggedIn(true);
      setCurrentUser(user);
      setToken(user.token);

      localStorage.setItem('User', JSON.stringify(user));
   };

   const logout = () => {
      localStorage.removeItem('User');

      setUser(initialUserState);
      setUserLoggedIn(false);
   };

   useEffect(() => {
      if (localStorage.getItem('User')) {
         const user = JSON.parse(localStorage.getItem('User')!) as UserDTO;

         setCurrentUser(user);
         setToken(user.token);
      }
   }, []);

   const contextValue: ContextValue = {
      user,
      token,
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
