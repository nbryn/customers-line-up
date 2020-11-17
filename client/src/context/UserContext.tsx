import React, {useContext, useEffect, useState} from 'react';

import {UserDTO} from '../services/dto/User';

export type ContextValue = {
   user: UserDTO;
   logout: () => void;
   setUser: (user: UserDTO) => void;
};

export const UserContext = React.createContext<ContextValue>({
   user: null,
   setUser: () => null,
   logout: () => null,
});

type Props = {
   children: React.ReactNode;
};

export const UserContextProvider: React.FC<Props> = (props: Props) => {
   const [user, setCurrentUser] = useState<UserDTO>(null);

   const setUser = (user: UserDTO) => {
      setCurrentUser(user);

      localStorage.setItem('User', JSON.stringify(user));
   };

   const logout = () => {
      localStorage.removeItem('User');

      setUser(null);
   };

   useEffect(() => {
      if (localStorage.getItem('User')) {
         const user = JSON.parse(localStorage.getItem('User')!) as UserDTO;

         setCurrentUser(user);
      }
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
