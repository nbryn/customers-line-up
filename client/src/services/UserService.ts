import {LOGIN_URL} from '../api/URL';
import { fetch} from '../api/RequestHandler';
import { UserDTO } from '../dto/User';

async function login(email: string, password: string): Promise<UserDTO> {
   const user: UserDTO = await fetch(LOGIN_URL, 'post', {email, password});


   return user;
}


// async function signup(request) {
//    const user = await fetchFromServer(BASE_URL + 'user/register', 'post', request);

//    return user;
// }


export default {
    login
}
