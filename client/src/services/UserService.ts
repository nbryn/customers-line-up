import {BASE_URL} from './Url';
import {fetchFromServer} from './Fetch';
import {UserDTO} from './dto/User';

export type LoginRequest = {
   email: string;
   password: string;
};

async function login(request: LoginRequest): Promise<UserDTO> {
   const user: UserDTO = await fetchFromServer<UserDTO>(BASE_URL + 'user/login', 'post', request);

   return user;
}

async function signup(request) {
   const user = await fetchFromServer(BASE_URL + 'user/signup', 'post', request);

   return user;
}

export default {
   login,
   signup,
};
