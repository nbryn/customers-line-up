import {DTO} from './General';

export interface BusinessDTO extends DTO {
   address: string;
   name: string;
   zip: string;
   type: string;
   timeSlotLength: number | string;
   capacity: number | string;
   opens: string;
   closes: string;
   ownerEmail?: string;
   longitude?: number;
   latitude?: number;
}

export type BusinessDataDTO = {
   numberOfBookings: number;
   numberOfTimeSlots: number;
   numberOfEmployees?: number;
};

export interface TimeSlotDTO extends DTO {
   id: string;
   date: string;
   start: string;
   end: string;
   business?: string;
}
