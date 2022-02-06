import {HasAddress, Index} from '../../shared/models/General';
import { MessageResponse } from '../message/Message';

export interface UserDTO extends HasAddress {
    [key: string]: string | number | undefined;
    email: string;
    id?: string;
    name?: string;
    password?: string;
    token?: string;
    role?: 'User' | 'Employee' | 'Owner' | 'Admin';
}

export interface LoginDTO extends Index {
    email: string;
    password: string;
}

export type NotEmployedByBusiness = {
    businessId: string;
    users: UserDTO[];
};

export interface UserMessageResponse extends MessageResponse {
    userId: string;
}
