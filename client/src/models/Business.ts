import {DTO} from './General';

export interface BusinessDTO extends DTO {
    name: string;
    zip: number | string;
    type: string;
    timeSlotLength: number | string;  
    capacity: number | string;  
    opens: string;
    closes: string;
}

export type BusinessDataDTO = {
    numberOfBookings: number;
    numberOfTimeSlots: number;
    numberOfEmployees?: number;
}

export interface TimeSlotDTO extends DTO {
    id: number;
    date: string;
    start: string;
    end: string;
    business?: string;
}