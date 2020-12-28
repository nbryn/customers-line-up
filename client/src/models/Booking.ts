import {DTO} from './General';

export interface BookingDTO extends DTO {
    timeSlotId: number;
    address: string;
    business: string;
    userMail: string;
    date: string;
    capacity: string;
    interval: string;
}