import {DTO, HasAddress, Index} from '../../shared/models/General';
import {BookingDTO} from '../booking/Booking';
import {BusinessDTO} from '../business/Business';
import {MessageDTO} from '../message/Message';

export interface UserDTO extends HasAddress {
    email: string;
    name?: string;
    id?: string;
    password?: string;
    token?: string;
    role?: 'User' | 'Employee' | 'Owner' | 'Admin';
    bookings?: BookingDTO[];
    businesses?: BusinessDTO[];
    messages?: MessageDTO[];
}

export interface LoginDTO extends Index {
    email: string;
    password: string;
}

export type NotEmployedByBusiness = {
    businessId: string;
    users: UserDTO[];
};

export type TokenResponse = {
    token: string;
};
