import {DTO} from './General';

export interface BookingDTO extends DTO {
    timeSlotId: number;
    address: string;
    business: string;
    longitude: number;
    latitude: number;
    userMail: string;
    date: string;
    capacity: string;
    interval: string;
}