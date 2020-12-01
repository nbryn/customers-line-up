import Cookies from 'js-cookie';

import { fetch} from './ApiService';
import { UserDTO } from '../models/dto/User';


// async function login(request: LoginRequest): Promise<UserDTO> {
//    const response: LoginResponse = await fetch<LoginResponse>(BASE_URL + 'user/login', 'post', request);

//    Cookies.set('token', response.token);

//    const user: UserDTO = {
//       name: response.name,
//       email: response.email,
//       zip: response.zip,
//       isOwner: response.isOwner as boolean
//    };

//    return user;
// }


// async function signup(request) {
//    const user = await fetchFromServer(BASE_URL + 'user/register', 'post', request);

//    return user;
// }

