import Cookies from 'js-cookie';

import { BASE_URL } from './Url';
import { fetchFromServer } from './ApiService';
import { UserDTO } from '../models/dto/User';


export type LoginRequest = {
   email: string;
   password: string;
};

export type LoginResponse = {
   email: string;
   name: string;
   zip: string;
   token: string;
   isOwner: boolean;
}

async function login(request: LoginRequest): Promise<UserDTO> {
   const response: LoginResponse = await fetchFromServer<LoginResponse>(BASE_URL + 'user/login', 'post', request);

   Cookies.set('token', response.token);

   const user: UserDTO = {
      name: response.name,
      email: response.email,
      zip: response.zip,
      isOwner: response.isOwner as boolean
   };

   return user;
}


// async function signup(request) {
//    const user = await fetchFromServer(BASE_URL + 'user/register', 'post', request);

//    return user;
// }

export default {
   login,
};
