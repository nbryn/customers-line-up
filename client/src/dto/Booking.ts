import {DTO} from './Business';

export interface BookingDTO extends DTO {
    timeSlotId: number;
    businessId: number;
    userMail: string;
    capacity: string;
    interval: string;
}