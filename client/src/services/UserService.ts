import Cookies from 'js-cookie';

import { BASE_URL } from './Url';
import { fetchFromServer } from './Fetch';
import { UserDTO } from '../models/dto/User';
import { BusinessQueueDTO } from '../models/dto/Business';

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


// JWT will be used to get current user's email
async function fetchUserBookings(): Promise<BusinessQueueDTO[]> {
   const bookings = await fetchFromServer<BusinessQueueDTO[]>(BASE_URL + 'businessqueue/user', 'get');

   console.log(bookings);

   return bookings;
}

async function removeBooking(queueId: number): Promise<void> {
   const bookings = await fetchFromServer<void>(BASE_URL + 'businessqueue/user', 'get');

   return bookings;
}

// async function signup(request) {
//    const user = await fetchFromServer(BASE_URL + 'user/register', 'post', request);

//    return user;
// }

export default {
   fetchUserBookings,
   login,
   removeBooking,
};
